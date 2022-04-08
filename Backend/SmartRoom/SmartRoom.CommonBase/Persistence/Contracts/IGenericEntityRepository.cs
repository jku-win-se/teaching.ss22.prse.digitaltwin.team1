namespace SmartRoom.CommonBase.Persistence.Contracts
{
    public interface IGenericEntityRepository<E> where E : Core.Entities.EntityObject
    {
        Task Add(E entity);

        Task Update(E entity);

        Task Delete(E entity);

        Task<E[]> Get();

        Task<E> GetBy(int id);
    }
}
