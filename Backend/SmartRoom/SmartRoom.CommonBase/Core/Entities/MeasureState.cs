using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public class MeasureState : State
    {
        [Required]
        public double MeasureValue { get; set; }
    }
}
