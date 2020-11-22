using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AngleSharp;
using AngleSharp.Dom;

using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Models.Entites;

namespace HRInPocket.Parsing.hh.ru.Service
{
    public class VacancyEventArgs : EventArgs
    {
        public Vacancy Vacancy { get; set; }
    }
    public class ParsehhService : IParsehhService
    {
        public IParsehh GetPasrse() => new Parsehh();
    }
    /// <summary>
    /// Парсер загружает страницу по адресу https://hh.ru/search/vacancy
    /// </summary>
    public class Parsehh : IParsehh
    {
        private const string _HHUrl = "https://hh.ru/search/vacancy";

        public event EventHandler<VacancyEventArgs> Result;

        protected virtual void OnVacancyEventArgs(Vacancy vacancy)
        {
            var e = new VacancyEventArgs { Vacancy = vacancy };
            Result?.Invoke(this, e);
        }

        /// <summary>
        /// Парсит https://hh.ru/search/vacancy и возвращает значения по готовности через событие Result
        /// </summary>
        /// <param name="GetParameters">
        /// Если нужно задать точные параметры поиска, передайте их в свойстве GetParameters
        /// в формате "?param1=value&param2=value&...&paramN=value"
        /// </param>
        public async Task ParseAsync(CancellationToken token, string GetParameters = null)
        {
            var path = GetParameters != null ? _HHUrl + GetParameters : _HHUrl;
            do
            {
                var config = Configuration.Default.WithDefaultLoader();

                //todo: Разобраться с исключениями: 
                // Вызвано исключение: "System.Net.WebException" в System.Net.Requests.dll
                // Вызвано исключение: "System.Net.WebException" в System.Private.CoreLib.dll
                // при вызове await BrowsingContext.New(config).OpenAsync(Url.Create(path));
                var document = await BrowsingContext.New(config).OpenAsync(Url.Create(path));

                var items = document.QuerySelectorAll("div")
                    .Where(item => item.ClassName != null && (item.ClassName
                        .Equals("vacancy-serp-item") || item.ClassName.Contains("vacancy-serp-item ")));

                foreach (var fitem in items)
                {
                    try
                    {
                        var vacancyNameParse = fitem.QuerySelectorAll("a")
                        .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                            .Equals("vacancy-serp__vacancy-title")).FirstOrDefault();

                        if (vacancyNameParse != null)
                        {
                            var vacancy = VacancyCreate(fitem, vacancyNameParse);
                            CompenstionParse(vacancy, fitem);
                            OnVacancyEventArgs(vacancy);
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.ToString());
                        throw e;
                    }
                }
                var NextPage = document.QuerySelectorAll("a")
                    .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                        .Equals("pager-next")).FirstOrDefault();
                if (NextPage is null) return;
                else path = "https://hh.ru" + NextPage.GetAttribute("href");
            } while (!token.IsCancellationRequested);
        }

        /// <summary>
        /// Создание вакансии
        /// </summary>
        /// <param name="fitem">Объект коллекции, из которой получают данные о вакансии</param>
        /// <param name="vacancyNameParse">Объект из fitem, содержащий данные о названии и адресе вакансии</param>
        /// <returns>Созданная вакансия</returns>
        private static Vacancy VacancyCreate(IElement fitem, IElement vacancyNameParse) => new Vacancy()
        {
            Name = new VacancyName()
            {
                Name = vacancyNameParse?.TextContent,
                Url = vacancyNameParse?.GetAttribute("href")
            },
            Company = new Company()
            {
                Name = fitem.QuerySelectorAll("a")
                    .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                    .Equals("vacancy-serp__vacancy-employer")).FirstOrDefault()
                    ?.TextContent,
                Url = "https://hh.ru" + fitem.QuerySelectorAll("a")
                    .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                    .Equals("vacancy-serp__vacancy-employer")).FirstOrDefault()
                    ?.GetAttribute("href")
            },
            VacancyAddress = fitem.QuerySelectorAll("span")
                .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                .Equals("vacancy-serp__vacancy-address")).FirstOrDefault()
                ?.TextContent,
            ShortDescription = fitem.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName
                .Equals("g-user-content")).FirstOrDefault()
                ?.TextContent,
            Date = DateTime.Parse(fitem.QuerySelectorAll("span")
                .Where(item => item.ClassName != null && item.ClassName
                .Equals("vacancy-serp-item__publication-date")).FirstOrDefault()
                ?.TextContent.Replace((char)160, (char)32)),
        };

        /// <summary>
        /// Метод рассчета заработной платы для вакансии
        /// </summary>
        /// <param name="vacancy">Вакансия, в которую записываются данные о зарплате</param>
        /// <param name="compensationParse">Объект, из которого получают данные о зарплате</param>
        private static void CompenstionParse(Vacancy vacancy, IElement fitem)
        {
            var compensationParse = fitem.QuerySelectorAll("span")
                .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                .Equals("vacancy-serp__vacancy-compensation")).FirstOrDefault();

            if (compensationParse == null) return;

            vacancy.CurrencyCode = compensationParse.TextContent.Substring(compensationParse.TextContent.LastIndexOf(' ') + 1);
            var compensationString = compensationParse.TextContent.Substring(0, compensationParse.TextContent.LastIndexOf(' '));

            var getIndexOff = compensationString.IndexOf(' ');

            if (getIndexOff > 0)
            {
                vacancy.PrefixCompensation = compensationString.Substring(0, getIndexOff);

                compensationString = compensationString.Substring(getIndexOff + 1).Replace((char)160, (char)32).Replace(" ", "");

                if (vacancy.PrefixCompensation.Equals("от") && ulong.TryParse(compensationString, out ulong compensationDown))
                {
                    vacancy.CompensationDown = compensationDown;
                }
                else if (vacancy.PrefixCompensation.Equals("до") && ulong.TryParse(compensationString, out ulong compensationUp))
                {
                    vacancy.CompensationUp = compensationUp;
                }
                return;
            }
            else
            {
                compensationString = compensationString.Replace((char)160, (char)32).Replace(" ", "");
                var CompensationStringSplit = compensationString.Split('-');

                if (CompensationStringSplit.Length > 1)
                {
                    ulong.TryParse(CompensationStringSplit[0], out ulong compensationDown);
                    ulong.TryParse(CompensationStringSplit[1], out ulong compensationUp);

                    vacancy.CompensationDown = compensationDown;
                    vacancy.CompensationUp = compensationUp;
                }
            }

            if (ulong.TryParse(vacancy.PrefixCompensation, out var s))
            {
                var c = 5;
                //TODO:  throw new FormatException();
            }

            if(!string.IsNullOrEmpty(vacancy.PrefixCompensation) && vacancy.CompensationDown == 0 && vacancy.CompensationUp == 0)
            {
                var x = 0;
                //TODO:  throw new FormatException();
            }
        }
    }
}

