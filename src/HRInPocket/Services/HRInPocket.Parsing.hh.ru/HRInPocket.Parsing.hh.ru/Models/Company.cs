using HRInPocket.Parsing.hh.ru.Models.Base;

namespace HRInPocket.Parsing.hh.ru.Models.Entites
{
    /// <summary>Компания, размещающая вакансии</summary>
    public class Company:BaseUnit
    {
        /// <summary>URL адрес (ссылка) логотипа компании</summary>
        public string Logo { get; set; }
    }
}
