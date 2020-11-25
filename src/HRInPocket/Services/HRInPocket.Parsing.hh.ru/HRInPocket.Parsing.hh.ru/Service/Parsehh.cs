using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using AngleSharp;
using AngleSharp.Dom;

using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Models.Entites;

namespace HRInPocket.Parsing.hh.ru.Service
{
    ///<inheritdoc cref="IParsehh"/>
    public class Parsehh : IParsehh
    {
        private const string _HHUrl = "https://hh.ru/search/vacancy";

        ///<inheritdoc/>
        public event EventHandler<VacancyEventArgs> Result;
        
        protected virtual void OnVacancyEventArgs(Vacancy vacancy)
        {
            var e = new VacancyEventArgs { Vacancy = vacancy };
            Result?.Invoke(this, e);
        }

        ///<inheritdoc/>
        public async IAsyncEnumerable<Vacancy> ParseEnumerableAsync([EnumeratorCancellation] CancellationToken token, string page, string GetParameters)
        {
            var random = new Random();
            var path = string.IsNullOrEmpty(GetParameters) ? page : page + "?text=" + GetParameters;
            IDocument document;
            IEnumerable<IElement> items;

            do
            {
                try
                {
                    var config = Configuration.Default.WithDefaultLoader();

                    document = await BrowsingContext.New(config).OpenAsync(Url.Create(path));

                    items = document.QuerySelectorAll("div")
                        .Where(item => item.ClassName != null &&
                                       (item.ClassName.Equals("vacancy-serp-item") ||
                                       item.ClassName.Contains("vacancy-serp-item ")));
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.ToString());
                    throw e;
                }

                foreach (var fitem in items)
                {

                    var vacancyNameParse = fitem.QuerySelectorAll("a")
                        .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-title"));

                    if (vacancyNameParse != null)
                    {
                        var vacancy = VacancyCreate(fitem, vacancyNameParse);
                        CompenstionParse(vacancy, fitem);
                        yield return vacancy;
                    }

                }

                var NextPage = document.QuerySelectorAll("a")
                    .FirstOrDefault(item => DataQA(item, "pager-next"));

                if (NextPage is null) yield return null;
                path = "https://hh.ru" + NextPage.GetAttribute("href");

                var taskDelay = Task.Delay(random.Next(300, 2000));
                await taskDelay;

            } while (!token.IsCancellationRequested);
            
        }

        ///<inheritdoc/>
        public async Task ParseAsync(CancellationToken token, string page, string GetParameters)
        {
            var random = new Random();
            var path = string.IsNullOrEmpty(GetParameters) ? page : page + "?text=" + GetParameters;

            try
            {
                do
                {
                    var config = Configuration.Default.WithDefaultLoader();

                    var document = await BrowsingContext.New(config).OpenAsync(Url.Create(path));

                    var items = document.QuerySelectorAll("div")
                        .Where(item => item.ClassName != null &&
                                       (item.ClassName.Equals("vacancy-serp-item") ||
                                       item.ClassName.Contains("vacancy-serp-item ")));

                    foreach (var fitem in items)
                    {

                        var vacancyNameParse = fitem.QuerySelectorAll("a")
                            .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-title"));

                        if (vacancyNameParse != null)
                        {
                            var vacancy = VacancyCreate(fitem, vacancyNameParse);
                            CompenstionParse(vacancy, fitem);
                            OnVacancyEventArgs(vacancy);
                        }

                    }
                    var NextPage = document.QuerySelectorAll("a")
                        .FirstOrDefault(item => DataQA(item, "pager-next"));

                    if (NextPage is null) return;
                    path = "https://hh.ru" + NextPage.GetAttribute("href");

                    var taskDelay = Task.Delay(random.Next(300, 2000));
                    await taskDelay;

                } while (!token.IsCancellationRequested);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Создание вакансии
        /// </summary>
        /// <param name="fitem">Объект коллекции, из которой получают данные о вакансии</param>
        /// <param name="vacancyNameParse">Объект из fitem, содержащий данные о названии и адресе вакансии</param>
        /// <returns>Созданная вакансия</returns>
        private static Vacancy VacancyCreate(IElement fitem, IElement vacancyNameParse) => new Vacancy
        {
            Company = new Company
            {
                Name = fitem.QuerySelectorAll("a")
                    .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-employer"))
                    ?.TextContent,

                Url = "https://hh.ru" + fitem.QuerySelectorAll("a")
                    .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-employer"))
                    ?.GetAttribute("href")
            },
            Name = vacancyNameParse?.TextContent,
            Url = vacancyNameParse?.GetAttribute("href"),
            VacancyAddress = fitem.QuerySelectorAll("span")
                .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-address"))
                ?.TextContent,

            ShortDescription = fitem.QuerySelectorAll("div")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Equals("g-user-content"))
                ?.TextContent,

            Date = DateTime.Parse(fitem.QuerySelectorAll("span")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Equals("vacancy-serp-item__publication-date"))
                ?.TextContent.Replace((char)160, (char)32)),
        };

        /// <summary>
        /// Получение данных о заработной плате
        /// </summary>
        /// <param name="vacancy">Вакансия, в которую будут внесены данные о зарплате</param>
        /// <param name="fitem">Объект, из которого будет получена строка зарплаты</param>
        private static void CompenstionParse(Vacancy vacancy, IElement fitem)
        {
            var compensationParse = fitem.QuerySelectorAll("span")
                .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-compensation"));

            if (compensationParse == null) return;

            var spaceIndex = compensationParse.TextContent.LastIndexOf(' ');
            vacancy.CurrencyCode = compensationParse.TextContent[(spaceIndex + 1)..];
            var compensationString = compensationParse.TextContent[..spaceIndex].Replace((char)160, (char)32).Replace((char)8239, (char)32).Replace(" ", "");


            if (compensationString.Contains("от")) CompensationWithPrefix("от");
            else if (compensationString.Contains("до")) CompensationWithPrefix("до");
            else
            {
                var CompensationStringSplit = compensationString.Split((char)8211, (char)45);

                if (CompensationStringSplit.Length > 1)
                {
                    ulong.TryParse(CompensationStringSplit[0], out var compensationDown);
                    ulong.TryParse(CompensationStringSplit[1], out var compensationUp);

                    vacancy.CompensationDown = compensationDown;
                    vacancy.CompensationUp = compensationUp;
                }
            }

            if (vacancy.CompensationDown == 0 &&
                vacancy.CompensationUp == 0 &&
                !string.IsNullOrEmpty(vacancy.PrefixCompensation))
            {
                throw new FormatException($"Ошибка формата строки, не удалось извлечь значение {{compensationString}}:\n\"{compensationString}\"");
            }

            void CompensationWithPrefix(string prefix)
            {
                vacancy.PrefixCompensation = prefix;
                var temp = compensationString.Remove(0, 2);
                ulong.TryParse(temp, out var compensation);

                if (prefix.Equals("от")) vacancy.CompensationDown = compensation;
                else if (prefix.Equals("до")) vacancy.CompensationUp = compensation;
            }
        }

        /// <summary>
        /// Поиск в объекте по указанной строке
        /// </summary>
        /// <param name="item">Объект, в котором ведется поиск</param>
        /// <param name="searchline">Строка, которую необходимо найти в объекте</param>
        /// <returns>true если строка найдена в переданном объекте</returns>
        private static bool DataQA(IElement item, string searchline) => item.HasAttribute("data-qa") && item.GetAttribute("data-qa").Equals(searchline);
    }
}