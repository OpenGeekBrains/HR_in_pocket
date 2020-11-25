namespace HRInPocket.Interfaces.Services
{
    public interface IMapper<TEntity, DTO>
    {
        DTO Map(TEntity entity);
        TEntity Map(DTO entity);
    }
}