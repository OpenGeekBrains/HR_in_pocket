using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using HRInPocket.Parsing.hh.ru.Models.Entites;
using HRInPocket.Parsing.hh.ru.Service;

namespace HRInPocket.Parsing.hh.ru.Interfaces
{
    /// <summary> Парсер загружает страницу по адресу https://hh.ru/search/vacancy </summary>
    public interface IParsehh
    {
        /// <summary> Событие передающее полученную вакансию </summary>
        event EventHandler<VacancyEventArgs> Result;

        /// <summary> Парсит https://hh.ru/search/vacancy и возвращает значения по готовности через событие Result </summary>
        /// <param name="GetParameters">
        /// Если нужно задать точные параметры поиска, передайте их в свойстве GetParameters
        /// в формате "?param1=value&param2=value&...&paramN=value"
        /// </param>
        Task ParseAsync(CancellationToken token, string page, string GetParameters);

        IAsyncEnumerable<Vacancy> ParseEnumerableAsync(CancellationToken token, string page, string GetParameters);
    }
}
