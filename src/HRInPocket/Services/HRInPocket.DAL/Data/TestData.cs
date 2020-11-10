using System;
using System.Collections.Generic;
using System.Linq;
using HRInPocket.DAL.Models.Entities;
using HRInPocket.DAL.Models.Users;
using static System.DateTime;

namespace HRInPocket.DAL.Data
{
    public static class TestData
    {
        #region  Private fields
        /// <summary>
        /// Минимально допустимая дата рождения
        /// </summary>
        private static readonly DateTime __MinDateTime = Convert.ToDateTime(Now.AddYears(-60));

        /// <summary>
        /// Максимально допустимая дата рождения
        /// </summary>
        private static readonly DateTime __MaxDateTime = Convert.ToDateTime(Now.AddYears(-18));

        private static readonly List<Address> __Addresses;
        private static readonly List<Speciality> __Specialties;
        private static readonly List<ActivityCategory> __ActivityCategories;
        private static readonly List<Applicant> __Applicants;
        private static readonly List<Resume> __Resumes;
        private static readonly List<Company> __Companies;
        private static readonly List<Vacancy> __Vacancies;
        private static readonly List<CompanyManager> __CompanyManagers;
        private static readonly List<Employer> __Employers;
        private static readonly List<SystemManager> __SystemManagers;
        private static readonly List<Tarif> __Tarifs;
        #endregion

        #region Properties
        public static List<Address> Addresses => __Addresses;
        public static List<Speciality> Specialties => __Specialties;
        public static List<ActivityCategory> ActivityCategories => __ActivityCategories;
        public static List<Applicant> Applicants => __Applicants;
        public static List<Resume> Resumes => __Resumes;
        public static List<Company> Companies => __Companies;
        public static List<Vacancy> Vacancies => __Vacancies;
        public static List<CompanyManager> CompanyManagers => __CompanyManagers;
        public static List<Employer> Employers => __Employers;
        public static List<SystemManager> SystemManagers => __SystemManagers;
        public static List<Tarif> Tarifs => __Tarifs;

        #endregion

        static TestData()
        {
            __Addresses = new List<Address>(
                Enumerable.Range(0, 700).Select(
                    Source => new Address
                    {
                        Country = $"Страна_{Source}", City = $"Город_{Source}", Street = $"Улица_{Source}", Building = $"Строение_{Source}"
                    }));

            __Specialties = new List<Speciality>(Enumerable.Range(0, 100).Select(Source => new Speciality {Name = $"Специальность_{Source}"}));

            __ActivityCategories = new List<ActivityCategory>(
                Enumerable.Range(0, 20).Select(
                    Source => new ActivityCategory {Name = $"Категория_{Source}", 
                        Specialties = Specialties.GetRange(Source * 4, 5)}));

            __Applicants = new List<Applicant>(
                Enumerable.Range(0, 500).Select(
                    Source => new Applicant
                    {
                        Name = $"Имя_{Source}",
                        LastName = $"Фамилия_{Source}",
                        Patronymic = $"Отчество_{Source}",
                        EmailAddress = $"Email_Соискателя_{Source}@.com",
                        Birthday = GenRandomDateTime(__MinDateTime, __MaxDateTime),
                        Address = Addresses[Source],
                        Speciality = new List<Speciality>(Specialties.GetRange(Source % (Specialties.Count - 5), Source % 4))
                    }));

            __Resumes = new List<Resume>(Enumerable.Range(0, Applicants.Count).Select(Source => new Resume {Applicant = Applicants[Source]}));

            __Vacancies = new List<Vacancy>(Enumerable.Range(0, 500).Select(Source => new Vacancy {Specialty = Specialties[Source % Specialties.Count]}));

            __Companies = new List<Company>(
                Enumerable.Range(0, 100).Select(
                    Source => new Company
                    {
                        Name = $"Название организации_{Source}",
                        FactAddress = Addresses[500 + Source * 2],
                        LegalAddress = Addresses[500 + Source * 2 + 1],
                        Inn = $"ИНН_{Source}",
                        Vacancies = new List<Vacancy>(Vacancies.GetRange(Source, 5))
                    }));

            

            __CompanyManagers = new List<CompanyManager>(Enumerable.Range(0, 150).Select(Source => new CompanyManager
            {
                Name = $"Имя_Менеджера_Работодателя_{Source}",
                LastName = $"Фамилия_Менеджера_Работодателя_{Source}",
                Patronymic = $"Отчество_Менеджера_Работодателя_{Source}",
                EmailAddress = $"Email_Менеджера_Работодателя_{Source}@.com",
            }));

            __Employers = new List<Employer>(
                Enumerable.Range(0, 50).Select(
                    Source => new Employer
                    {
                        Name = $"Имя_{Source}",
                        LastName = $"Фамилия_{Source}",
                        Patronymic = $"Отчество_{Source}",
                        EmailAddress = $"Email_Работодателья{Source}@.com",
                        Companies = new List<Company>(Companies.GetRange(Source, 2)),
                        CompanyManagers = new List<CompanyManager>(CompanyManagers.GetRange(Source, 5))
                    }));

            __SystemManagers = new List<SystemManager>(Enumerable.Range(0, 20).Select(Source => new SystemManager
            {
                Name = $"Имя_Менеджера_Системы_{Source}",
                LastName = $"Фамилия_Менеджера_Системы_{Source}",
                Patronymic = $"Отчество_Менеджера_Системы_{Source}",
                EmailAddress = $"Email_Менеджера_Системы_{Source}@.com",
                Applicants = new List<Applicant>(Applicants.GetRange(Source * 20, Source * 5)),
                Employers = new List<Employer>(Employers.GetRange(Source, Source % 3))
            }));

            __Tarifs = new List<Tarif>
            {
                new Tarif{Name = "Базовый", Visits = 1, Price = 1500, Description = "Хороший повод начать"},
                new Tarif{Name = "Средний", Visits = 3, Price = 4500, Description = "Набирайте обороты"},
                new Tarif{Name = "Эффективный", Visits = 5, Price = 6000, Description = "Выбирайте и сравнивайте разные предложения"},
                new Tarif{Name = "Профи", Visits = 10, Price = 10000, Description = "Обеспечьте гарантию трудоустройства"}
            };
        }

        #region Halp Methods

        /// <summary>
        /// Возвращает случайную дату в указанном диапазоне
        /// </summary>
        /// <param name="from">Начальная дата диапазона</param>
        /// <param name="to">Конечная дата диапазона</param>
        /// <param name="random"></param>
        /// <returns></returns>
        private static DateTime GenRandomDateTime(DateTime from, DateTime to, Random random = null)
        {
            if (from >= to)
            {
                throw new Exception("Параметр \"from\" должен быть меньше параметра \"to\"!");
            }
            random ??= new Random();
            var range = to - from;
            var randts = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            var result = from + randts;
            return result;
        }

        #endregion
    }
}
