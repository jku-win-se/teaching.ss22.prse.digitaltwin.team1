using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.TransDataService.Persistence;

namespace SmartRoom.TransDataService.Logic
{
    public class WriteManager
    {
        private IDbContextFactory<TransDataDBContext> _dbContextFactory;
        public WriteManager(IDbContextFactory<TransDataDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task addState<E>(E[] state) where E : class, IState
        {
            await Task.Run(() =>
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    context.AddRange(state);
                    context.SaveChanges();
                }
            });
        } 
    }
}
