using System;

using AutoMapper;

namespace HRInPocket.Interfaces.Services
{
    public abstract class BaseMapper<T, DTO>
    {
        protected DTO ToDTO(T entity, Action<IMapperConfigurationExpression> action) =>
            new Mapper(new MapperConfiguration(action)).Map<DTO>(entity);

        protected T FromDTO(DTO entity, Action<IMapperConfigurationExpression> action) =>
            new Mapper(new MapperConfiguration(action)).Map<T>(entity);
    }
}
