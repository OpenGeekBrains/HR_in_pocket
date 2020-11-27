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
using HRInPocket.Parsing.hh.ru.Models;

namespace HRInPocket.Parsing.hh.ru.Service
{
    ///<inheritdoc cref="IParsehh"/>
    public class Parsehh : IParsehh
    {
        /// <summary> Рандом, использующийся для определения случайной задержки между отправкой запроса на получение следующей страницы для парсера </summary>
        private readonly static Random _Random = new Random();
        /// <summary> Объект, содержащий ссылку на следующую страницу</summary>
        private static IElement _NextPage;
        /// <summary> Коллекция, содержащая объекты, которые содержат вакансии </summary>
        private static IEnumerable<IElement> _Items;

        ///<inheritdoc/>
        public event EventHandler<VacancyEventArgs> SendVacancy;
        
        protected virtual void OnVacancyEventArgs(Vacancy vacancy)
        {
            var e = new VacancyEventArgs { Vacancy = vacancy };
            SendVacancy?.Invoke(this, e);
        }

        ///<inheritdoc/>
        public async IAsyncEnumerable<Vacancy> ParseEnumerableAsync([EnumeratorCancellation] CancellationToken token, string page, int MinWaitTime = 300, int MaxWaitTime = 2000)
        {
            do
            {
                await GetPage(page, MinWaitTime, MaxWaitTime);

                foreach (var fitem in _Items)
                {
                    var vacancyNameParse = fitem.QuerySelectorAll("a")
                        .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-title"));

                    if (vacancyNameParse != null)
                    {
                        var vacancy = VacancyCreate(fitem, vacancyNameParse);
                        CompensationParse(vacancy, fitem);
                        yield return vacancy;
                    }
                }

                if (_NextPage is null)
                {
                    yield return null;
                }
                page = "https://hh.ru" + _NextPage.GetAttribute("href");

            } while (!token.IsCancellationRequested);
        }

        ///<inheritdoc/>
        public async Task<(Vacancy[], string)> ParseAsync(CancellationToken token, string page, int MinWaitTime = 300, int MaxWaitTime = 2000)
        {
            var result = new List<Vacancy>();

            await GetPage(page, MinWaitTime, MaxWaitTime);

            foreach (var fitem in _Items)
            {
                var vacancyNameParse = fitem.QuerySelectorAll("a")
                    .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-title"));

                if (vacancyNameParse != null)
                {
                    var vacancy = VacancyCreate(fitem, vacancyNameParse);
                    CompensationParse(vacancy, fitem);
                    result.Add(vacancy);
                    if (token.IsCancellationRequested) return (result.ToArray(), null);
                }
            }

            if (_NextPage is null) return (result.ToArray(), null);
            page = "https://hh.ru" + _NextPage.GetAttribute("href");

            return (result.ToArray(), page);
        }

        ///<inheritdoc/>
        [ObsoleteAttribute("Данный метод парсинга устарел. Используйте новый ParseAsync или ParseEnumerableAsync.", false)]
        public async Task ParseAsync(CancellationToken token, string page, string GetParameters)
        {
            var random = new Random();
            var MaxWaitTime = 2000;
            var MinWaitTime = 300;
            var path = string.IsNullOrEmpty(GetParameters) ? page : page + "?text=" + GetParameters;

            try
            {
                do
                {
                    await GetPage(path, MinWaitTime, MaxWaitTime);

                    foreach (var fitem in _Items)
                    {
                        var vacancyNameParse = fitem.QuerySelectorAll("a")
                            .FirstOrDefault(item => DataQA(item, "vacancy-serp__vacancy-title"));

                        if (vacancyNameParse != null)
                        {
                            var vacancy = VacancyCreate(fitem, vacancyNameParse);
                            CompensationParse(vacancy, fitem);
                            OnVacancyEventArgs(vacancy);
                        }
                    }

                    if (_NextPage is null) return;
                    path = "https://hh.ru" + _NextPage.GetAttribute("href");

                    var taskDelay = Task.Delay(random.Next(MinWaitTime, MaxWaitTime));
                    await taskDelay;

                } while (!token.IsCancellationRequested);
            }
            catch (Exception e)
            {
                throw new ParseHHVacancyException($"Невозможно получить данные из страницы {path}", e);
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
        private static void CompensationParse(Vacancy vacancy, IElement fitem)
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

        /// <summary> Получение коллекции объектов вакансий из указанной страницы </summary>
        /// <param name="path"> Страницы, из которой получается коллекция объектов вакансий </param>
        /// <param name="MinWaitTime"> Минимальное время ожидания до получения страницы </param>
        /// <param name="MaxWaitTime"> Максимальное время ожидания до получения страницы </param>
        /// <returns>Кортеж, где IEnumerable<IElement> items - коллекция объектов вакансий, IElement NextPage - объект, содержащий следующую страницу</returns>
        private static async Task GetPage(string path, int MinWaitTime, int MaxWaitTime)
        {
            try
            {
                await Task.Delay(_Random.Next(MinWaitTime, MaxWaitTime));

                var config = Configuration.Default.WithDefaultLoader();

                var document = await BrowsingContext.New(config).OpenAsync(Url.Create(path));

                _Items = document.QuerySelectorAll("div")
                    .Where(item => item.ClassName != null &&
                                   (item.ClassName.Equals("vacancy-serp-item") ||
                                   item.ClassName.Contains("vacancy-serp-item ")));

                _NextPage = document.QuerySelectorAll("a")
                    .FirstOrDefault(item => DataQA(item, "pager-next"));
            }
            catch (Exception e)
            {
                throw new ParseHHVacancyException($"Невозможно получить данные из страницы {path}", e);
            }
        }
    }
}