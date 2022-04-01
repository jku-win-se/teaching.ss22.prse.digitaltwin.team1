using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Persistence;

namespace SmartRoom.BaseDataService.Persistence
{
    public class SmartRoomUOW : UnitOfWork
    {
        public SmartRoomUOW(SmartRoomDBContext context) : base(context)
        {

        }
    }
}
