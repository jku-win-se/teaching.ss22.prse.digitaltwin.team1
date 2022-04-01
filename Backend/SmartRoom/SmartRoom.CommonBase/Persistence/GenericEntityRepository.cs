using Microsoft.EntityFrameworkCore;

namespace SmartRoom.CommonBase.Persistence
{
    public class GenericEntityRepository<E> where E : Core.Entities.EntityObject
    {
        private protected readonly DbContext _context;
        public GenericEntityRepository(DbContext context)
        {
            _context = context;
        }

        public async Task Add(E entity)
        {
            await _context.Set<E>().AddAsync(entity);
        }

        public async Task Update(E entity)
        {
            await Task.Run(() => _context.Set<E>().Update(entity));
        }

        public async Task Delete(E entity)
        {
            await Task.Run(() => _context.Set<E>().Remove(entity));
        }

        public virtual async Task<E[]> Get()
        {
            return await GetDataWithIncludes().ToArrayAsync();
        }

        public virtual async Task<E> GetBy(int id)
        {
            return await GetDataWithIncludes().Where(e => e.Id == id).FirstAsync();
        }

        protected virtual IQueryable<E> GetDataWithIncludes()
        {
            return _context.Set<E>();
        }
    }
}
