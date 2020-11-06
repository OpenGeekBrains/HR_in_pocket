
namespace HRInPocket.DAL.Models.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор ID
        /// </summary>
        public long Id { get; set; }
    }
}