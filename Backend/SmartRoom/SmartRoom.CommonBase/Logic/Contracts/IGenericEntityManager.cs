using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CommonBase.Logic.Contracts
{
    public interface IGenericEntityManager<E> where E : EntityObject
    {
        Task Add(E entity);
        Task Delete(Guid id);
        Task Update(E entity);
        Task<E> GetBy(Guid id);
        Task<E[]> Get();
    }
}
