using SmartRoom.CommonBase.Core.Entities;
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
            try
            {
                await GetRepo().Add(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            var del = await GetRepo().GetBy(id);
            if (del == null) throw new KeyNotFoundException();
            try
            {
                await GetRepo().Delete(del);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(E entity)
        {
            if (entity == null) throw new ArgumentNullException();
            var toUpdate = await GetRepo().GetBy(entity.Id);
            if (toUpdate == null) throw new KeyNotFoundException();
            
            try
            {
                Utils.GenericMapper.MapObjects(toUpdate, entity);
                await GetRepo().Update(toUpdate);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<E> GetBy(Guid id)
        {
            return await GetRepo().GetBy(id);
        }

        public async Task<E[]> Get()
        {
            return await GetRepo().Get();
        }

        private protected IGenericEntityRepository<E> GetRepo()
        {
            return _unitOfWork.GetRepo<IGenericEntityRepository<E>>()!;
        }

    }
}
