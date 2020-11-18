using System.ComponentModel.DataAnnotations.Schema;
using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Задание
    /// </summary>
    public class TargetTask : BaseEntity
    {
        // пока добавил адрес. При работе с API - там можно будет выбирать из списка городов/районов и т.д.
        /// <summary>
        /// Адрес
        /// </summary>
        public Address Address { get; set; } 
        //----------------------------------------------

        /// <summary>
        /// Удаленная работа
        /// </summary>
        public bool RemoteWork { get; set; }

        /// <summary>
        /// Должность/специальность/профессия
        /// </summary>
        public Speciality Speciality { get; set; }

        // todo: Сделать свойство опциональным
        /// <summary>
        /// Желаемый оклад
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Сделать резюме
        /// </summary>
        public bool CreateResume { get; set; }

        /// <summary>
        /// Сделать сопроводительное письмо
        /// </summary>
        public bool CreateCoverLetter { get; set; }

        /// <summary>
        /// Ссылка на резюме
        /// </summary>
        public string ResumeLink { get; set; }

        /// <summary>
        /// Ссылка на сопроводительное письмо
        /// </summary>
        public string CoverLetterLink { get; set; }

        /// <summary>
        /// Внешний ключ
        /// </summary>
        public string ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
