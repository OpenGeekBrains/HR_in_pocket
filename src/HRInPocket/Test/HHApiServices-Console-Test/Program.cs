using System;
using HRInPocket.HHApi.Models.Employers;
using HRInPocket.HHApi.Services;

namespace HHApiServices_Console_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new EmployersService();

            var fOption = new FindOption() { only_with_vacancies = "true" };
            var pOption = new PageOption() { page = 0, per_page = 1 };

            var result = service.FindEmplouer(pageOption: pOption).Result;

            foreach (var item in result.items)
            {
                Console.WriteLine(item.name);

                Console.WriteLine(item.url);
                if (item.logo_urls != null)
                {
                    foreach (var logo in item.logo_urls)
                    {
                        Console.WriteLine(logo.Key + " : " + logo.Value);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            var employer = service.GetEmployerInfoById(1455).Result;
            Console.WriteLine($"Name: {employer.name}");
            Console.WriteLine($"Site URL: {employer.site_url}");
            Console.WriteLine($"Branded Description: {employer.branded_description}");
            Console.WriteLine();
            foreach (var item in employer.relations)
            {
                Console.WriteLine($"Relation: {item}");
            }

            Console.ReadLine();
        }
    }
}
