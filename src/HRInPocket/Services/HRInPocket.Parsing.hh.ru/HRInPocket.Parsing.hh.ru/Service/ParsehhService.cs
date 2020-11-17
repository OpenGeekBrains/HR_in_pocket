﻿using System;
using System.Linq;

using AngleSharp;

using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Models.Entites;

namespace HRInPocket.Parsing.hh.ru.Service
{
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
        //public event Action<Vacancy> Result;
        private const string _HHUrl = "https://hh.ru/search/vacancy";

        public event EventHandler<Vacancy> Result;
        /// <summary>
        /// Парсит https://hh.ru/search/vacancy и возвращает значения по готовности через событие Result
        /// </summary>
        /// <param name="GetParameters">
        /// Если нужно задать точные параметры поиска, передайте их в свойстве GetParameters
        /// в формате "?param1=value&param2=value&...&paramN=value"
        /// </param>
        public async void Parse(string GetParameters=null)
        {
            string path;
            if (GetParameters != null) path = _HHUrl + GetParameters;
            else path = _HHUrl;            
            do {
                var config = Configuration.Default.WithDefaultLoader();

                var document = await BrowsingContext.New(config).OpenAsync(Url.Create(path));

                var items = document.QuerySelectorAll("div")
                    .Where(item => item.ClassName != null && (item.ClassName
                        .Equals("vacancy-serp-item") || item.ClassName.Contains("vacancy-serp-item ")));

                foreach (var fitem in items)
                {
                    var vacancyNameParse = fitem.QuerySelectorAll("a")
                        .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                            .Equals("vacancy-serp__vacancy-title")).FirstOrDefault();
                    if (vacancyNameParse != null)
                    {

                        var companyParse = fitem.QuerySelectorAll("a")
                            .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                .Equals("vacancy-serp__vacancy-employer")).FirstOrDefault();
                        var addressParse = fitem.QuerySelectorAll("span")
                            .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                .Equals("vacancy-serp__vacancy-address")).FirstOrDefault();
                        var compensationParse = fitem.QuerySelectorAll("span")
                            .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                .Equals("vacancy-serp__vacancy-compensation")).FirstOrDefault();
                        var descriptionShortPasrse = fitem.QuerySelectorAll("div")
                            .Where(item => item.ClassName != null && item.ClassName
                                .Equals("g-user-content")).FirstOrDefault();
                        var DatePasrse = fitem.QuerySelectorAll("span")
                            .Where(item => item.ClassName != null && item.ClassName
                                .Equals("vacancy-serp-item__publication-date")).FirstOrDefault();

                        var vacancyname = vacancyNameParse.TextContent;
                        var vacancynameUrl = vacancyNameParse.GetAttribute("href");
                        var company = companyParse?.TextContent;
                        var companyUrl = "https://hh.ru" + companyParse?.GetAttribute("href");
                        var address = addressParse.TextContent;
                        var descriptionShort = descriptionShortPasrse.TextContent;
                        var prefix = "";
                        var currency = "";
                        var date = DateTime.Parse(DatePasrse.TextContent.Replace((char)160, (char)32));
                        ulong compensationUp=0, compensationDown=0;
                        if (compensationParse != null)
                        {
                            var compensationString = compensationParse.TextContent;
                            currency = compensationString.Substring(compensationString.LastIndexOf(' ') + 1);
                            compensationString = compensationString.Substring(0, compensationString.LastIndexOf(' '));
                            var getIndexOff = compensationString.IndexOf(' ');
                            
                            if (getIndexOff>0)
                            {
                                prefix = compensationString.Substring(0, getIndexOff);
                            }                            
                            if (prefix.Equals("от") || prefix.Equals("до"))
                            {
                                compensationString = compensationString.Substring(getIndexOff + 1);
                                compensationString = compensationString.Replace((char)160, (char)32);
                                compensationString = compensationString.Replace(" ", "");
                                if (prefix.Equals("от"))
                                {
                                    compensationDown = 0;
                                    ulong.TryParse(compensationString,out compensationUp);
                                }
                                if (prefix.Equals("до"))
                                {
                                    compensationUp = 0;
                                    ulong.TryParse(compensationString, out compensationDown);
                                }
                            }
                            else
                            {
                                compensationString = compensationString.Replace((char)160, (char)32);
                                compensationString = compensationString.Replace(" ", "");
                                var CompensationStringSplit = compensationString.Split('-');
                                if (CompensationStringSplit.GetLength(0)>1)
                                {
                                    ulong.TryParse(CompensationStringSplit[0], out compensationUp);
                                    ulong.TryParse(CompensationStringSplit[1], out compensationDown);
                                }
                            }
                        }
                        var vacancy = new Vacancy()
                        {
                            Name = new VacancyName()
                            {
                                Name = vacancyname,
                                Url = vacancynameUrl
                            },
                            Company = new Company()
                            {
                                Name = company,
                                Url = companyUrl
                            },
                            CompensationUp = compensationUp,
                            CompensationDown = compensationDown,
                            CurrencyCode = currency,
                            Date = date,
                            PrefixCompensation = prefix,
                            ShortDescription = descriptionShort,
                            VacancyAddress = address
                        };
                        Result?.Invoke(this,vacancy);
                    }
                }
                var NextPage = document.QuerySelectorAll("a")
                    .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                        .Equals("pager-next")).FirstOrDefault();
                if (NextPage is null) return;
                else path = "https://hh.ru" + NextPage.GetAttribute("href");
            } while (true);
        }
    }
}
