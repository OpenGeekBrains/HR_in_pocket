using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HRInPocket.HHApi.Models.Hints;

namespace HRInPocket.HHApi.Interfaces
{
    public interface IHintServices
    {
        /// <summary>
        /// Подсказки по названиям университетов
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <param name="locale">Язык подсказки. Необязательный параметр (стандартно используется RU)</param>
        /// <returns></returns>
        public Task<Hints<Education>> EducationalHint(string text, SuggestsLocale? locale = null);


        /// <summary>
        /// Подсказки по организациям
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <param name="locale">Язык подсказки. Необязательный параметр (стандартно используется RU)</param>
        /// <returns></returns>
        public Task<Hints<Companies>> CompaniesHint(string text, SuggestsLocale? locale = null);


        /// <summary>
        /// Подсказки по специализациям
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <param name="locale">Язык подсказки. Необязательный параметр (стандартно используется RU)</param>
        /// <returns></returns>
        public Task<Hints<Specialization>> SpecializationHint(string text, SuggestsLocale? locale = null);


        /// <summary>
        /// Подсказки по ключевым навыкам
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <returns></returns>
        public Task<Hints<Skill>> SkillsHint(string text);


        /// <summary>
        /// Подсказки по должностям
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <returns></returns>
        public Task<Hints<Position>> PositionHint(string text);


        /// <summary>
        /// Подсказки по регионам. Подсказка по всем регионам.
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <param name="locale">Язык подсказки. Необязательный параметр (стандартно используется RU)</param>
        /// <returns></returns>
        public Task<Hints<AreaHint>> AreasHint(string text, SuggestsLocale? locale = null);


        /// <summary>
        /// Подсказки по регионам. Подсказка по всем регионам, являющимися листами в дереве регионов.
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <param name="locale">Язык подсказки. Необязательный параметр (стандартно используется RU)</param>
        /// <returns></returns>
        public Task<Hints<AreaHint>> AreasLeavesHint(string text, SuggestsLocale? locale = null);


        /// <summary>
        /// Подсказки по ключевым словам поиска вакансий
        /// </summary>
        /// <param name="text">Текст для поиска (кол-во символов от 3 до 3000)</param>
        /// <returns></returns>
        public Task<Hints<Vacancy>> VacancyHint(string text);
    }
}
