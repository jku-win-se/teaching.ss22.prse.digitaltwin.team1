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

        public async Task<BinaryState[]> GetStatesByEntityID(Guid id)
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return await context.BinaryStates.Where(s => s.EntityRefID.Equals(id)).ToArrayAsync();
            }
        }

        public async Task<State[]> GetStatesByTimeSpan(DateTime from, DateTime to)
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return await context.BinaryStates.Where(s => s.TimeStamp >= from && s.TimeStamp < to).ToArrayAsync();
            }
        }
    }
}
