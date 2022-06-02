using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.TransDataService.Logic.Contracts;
using SmartRoom.TransDataService.Persistence;

namespace SmartRoom.TransDataService.Logic
{
    public class WriteManager : IWriteManager
    {
        private readonly IStateActions _stateActions;
        private readonly IDbContextFactory<TransDataDBContext> _dbContextFactory;
        private readonly IHubContext<SensorHub> _hub;

        public WriteManager(IDbContextFactory<TransDataDBContext> dbContextFactory, IHubContext<SensorHub> hub, IStateActions stateActions)
        {
            _stateActions = stateActions;
            _dbContextFactory = dbContextFactory;
            _hub = hub;
        }

        public async Task addState<E>(E[] states) where E : class, IState
        {

            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                context.AddRange(states);
                context.SaveChanges();
            }
            await NewStates(states);
            _stateActions.RunActions(states);
        }

        private async Task NewStates(IState[] states)
        {
            var distStates = states.DistinctBy(s => s.EntityRefID + s.Name);
            foreach (var s in distStates)
            {
                var latestState = states.Where(st => st.EntityRefID.Equals(s.EntityRefID) && st.Name.Equals(s.Name)).OrderBy(st => st.TimeStamp).Last();
                await _hub.Clients.All.SendAsync($"Sensor/{latestState.EntityRefID}/{latestState.Name}", latestState);
            }
        }
    }
}
