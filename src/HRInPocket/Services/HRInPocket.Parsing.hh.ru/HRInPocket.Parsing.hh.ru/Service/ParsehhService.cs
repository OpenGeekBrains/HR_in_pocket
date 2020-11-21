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
        public IParsehh GetPasrse()
        {
            return new Parsehh();
        }
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
                    var vacancy = new Vacancy()
                    {
                        Name = new VacancyName(),
                        Company = new Company()
                    };

                    IElement vacancyNameParse;

                    try
                    {
                        vacancyNameParse = fitem.QuerySelectorAll("a")
                        .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                            .Equals("vacancy-serp__vacancy-title")).FirstOrDefault();

                        if (vacancyNameParse != null)
                        {
                            IElement companyParse, addressParse, compensationParse, descriptionShortPasrse, DatePasrse;

                            companyParse = fitem.QuerySelectorAll("a")
                            .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                .Equals("vacancy-serp__vacancy-employer")).FirstOrDefault();
                            addressParse = fitem.QuerySelectorAll("span")
                                .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                    .Equals("vacancy-serp__vacancy-address")).FirstOrDefault();
                            compensationParse = fitem.QuerySelectorAll("span")
                                .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                    .Equals("vacancy-serp__vacancy-compensation")).FirstOrDefault();
                            descriptionShortPasrse = fitem.QuerySelectorAll("div")
                                .Where(item => item.ClassName != null && item.ClassName
                                    .Equals("g-user-content")).FirstOrDefault();
                            DatePasrse = fitem.QuerySelectorAll("span")
                                .Where(item => item.ClassName != null && item.ClassName
                                    .Equals("vacancy-serp-item__publication-date")).FirstOrDefault();

                            vacancy.Name.Name = vacancyNameParse?.TextContent;
                            vacancy.Name.Url = vacancyNameParse?.GetAttribute("href");
                            vacancy.Company.Name = companyParse?.TextContent;
                            vacancy.Company.Url = "https://hh.ru" + companyParse?.GetAttribute("href");
                            vacancy.VacancyAddress = addressParse?.TextContent;
                            vacancy.ShortDescription = descriptionShortPasrse?.TextContent;
                            vacancy.Date = DateTime.Parse(DatePasrse?.TextContent.Replace((char)160, (char)32));

                            if (compensationParse != null)
                            {
                                CompenstionParse(vacancy, compensationParse);
                            }
                            OnVacancyEventArgs(vacancy);
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
                var NextPage = document.QuerySelectorAll("a")
                    .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                        .Equals("pager-next")).FirstOrDefault();
                if (NextPage is null) return;
                else path = "https://hh.ru" + NextPage.GetAttribute("href");
            } while (!token.IsCancellationRequested);
        }

        private static void CompenstionParse(Vacancy vacancy, IElement compensationParse)
        {
            vacancy.CurrencyCode = compensationParse.TextContent.Substring(compensationParse.TextContent.LastIndexOf(' ') + 1);
            var compensationString = compensationParse.TextContent.Substring(0, compensationParse.TextContent.LastIndexOf(' '));

            var getIndexOff = compensationString.IndexOf(' ');

            if (getIndexOff > 0)
            {
                vacancy.PrefixCompensation = compensationString.Substring(0, getIndexOff);
            }

            if (!string.IsNullOrEmpty(vacancy.PrefixCompensation) && (vacancy.PrefixCompensation.Equals("от") || vacancy.PrefixCompensation.Equals("до")))
            {
                compensationString = compensationString.Substring(getIndexOff + 1).Replace((char)160, (char)32).Replace(" ", "");

                if (vacancy.PrefixCompensation.Equals("от") && ulong.TryParse(compensationString, out ulong compensationDown))
                {
                    vacancy.CompensationDown = compensationDown;
                }
                if (vacancy.PrefixCompensation.Equals("до") && ulong.TryParse(compensationString, out ulong compensationUp))
                {
                    vacancy.CompensationUp = compensationUp;
                }
            }
            else
            {
                compensationString = compensationString.Replace((char)160, (char)32).Replace(" ", "");
                var CompensationStringSplit = compensationString.Split('-');

                if (CompensationStringSplit.Length > 1)
                {
                    if (ulong.TryParse(CompensationStringSplit[0], out ulong compensationUp))
                    {
                        vacancy.CompensationUp = compensationUp;
                    }
                    if (ulong.TryParse(CompensationStringSplit[1], out ulong compensationDown))
                    {
                        vacancy.CompensationDown = compensationDown;
                    }
                }
                //else throw new FormatException();
            }
        }
    }
}

