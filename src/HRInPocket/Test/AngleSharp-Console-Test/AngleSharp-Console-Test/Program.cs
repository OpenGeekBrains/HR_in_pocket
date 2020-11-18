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

        private static void GetResult(object sender, VacancyEventArgs e)
        {
            var vacancy = e.Vacancy;
            Console.WriteLine("{0} - {1}", vacancy.Name.Name, vacancy.Name.Url);
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
