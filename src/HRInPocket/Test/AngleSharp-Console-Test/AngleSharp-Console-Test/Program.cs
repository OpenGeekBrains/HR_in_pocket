using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using HRInPocket.Parsing.hh.ru.Models;
using HRInPocket.Parsing.hh.ru.Service;

namespace AngleSharp_Console_Test
{
    class Program
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();
        static async Task Main()
        {
            Console.WriteLine("Press any key to start parsing");
            Console.ReadKey();
            
            await ParseUseDLL(s_cts.Token);

            Console.WriteLine("\nParsing stopped.");
            Console.ReadKey();

        }
        static async Task<Vacancy[]> ParseUseDLL(CancellationToken token)
        {
            Vacancy[] collection;
            var NextPage = "https://hh.ru/search/vacancy";
            var parse = new Parsehh();
            do
            {
                (collection, NextPage) = await parse.ParseAsync(token, NextPage);
                foreach (var vacancy in collection) ShowResult(vacancy);

                Console.WriteLine("Press Enter to continue parsing. Press Escape to stop.");
                Console.WriteLine("-----------");
                var key = Console.ReadKey();
                if (Equals(key.Key, ConsoleKey.Escape))
                {
                    s_cts.Cancel();
                }
            } while (!string.IsNullOrEmpty(NextPage) && !token.IsCancellationRequested);
            s_cts.Dispose();
            return collection;
        }

        private static void ShowResult(Vacancy vacancy)
        {
            Console.WriteLine("{0} - {1}", vacancy.Name, vacancy.Url);
            Console.WriteLine(vacancy.VacancyAddress);
            if (vacancy.CompensationUp == 0 && vacancy.CompensationDown > 0)
            {
                Console.WriteLine("{0} {1} {2}", vacancy.PrefixCompensation, vacancy.CompensationDown, vacancy.CurrencyCode);
            }
            if (vacancy.CompensationDown == 0 && vacancy.CompensationUp > 0)
            {
                Console.WriteLine("{0} {1} {2}", vacancy.PrefixCompensation, vacancy.CompensationUp, vacancy.CurrencyCode);
            }
            if (vacancy.CompensationDown > 0 && vacancy.CompensationUp > 0)
            {
                Console.WriteLine("{0}-{1} {2}", vacancy.CompensationUp, vacancy.CompensationDown, vacancy.CurrencyCode);
            }
            Console.WriteLine("{0} - {1}", vacancy.Company.Name, vacancy.Company.Url);
            Console.WriteLine(vacancy.ShortDescription);
            Console.WriteLine("Date: {0}", vacancy.Date.ToString());
            Console.WriteLine("-----------");
        }
    }
}
