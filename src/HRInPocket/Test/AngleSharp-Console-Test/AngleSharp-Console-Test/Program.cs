using System;
using HRInPocket.Parsing.hh.ru.Models.Entites;
using HRInPocket.Parsing.hh.ru.Service;

namespace AngleSharp_Console_Test
{
    class Program
    {
        static void Main()
        {
            ParseUseDLL();
            Console.ReadKey();
        }
        static void ParseUseDLL()
        {
            var parse = new Parsehh();
            parse.Result += GetResult;
            parse.Parse();
        }

        private static void GetResult(object sender, Vacancy e)
        {
            Console.WriteLine("{0} - {1}", e.Name.Name, e.Name.Url);
            Console.WriteLine(e.VacancyAddress);
            if (e.CompensationUp == 0 && e.CompensationDown > 0)
            {
                Console.WriteLine("{0} {1} {2}", e.PrefixCompensation, e.CompensationDown, e.CurrencyCode);
            }
            if (e.CompensationDown == 0 && e.CompensationUp > 0)
            {
                Console.WriteLine("{0} {1} {2}", e.PrefixCompensation, e.CompensationUp, e.CurrencyCode);
            }
            if (e.CompensationDown > 0 && e.CompensationUp > 0)
            {
                Console.WriteLine("{0}-{1} {2}", e.CompensationUp, e.CompensationDown, e.CurrencyCode);
            }
            Console.WriteLine("{0} - {1}", e.Company.Name, e.Company.Url);
            Console.WriteLine(e.ShortDescription);
            Console.WriteLine("Date: {0}", e.Date.ToString());
            Console.WriteLine("-----------");
        }
    }
}
