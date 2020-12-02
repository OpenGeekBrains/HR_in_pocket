using System.ComponentModel.DataAnnotations;

namespace HRInPocket.ViewModels.MakeTask
{
    public class ShortFormCreateTaskViewModel
    {
        /// <summary>Желаемая должность</summary>
        [Display(Name = "Желаемая должность")]
        public string Position { get; set; }

        /// <summary>Ссылка на резюме</summary>
        [Display(Name = "Ссылка на резюме")]
        [DataType(DataType.Url)]
        public string ResumeUrl { get; set; }
    }
}