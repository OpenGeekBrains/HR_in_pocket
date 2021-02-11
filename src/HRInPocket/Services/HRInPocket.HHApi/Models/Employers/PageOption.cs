namespace HRInPocket.HHApi.Models.Employers
{
    /// <summary>
    /// Работает ограничение: глубина возвращаемых результатов не может быть больше 2000.
    /// </summary>
    public class PageOption
    {
        /// <summary>
        /// Номер страницы с работодателями (считается от 0, по умолчанию - 0)
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int per_page { get; set; }
    }
}
