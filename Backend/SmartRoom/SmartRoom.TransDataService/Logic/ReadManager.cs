using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Persistence;

namespace SmartRoom.TransDataService.Logic
{
    public class ReadManager
    {
        private readonly IDbContextFactory<TransDataDBContext> _dbContextFactory;
        public ReadManager(IDbContextFactory<TransDataDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<E[]> GetStatesByEntityID<E>(Guid id) where E : class, IState
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return await context.Set<E>().Where(s => s.EntityRefID.Equals(id)).OrderByDescending(s => s.TimeStamp).Take(500).ToArrayAsync();
            }
        }

        public async Task<E> GetRecentStateByEntityID<E>(Guid id, string name) where E : class, IState
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                var data = context.Set<E>().Where(s => s.EntityRefID.Equals(id) && s.Name.Equals(name));

                return await data.Where(s => s.TimeStamp.Equals(data.Max(st => st.TimeStamp))).FirstAsync();
            }
        }

        public async Task<E[]> GetStatesByTimeSpan<E>(DateTime from, DateTime to) where E : class, IState
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return await context.Set<E>().Where(s => s.TimeStamp >= from && s.TimeStamp < to).OrderByDescending(s => s.TimeStamp).Take(500).ToArrayAsync();
            }
        }

        public async Task<string[]> GetStateTypesByEntityID<E>(Guid id) where E : class, IState
        {
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return context.Set<E>().Where(s => s.EntityRefID.Equals(id)).Select(d => d.Name).Distinct().ToArray();
            }
        }

        public async Task<object> GetChartData<E>(Guid id, string name, int intervall, int daySpan) where E : class, IState
        {
            intervall *= daySpan;

            if (typeof(E).Equals(typeof(MeasureState))) return await GetMeasureChartData(id, name, intervall, typeof(E).Name, daySpan);
            else if (typeof(E).Equals(typeof(BinaryState))) return await GetBinaryChartData(id, name, intervall, typeof(E).Name, daySpan);
            else return new();
        }
        public async Task<object> GetChartData<E>(Guid[] ids, string name, int intervall, int daySpan) where E : class, IState
        {
            intervall *= daySpan;

            if (typeof(E).Equals(typeof(BinaryState))) return await GetBinaryChartData(ids, name, intervall, typeof(E).Name, daySpan);
            else return new();
        }

        private async Task<object> GetBinaryChartData(Guid id, string name, int intervall, string type, int daySpan)
        {
            string stmd =
                $"SELECT \"Name\", \"EntityRefID\", time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min, bool_or(\"Value\") " +
                $"FROM public.\"{type}s\" " +
                $"WHERE \"TimeStamp\" > now() - interval '{daySpan} day' " +
                $"GROUP BY five_min, \"Name\", \"EntityRefID\" " +
                $"HAVING \"Name\" like '{name}' and \"EntityRefID\" = '{id}'" +
                $"ORDER BY five_min";

            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return context.RawSqlQuery(stmd,
                    d => new
                    {
                        TimeStamp = d[2],
                        Value = d[3]
                    });
            }
        }

        private async Task<object> GetBinaryChartData(Guid[] ids, string name, int intervall, string type, int daySpan)
        {
            string idStmdChain = "";

            foreach (var id in ids)
            {
                if(id == ids.Last()) idStmdChain += $"\"EntityRefID\" = '{id}'";
                else idStmdChain += $"\"EntityRefID\" = '{id}' or ";
            }

            string stmd =
                $"Select q.five_min_2, COALESCE(sum(a.count), 0) from " +
                    $"(SELECT time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min, Count(DISTINCT \"EntityRefID\") as count, bool_or(\"Value\") as val " +
                    $"FROM public.\"{type}s\" " +
                    $"WHERE \"TimeStamp\" > now() - interval '{daySpan} day' and \"Name\" like '{name}' and ({idStmdChain})" +
                    $"GROUP BY five_min, \"EntityRefID\" " +
                    $"HAVING bool_or(\"Value\") = true) AS a " +
                $"RIGHT OUTER JOIN " +
                    $"(SELECT time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min_2 " +
                    $"FROM public.\"{type}s\" WHERE \"TimeStamp\" > now() - interval '{daySpan} week' and \"Name\" like '{name}' and ({idStmdChain}) GROUP BY five_min_2) as q " +
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

        private async Task<object> GetMeasureChartData(Guid id, string name, int intervall, string type, int daySpan)
        {
            string stmd =
                $"SELECT \"Name\", \"EntityRefID\", time_bucket('{intervall} minutes', \"TimeStamp\") AS five_min, avg(\"Value\") " +
                $"FROM public.\"{type}s\" " +
                $"WHERE \"TimeStamp\" > now() - interval '{daySpan} day' " +
                $"GROUP BY five_min, \"Name\", \"EntityRefID\" " +
                $"HAVING \"Name\" like '{name}' and \"EntityRefID\" = '{id}'" +
                $"ORDER BY five_min";

            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                return context.RawSqlQuery(stmd,
                    d => new
                    {
                        TimeStamp = d[2],
                        Value = d[3]
                    });
            }
        }
    }
}
