using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Persistence;

namespace SmartRoom.TransDataService.Logic
{
    public class ReadManager
    {
        private IDbContextFactory<TransDataDBContext> _dbContextFactory;
        public ReadManager(IDbContextFactory<TransDataDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<E[]> GetStatesByEntityID<E>(Guid id) where E : State
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return await context.Set<E>().Where(s => s.EntityRefID.Equals(id)).ToArrayAsync();
            }
        }

        public async Task<E> GetRecentStateByEntityID<E>(Guid id, string name) where E : State
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                var data = context.Set<E>().Where(s => s.EntityRefID.Equals(id) && s.Name.Equals(name));
                
                return await data.Where(s => s.TimeStamp.Equals(data.Max(st => st.TimeStamp))).FirstAsync();
            }
        }

        public async Task<E[]> GetStatesByTimeSpan<E>(DateTime from, DateTime to) where E : State
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return await context.Set<E>().Where(s => s.TimeStamp >= from && s.TimeStamp < to).ToArrayAsync();
            }
        }
    }
}
