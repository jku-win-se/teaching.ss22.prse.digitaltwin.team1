using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Persistence.Contracts;

namespace SmartRoom.CommonBase.Persistence
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private bool _disposedValue;
        public readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public R? GetRepo<R>()
        {
            return (R?)this.GetType().GetProperties().FirstOrDefault(p => p.PropertyType.Equals(typeof(R)))?.GetValue(this);
        }
    }
}
