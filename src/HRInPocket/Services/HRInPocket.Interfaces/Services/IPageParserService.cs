using System.Threading.Tasks;

namespace HRInPocket.Interfaces.Services
{
    /// <summary>
    /// Сервис скачивания и парсинга страниц сайтов
    /// </summary>
    public interface IPageParserService
    {
        // Надо подумать

       
        Task AddUrl();
        Task RemoveUrl();

        /// <summary>
        /// Загрузить страницы с сайта
        /// </summary>
        Task DownloadPages();

        /// <summary>
        /// Парсить страницы
        /// </summary>
        Task ParsePages();

        /// <summary>
        /// Сформировать данные о вакансиях
        /// </summary>
        Task Vacancies();
    }
}