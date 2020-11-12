using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;


namespace AngleSharp_Console_Test
{
    class Program
    {
        static void Main()
        {
            //Use the default configuration for AngleSharp
            var config = Configuration.Default.WithDefaultLoader();

            var parser = new HtmlParser();

            var document = BrowsingContext.
                New(config).
                OpenAsync(
                Url.Create(
                    "https://hh.ru/search/vacancy?clusters=true&enable_snippets=true&L_save_area=true&area=113&from=cluster_area&showClusters=false"
                    )).Result;
            IEnumerable<IElement> items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("vacancy-serp-item"));

            foreach (var item in items)
            {
                var rowheader = item.GetElementsByClassName("vacancy-serp-item__row vacancy-serp-item__row_header");
                foreach (var rowheader_item in rowheader)
                {
                    Console.WriteLine(rowheader_item.TextContent);
                }
                var meta_info = item.QuerySelectorAll("div").
                    Where(item => item.ClassName != null && item.ClassName.Contains("vacancy-serp-item__meta-info"));
                foreach (var meta_info_item in meta_info)
                {
                    Console.WriteLine(meta_info_item.TextContent);
                }
                var item__info = item.QuerySelectorAll("div").
                    Where(item => item.ClassName != null && item.ClassName.Contains("g-user-content"));
                foreach (var item__row_item in item__info)
                {
                    Console.WriteLine(item__row_item.TextContent);
                }
                Console.WriteLine("============");
                Console.WriteLine(item.OuterHtml);
                break;
            }

        }
    }
}
