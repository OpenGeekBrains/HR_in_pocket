
namespace HRInPocket.DAL.Models.Base
{
    /// <summary>
    /// Именованная сущность
    /// </summary>
    public abstract class NamedEntity : BaseEntity
    {
        /// <summary>
        /// Имя / Название
        /// </summary>
        public string Name { get; set; }
    }
}