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
            parse.Result += getResult;
            parse.Parse();
        }

        private static void getResult(Vacancy obj)
        {
            Console.WriteLine("{0} - {1}", obj.Name.Name, obj.Name.Url);
            Console.WriteLine(obj.VacancyAddress);
            if (obj.CompensationUp == 0 && obj.CompensationDown>0)
            {
                Console.WriteLine("{0} {1} {2}", obj.PrefixCompensation, obj.CompensationDown, obj.CurrencyCode);
            }
            if (obj.CompensationDown == 0 && obj.CompensationUp>0)
            {
                Console.WriteLine("{0} {1} {2}", obj.PrefixCompensation, obj.CompensationUp, obj.CurrencyCode);
            }
            if (obj.CompensationDown > 0 && obj.CompensationUp > 0)
            {
                Console.WriteLine("{0}-{1} {2}", obj.CompensationUp, obj.CompensationDown, obj.CurrencyCode);
            }
            Console.WriteLine("{0} - {1}", obj.Company.Name, obj.Company.Url);
            Console.WriteLine(obj.ShortDescription);
            Console.WriteLine("Date: {0}", obj.Date.ToString());
            Console.WriteLine("-----------");
        }
    }
}
