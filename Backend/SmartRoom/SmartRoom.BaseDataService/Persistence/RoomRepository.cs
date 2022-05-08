using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Persistence;

namespace SmartRoom.BaseDataService.Persistence
{
    public class RoomRepository : GenericEntityRepository<Room>
    {
        public RoomRepository(SmartRoomDBContext context) : base(context)
        {
        }

        protected override IQueryable<Room> GetDataWithIncludes()
        {
            return base.GetDataWithIncludes().Include(r => r.RoomEquipment);
        }
    }
}
