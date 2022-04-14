using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Persistence;
using SmartRoom.CommonBase.Persistence.Contracts;

namespace SmartRoom.BaseDataService.Persistence
{
    public class RoomRepository : GenericEntityRepository<Room> , IGenericEntityRepository<Room>
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
