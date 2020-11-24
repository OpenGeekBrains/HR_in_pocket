using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using HRInPocket.Parsing.hh.ru.Models.Entites;

namespace HRInPocket.WPF.Data
{
    static class TestData
    {
        public static ObservableCollection<Vacancy> GetTestData()
        {
            var testData = new ObservableCollection<Vacancy>();

            for (int i = 0; i < 10; i++)
            {
                testData.Add(new Vacancy()
                {
                    Name = new VacancyName()
                    {
                        Name = "VacancyName " + i,
                        Url = "VacancyUrl " + i
                    },
                    Company = new Company()
                    {
                        Name = "CompanyName " + i,
                        Url = "CompanyUrl " + i
                    },
                    VacancyAddress = "VacancyAddres " + i,
                    Description = "VacancyDescription" + i,
                    ShortDescription = "VacancyShortDescription " + i,
                    CompensationUp = (ulong)i * 1000,
                    CompensationDown = (ulong)i * 2000,
                    CurrencyCode = "USD",
                    Date = DateTime.Now
                });
            }

            return testData;
        }
    }
}
