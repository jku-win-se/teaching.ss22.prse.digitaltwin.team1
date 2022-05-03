using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Contracts
{
    public interface IState
    {
        public string Name { get; set; }
        [Required]
        public Guid EntityRefID { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
