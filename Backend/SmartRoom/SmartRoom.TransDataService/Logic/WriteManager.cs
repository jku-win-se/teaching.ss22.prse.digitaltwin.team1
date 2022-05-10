using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.TransDataService.Persistence;

namespace SmartRoom.TransDataService.Logic
{
    public class WriteManager
    {
        private readonly IDbContextFactory<TransDataDBContext> _dbContextFactory;
        private readonly IHubContext<SensorHub> _hub;

        public WriteManager(IDbContextFactory<TransDataDBContext> dbContextFactory, IHubContext<SensorHub> hub)
        {
            _dbContextFactory = dbContextFactory;
            _hub = hub;
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
            await _hub.Clients.All.SendAsync("SensorData", state.Last());
        } 
    }
}
