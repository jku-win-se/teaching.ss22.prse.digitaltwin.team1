using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CommonBase.Logic.Contracts
{
    public interface IGenericEntityManager<E> where E : EntityObject
    {
        Task Add(E entity);
        Task Delete(int id);
        Task Update(E entity);
        Task<E> GetBy(int id);
        Task<E[]> Get();
    }
}
