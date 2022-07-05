using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;
using System.Data.Common;

namespace SmartRoom.TransDataService.Persistence.Contracts
{
    public interface ITransDataDBContext
    {
        DbSet<BinaryState> BinaryStates { get; }
        DbSet<MeasureState> MeasureStates { get; }

        List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map);
    }
}