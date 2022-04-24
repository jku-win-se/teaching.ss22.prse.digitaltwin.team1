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
            if (typeof(E).Equals(typeof(MeasureState))) return await GetMeasureChartData(id, name, intervall, typeof(E).Name);
            else if (typeof(E).Equals(typeof(BinaryState))) return await GetBinaryChartData(id, name, intervall, typeof(E).Name);
            else return new();
        }
        public async Task<object> GetChartData<E>(Guid[] ids, string name, int intervall) where E : State
        {
            if (typeof(E).Equals(typeof(BinaryState))) return await GetBinaryChartData(ids, name, intervall, typeof(E).Name);
            else return new();
        }

        private async Task<object> GetBinaryChartData(Guid id, string name, int intervall, string type)
        {
            string stmd =
                $"SELECT \"Discriminator\", \"Name\", \"EntityRefID\", time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min, bool_or(\"BinaryValue\") " +
                $"FROM public.\"State\" " +
                $"GROUP BY five_min, \"Discriminator\", \"Name\", \"EntityRefID\" " +
                $"HAVING \"Discriminator\" like '{type}' and \"Name\" like '{name}' and \"EntityRefID\" = '{id}'" +
                $"ORDER BY five_min";

            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return context.RawSqlQuery(stmd,
                    d => new
                    {
                        TimeStamp = d[3],
                        Value = d[4]
                    });
            }
        }

        private async Task<object> GetBinaryChartData(Guid[] ids, string name, int intervall, string type)
        {
            string idStmdChain = "";

            foreach (var id in ids)
            {
                if(id == ids.Last()) idStmdChain += $"\"EntityRefID\" = '{id}'";
                else idStmdChain += $"\"EntityRefID\" = '{id}' or ";
            }

            string stmd =
                $"Select q.five_min_2, COALESCE(sum(a.count), 0) from " +
                    $"(SELECT time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min, Count(DISTINCT \"EntityRefID\") as count, bool_or(\"BinaryValue\") as val " +
                    $"FROM public.\"State\" " +
                    $"WHERE \"Discriminator\" like '{type}' and \"Name\" like '{name}' and ({idStmdChain})" +
                    $"GROUP BY five_min, \"EntityRefID\" " +
                    $"HAVING bool_or(\"BinaryValue\") = true) AS a " +
                $"RIGHT OUTER JOIN " +
                    $"(SELECT time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min_2 " +
                    $"FROM public.\"State\" WHERE \"Discriminator\" like '{type}' and \"Name\" like '{name}' and ({idStmdChain}) GROUP BY five_min_2) as q " +
                $"ON q.five_min_2 = a.five_min " +
                $"Group By q.five_min_2 " +
                $"ORDER BY q.five_min_2";

            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                var res = context.RawSqlQuery(stmd,
                    d => new
                    {
                        res1 = d[0],
                        res2 = d[1]
                    });

                return res;
            }
        }

        private async Task<object> GetMeasureChartData(Guid id, string name, int intervall, string type)
        {
            string stmd =
                $"SELECT \"Discriminator\", \"Name\", \"EntityRefID\", time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min, avg(\"MeasureValue\") " +
                $"FROM public.\"State\" " +
                $"GROUP BY five_min, \"Discriminator\", \"Name\", \"EntityRefID\" " +
                $"HAVING \"Discriminator\" like '{type}' and \"Name\" like '{name}' and \"EntityRefID\" = '{id}'" +
                $"ORDER BY five_min";

            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return context.RawSqlQuery(stmd,
                    d => new
                    {
                        TimeStamp = d[3],
                        Value = d[4]
                    });
            }
        }
    }
}
