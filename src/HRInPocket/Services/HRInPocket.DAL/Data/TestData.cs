using System;
using System.Collections.Generic;
using System.Linq;

using HRInPocket.Domain.Entities.Data;
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

        /// <summary>
        /// Просто статический рандом для генерации
        /// </summary>
        private static readonly Random __Random = new Random();
        #endregion

        #region Properties
        public static List<Address> Addresses { get; }
        public static List<Speciality> Specialties { get; }
        public static List<ActivityCategory> ActivityCategories { get; }
        public static List<TargetTask> TargetTasks { get; }

        public static List<Tarif> Tarifs { get; }
        public static List<PriceItem> Price { get; }


        #region NotFilled

        //public static List<Vacancy> Vacancies { get; }
        //public static List<CoverLetter> CoverLetters { get; } 
        #endregion


        #region Identity
        //public static List<Profile> Profiles { get; }
        //public static List<User> Users { get; }
        //public static List<Applicant> Applicants { get; }
        //public static List<SystemManager> SystemManagers { get; }
        //public static List<Company> Companies { get; } 
        #endregion

        #region Behave on Identity Entities
        //public static List<Resume> Resumes { get; } 
        #endregion
        #endregion
        static TestData()
        {

            #region Tarifs
            Tarifs = new List<Tarif>
            {
                new Tarif{Name = "Базовый", Visits = 1, Price = 1500, Description = "Хороший повод начать"},
                new Tarif{Name = "Средний", Visits = 3, Price = 4500, Description = "Набирайте обороты"},
                new Tarif{Name = "Эффективный", Visits = 5, Price = 6000, Description = "Выбирайте и сравнивайте разные предложения"},
                new Tarif{Name = "Профи", Visits = 10, Price = 10000, Description = "Обеспечьте гарантию трудоустройства"}
            };
            #endregion

            #region Price
            Price = new List<PriceItem>
            {
                new PriceItem{Name = "Составление резюме", Price = 2000},
                new PriceItem{Name = "Написание сопроводительного письма", Price = 800},
                new PriceItem{Name = "Перевод резюме", Price = 1500}
            };
            #endregion


            #region Not Filled

            #region Addresses
            Addresses = Enumerable.Range(0, 700).Select(
                        source => new Address
                        {
                            Country = $"Страна_{source}",
                            City = $"Город_{source}",
                            Street = $"Улица_{source}",
                            Building = $"Строение_{source}"
                        }).ToList();
            #endregion

            #region ActivityCategories
            ActivityCategories = Enumerable.Range(0, 20)
                .Select(source => new ActivityCategory
                {
                    Name = $"Категория_{source}", 
                }).ToList();
            #endregion

            #region Specialties
            Specialties = Enumerable.Range(0, 100).Select(source => new Speciality
            {
                Name = $"Специальность_{source}",
                ActivityCategory = ActivityCategories[__Random.Next(0, ActivityCategories.Count-1)]
                
            }).ToList();
            #endregion


            #region TargetTasks
            //TargetTasks = Enumerable.Range(0, 50).Select(
            //        source => new TargetTask
            //        {
            //            Address = Addresses[__Random.Next(0, Addresses.Count - 1)],
            //            Speciality = Specialties[__Random.Next(0, Specialties.Count - 1)],
            //            CoverLetterLink = $"Ссылка на сопроводительное письмо {source}",
            //            ResumeLink = $"Ссылка на резюме {source}",
            //            Salary = __Random.Next(30, 301) * 1000,
            //            CreateCoverLetter = true,
            //            CreateResume = true,
            //            Tags = string.Empty,
            //            RemoteWork = __Random.Next(0, 100) % 2 == 0,
                        
            //        }).ToList();
            #endregion

            #endregion


            #region OnRework for Identity

            #region CompanyManagers
            //CompanyManagers = new List<CompanyManager>(Enumerable.Range(0, 150).Select(source => new CompanyManager
            //{
            //    Name = $"Имя_Менеджера_Работодателя_{source}",
            //    LastName = $"Фамилия_Менеджера_Работодателя_{source}",
            //    Patronymic = $"Отчество_Менеджера_Работодателя_{source}",
            //    EmailAddress = $"Email_Менеджера_Работодателя_{source}@.com",
            //}));
            #endregion

            #region Applicants
            //Applicants = new List<Applicant>(
            //    Enumerable.Range(0, 500).Select(
            //        source => new Applicant
            //        {
            //            Birthday = GenRandomDateTime(__MinDateTime, __MaxDateTime, __Random),
            //            Address = Addresses[source],
            //            Speciality = Specialties.GetRange(source % (Specialties.Count - 5), source % 4),
            //            Tarif = Tarifs[__Random.Next(0, Tarifs.Count - 1)],
            //            TargetTask = (source % 10 == 0) ? TargetTasks[source / 10] : null
            //        }));
            #endregion

            #region Employers
            //Employers = new List<Employer>(
            //        Enumerable.Range(0, 50).Select(
            //            source => new Employer
            //            {
            //                //Name = $"Имя_{source}",
            //                //LastName = $"Фамилия_{source}",
            //                //Patronymic = $"Отчество_{source}",
            //                //EmailAddress = $"Email_Работодателья{source}@.com",
            //                Companies = Companies.GetRange(source, 2),
            //                CompanyManagers = CompanyManagers.GetRange(source, 5)
            //            }));
            #endregion

            #region SystemManagers
            //SystemManagers = new List<SystemManager>(Enumerable.Range(0, 20).Select(source => new SystemManager
            //{
            //    Name = $"Имя_Менеджера_Системы_{source}",
            //    LastName = $"Фамилия_Менеджера_Системы_{source}",
            //    Patronymic = $"Отчество_Менеджера_Системы_{source}",
            //    EmailAddress = $"Email_Менеджера_Системы_{source}@.com",
            //    Applicants = Applicants.GetRange(source * 20, source * 5),
            //    Employers = Employers.GetRange(source, source % 3)
            //}));
            #endregion

            #endregion

            #region In Need Of Identity Entities

            #region Resumes
            //Resumes = new List<Resume>(Enumerable.Range(0, Applicants.Count)
            //   .Select(source => new Resume { Applicant = Applicants[source] }));
            #endregion 

            #endregion
        }

        #region Methods

        /// <summary>
        /// Возвращает случайную дату в указанном диапазоне
        /// </summary>
        /// <param name="from">Начальная дата диапазона</param>
        /// <param name="to">Конечная дата диапазона</param>
        /// <param name="random"></param>
        /// <returns></returns>
        private static DateTime GenRandomDateTime(DateTime from, DateTime to, Random random)
        {
            if (from >= to)
            {
                throw new ArgumentOutOfRangeException(nameof(from), "Параметр \"from\" должен быть меньше параметра \"to\"!");
            }
            var range = to - from;
            var randts = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            var result = from + randts;
            return result;
        }

        #endregion
    }
}
