using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Persistence;
using SmartRoom.CommonBase.Persistence.Contracts;

namespace SmartRoom.CommonBase.Logic
{
    public class GenericEntityManager<E> : Contracts.IGenericEntityManager<E> where E : EntityObject
    {
        readonly IUnitOfWork _unitOfWork;
        public GenericEntityManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(E entity)
        {
            if (entity == null) throw new ArgumentNullException();
            await GetRepo().Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var del = await GetRepo().GetBy(id);
            if (del == null) return;

            await GetRepo().Delete(del);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(E entity)
        {
            if (entity == null) throw new ArgumentNullException();
            var toUpdate = await GetRepo().GetBy(entity.Id);
            if (toUpdate == null) return;
            Utils.GenericMapper.MapObjects(toUpdate, entity);
            await GetRepo().Update(toUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        public virtual async Task<E> GetBy(int id)
        {
            return await GetRepo().GetBy(id);
        }

        public virtual async Task<E[]> Get()
        {
            var dat = await GetRepo().Get();
            return dat;
        }

        private protected GenericEntityRepository<E> GetRepo()
        {
            return _unitOfWork.GetRepo<GenericEntityRepository<E>>()!;
        }

    }
}
