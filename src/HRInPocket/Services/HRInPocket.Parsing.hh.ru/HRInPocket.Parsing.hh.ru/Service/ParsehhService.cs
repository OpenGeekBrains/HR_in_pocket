using System;
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
        public event Action<Vacancy> Result;
        readonly string HHUrl = "https://hh.ru/search/vacancy";

        /// <summary>
        /// Парсит https://hh.ru/search/vacancy и возвращает значения по готовности через событие Result
        /// </summary>
        /// <param name="GetParameters">
        /// Если нужно задать точные параметры поиска, передайте их в свойстве GetParameters
        /// в формате "?param1=value&param2=value&...&paramN=value"
        /// </param>
        public void Parse(string GetParameters=null)
        {
            string path;
            if (GetParameters != null) path = HHUrl + GetParameters;
            else path = HHUrl;            
            do {
                var config = Configuration.Default.WithDefaultLoader();

                var document = BrowsingContext.New(config).OpenAsync(Url.Create(path)).Result;

                var items = document.QuerySelectorAll("div")
                    .Where(item => item.ClassName != null && (item.ClassName
                        .Equals("vacancy-serp-item") || item.ClassName.Contains("vacancy-serp-item ")));

                foreach (var fitem in items)
                {
                    var VacancyNameParse = fitem.QuerySelectorAll("a")
                        .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                            .Equals("vacancy-serp__vacancy-title")).FirstOrDefault();
                    if (VacancyNameParse != null)
                    {

                        var CompanyParse = fitem.QuerySelectorAll("a")
                            .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                .Equals("vacancy-serp__vacancy-employer")).FirstOrDefault();
                        var AddressParse = fitem.QuerySelectorAll("span")
                            .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                .Equals("vacancy-serp__vacancy-address")).FirstOrDefault();
                        var CompensationParse = fitem.QuerySelectorAll("span")
                            .Where(item => item.HasAttribute("data-qa") != false && item.GetAttribute("data-qa")
                                .Equals("vacancy-serp__vacancy-compensation")).FirstOrDefault();
                        var DescriptionShortPasrse = fitem.QuerySelectorAll("div")
                            .Where(item => item.ClassName != null && item.ClassName
                                .Equals("g-user-content")).FirstOrDefault();
                        var DatePasrse = fitem.QuerySelectorAll("span")
                            .Where(item => item.ClassName != null && item.ClassName
                                .Equals("vacancy-serp-item__publication-date")).FirstOrDefault();

                        var _vacancyname = VacancyNameParse.TextContent;
                        var _vacancynameUrl = VacancyNameParse.GetAttribute("href");
                        var _company = CompanyParse?.TextContent;
                        var _companyUrl = "https://hh.ru" + CompanyParse?.GetAttribute("href");
                        var _address = AddressParse.TextContent;
                        var _descriptionShort = DescriptionShortPasrse.TextContent;
                        var Prefix = "";
                        var Currency = "";
                        var _date = DateTime.Parse(DatePasrse.TextContent.Replace((char)160, (char)32));
                        ulong CompensationUp=0, CompensationDown=0;
                        if (CompensationParse != null)
                        {
                            var CompensationString = CompensationParse.TextContent;
                            Currency = CompensationString.Substring(CompensationString.LastIndexOf(' ') + 1);
                            CompensationString = CompensationString.Substring(0, CompensationString.LastIndexOf(' '));
                            var getIndexOff = CompensationString.IndexOf(' ');
                            
                            if (getIndexOff>0)
                            {
                                Prefix = CompensationString.Substring(0, getIndexOff);
                            }                            
                            if (Prefix.Equals("от") || Prefix.Equals("до"))
                            {
                                CompensationString = CompensationString.Substring(getIndexOff + 1);
                                CompensationString = CompensationString.Replace((char)160, (char)32);
                                CompensationString = CompensationString.Replace(" ", "");
                                if (Prefix.Equals("от"))
                                {
                                    CompensationDown = 0;
                                    ulong.TryParse(CompensationString,out CompensationUp);
                                }
                                if (Prefix.Equals("до"))
                                {
                                    CompensationUp = 0;
                                    ulong.TryParse(CompensationString, out CompensationDown);
                                }
                            }
                            else
                            {
                                CompensationString = CompensationString.Replace((char)160, (char)32);
                                CompensationString = CompensationString.Replace(" ", "");
                                var CompensationStringSplit = CompensationString.Split('-');
                                if (CompensationStringSplit.GetLength(0)>1)
                                {
                                    ulong.TryParse(CompensationStringSplit[0], out CompensationUp);
                                    ulong.TryParse(CompensationStringSplit[1], out CompensationDown);
                                }
                            }
                        }
                        var vacancy = new Vacancy()
                        {
                            Name = new VacancyName()
                            {
                                Name = _vacancyname,
                                Url = _vacancynameUrl
                            },
                            Company = new Company()
                            {
                                Name = _company,
                                Url = _companyUrl
                            },
                            CompensationUp = CompensationUp,
                            CompensationDown = CompensationDown,
                            CurrencyCode = Currency,
                            Date = _date,
                            PrefixCompensation = Prefix,
                            ShortDescription = _descriptionShort,
                            VacancyAddress = _address
                        };
                        Result?.Invoke(vacancy);
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
