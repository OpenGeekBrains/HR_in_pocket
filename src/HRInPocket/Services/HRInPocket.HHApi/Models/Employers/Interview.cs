namespace HRInPocket.HHApi.Models.Employers
{
    public class Interview
    {
        /// <summary>
        /// Идентификатор интервью
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Заголовок интервью
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Адрес страницы, содержащей интервью
        /// </summary>
        public string url { get; set; }
    }
}
