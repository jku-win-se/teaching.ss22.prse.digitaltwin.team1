using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public class BinaryState : State
    {
        [Required]
        public bool BinaryValue { get; set; }
    }
}
