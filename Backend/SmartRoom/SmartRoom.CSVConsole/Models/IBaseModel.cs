namespace SmartRoom.CSVConsole.Models
{
    public interface IBaseModel<out E> where E : new()
    {
        public E GetEntity();

    }
}
