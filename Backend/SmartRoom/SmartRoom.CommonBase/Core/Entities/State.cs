using SmartRoom.CommonBase.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public abstract class State<T> : EntityObject, IState
    {
        public string Name { get; set; } = "isTriggerd";
        [Required]
        public Guid EntityRefID { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public T? Value { get; set; }
    }
}
