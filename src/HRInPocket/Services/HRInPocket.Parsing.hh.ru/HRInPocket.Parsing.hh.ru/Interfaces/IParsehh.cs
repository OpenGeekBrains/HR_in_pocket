using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using HRInPocket.Parsing.hh.ru.Models;
using HRInPocket.Parsing.hh.ru.Service;

namespace HRInPocket.Parsing.hh.ru.Interfaces
{
    /// <summary> Парсер обрабатывает веб-страницы на получение содержащихся на ней вакансий </summary>
    public interface IParsehh
    {
        /// <summary> Событие передающее полученную вакансию </summary>
        event EventHandler<VacancyEventArgs> SendVacancy;

        /// <summary> Парсит страницу и возвращает вакансию через событие VacancyEventArgs </summary>
        /// <param name="token"> Токен остановки парсера </param>
        /// <param name="page"> Страницу, которую необходимо парсить </param>
        /// <param name="GetParameters"> Ключевые слова для поиска конкретных вакансий</param>
        /// <returns></returns>
        Task ParseAsync(CancellationToken token, string page, string GetParameters);

        /// <summary> Парсит определенную страницу на наличие вакансий </summary>
        /// <param name="token">Токен отмены парсинга</param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<(Vacancy[] Vacancies, string NextPage)> ParseAsync(CancellationToken token, string page);

        /// <summary> Парсит страницу и возвращает вакансию в IAsyncEnumerable </summary>
        /// <param name="token"> Токен остановки парсера </param>
        /// <param name="page"> Страница, которую необходимо парсить </param>
        /// <param name="GetParameters"> Ключевые слова для поиска конкретных вакансий</param>
        /// <returns> Вакансии в IAsyncEnumerable </returns>
        IAsyncEnumerable<Vacancy> ParseEnumerableAsync(CancellationToken token, string page);
    }
}
