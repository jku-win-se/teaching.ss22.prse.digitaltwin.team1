using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;
using System.Data;
using System.Data.Common;

namespace SmartRoom.TransDataService.Persistence
{
    public class TransDataDBContext : DbContext
    {
        public DbSet<MeasureState> MeasureStates => Set<MeasureState>();
        public DbSet<BinaryState> BinaryStates => Set<BinaryState>();
        public TransDataDBContext(DbContextOptions<TransDataDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasDiscriminator().IsComplete(false);
        }

        public List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {

            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }

        }
    }
}
