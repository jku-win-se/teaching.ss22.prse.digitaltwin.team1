using Microsoft.AspNetCore.Mvc;
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

        public async Task<string[]> GetStateTypesByEntityID<E>(Guid id) where E : State
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return context.Set<E>().Where(s => s.EntityRefID.Equals(id)).Select(d => d.Name).Distinct().ToArray();
            }
        }

        public async Task<object> GetChartData<E>(Guid id, string name, int intervall) where E : State
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return context.RawSqlQuery($"SELECT \"Discriminator\", \"Name\", \"EntityRefID\", time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min, avg(\"MeasureValue\") FROM public.\"State\" GROUP BY five_min, \"Discriminator\", \"Name\", \"EntityRefID\" HAVING \"Discriminator\" like '{typeof(E).Name}' and \"Name\" like '{name}'",
                    d => new 
                    { 
                        TimeStamp = d[3],
                        Value = d[4]
                    });
            }
        }
    }
}
