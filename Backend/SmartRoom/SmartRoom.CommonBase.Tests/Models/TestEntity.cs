using System;

namespace SmartRoom.CommonBase.Tests.Models
{
    public class TestEntity : Core.Entities.EntityObject, IComparable<TestEntity>
    {
        public DateTime TestDate { get; set; }
        public string TestString { get; set; } = string.Empty;
        public int TestInt { get; set; }
        public bool TestBool { get; set; }
        public double TestDouble { get; set; }

        public int CompareTo(TestEntity? other)
        {
            if(GetHashCode().Equals(other?.GetHashCode())) return 0;
            else return 1;
        }

        public override bool Equals(object? obj)
        {
            if(obj is not TestEntity) return false;
            else return CompareTo((TestEntity) obj!) == 0;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id.GetHashCode(), TestDate.GetHashCode());
        }
    }
}
