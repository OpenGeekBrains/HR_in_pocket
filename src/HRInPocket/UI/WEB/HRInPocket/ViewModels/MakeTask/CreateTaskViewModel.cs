using System.ComponentModel.DataAnnotations;

namespace HRInPocket.ViewModels.MakeTask
{
    public class CreateTaskViewModel
    {
        /// <summary>Удалённая работа</summary>
        [Display(Name = "Удалённая работа")]
        public bool RemoteWork { get; set; }

        /// <summary>Желаемая должность</summary>
        [Display(Name = "Желаемая должность")]
        public string Position { get; set; } = "HR менеджер";

        /// <summary>Зарплата от</summary>
        [Display(Name = "Зарплата от")]
        public decimal? Salary { get; set; }

        /// <summary>По договорённости</summary>
        [Display(Name = "По договорённости")]
        public bool SalaryByAgreement { get; set; }

        /// <summary>Ключевые слова</summary>
        [Display(Name = "Ключевые слова")]
        public string Tags { get; set; }

        /// <summary>Ссылка на резюме</summary>
        [Display(Name = "Ссылка на резюме")]
        [DataType(DataType.Url)]
        public string ResumeUrl { get; set; }

        /// <summary>Разрешение на обработку персональны данных</summary>
        [Display(Name = "Разрешение на обработку персональны данных")]
        public bool PermissionToProcessPersonalData { get; set; }
    }
}
