namespace SmartRoom.CommonBase.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
        R? GetRepo<R>();
    }
}
