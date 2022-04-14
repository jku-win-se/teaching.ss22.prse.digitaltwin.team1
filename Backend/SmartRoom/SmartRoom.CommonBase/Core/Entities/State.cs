using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public abstract class State : EntityObject
    {
        public string Name { get; set; } = "isTriggerd";
        [Required]
        public Guid EntityRefID { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
