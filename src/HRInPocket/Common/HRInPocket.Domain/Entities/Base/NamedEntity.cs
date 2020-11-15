
namespace HRInPocket.Domain.Entities.Base
{
    /// <summary>
    /// Именованная сущность
    /// </summary>
    public abstract class NamedEntity : BaseEntity
    {
        /// <summary>
        /// Имя / Название
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}