using System.ComponentModel.DataAnnotations;

namespace HRInPocket.Domain.DTO.Base
{
    /// <summary>
    /// Именованная сущность
    /// </summary>
    public abstract class NamedDTO : BaseDTO
    {
        /// <summary>
        /// Имя / Название </summary>
        [Required]
        public string Name { get; set; }
    }
}
