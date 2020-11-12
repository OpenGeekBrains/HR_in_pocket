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
            IEnumerable<IElement> items = document.QuerySelectorAll("a")
                .Where(item => item.ClassName != null && item.ClassName.Contains("bloko-link HH-LinkModifier"));

            foreach (var item in items)
            {
                Console.WriteLine(item.TextContent);
            }
            //Console.WriteLine(document.DocumentElement.OuterHtml);

        }
    }
}
