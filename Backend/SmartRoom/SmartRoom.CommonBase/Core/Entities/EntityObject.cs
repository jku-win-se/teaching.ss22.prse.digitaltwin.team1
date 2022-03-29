using System.ComponentModel.DataAnnotations;

namespace SmartRoom.CommonBase.Core.Entities
{
    public abstract class EntityObject
    {
        [Key]
        public int Id { get; set; }
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
