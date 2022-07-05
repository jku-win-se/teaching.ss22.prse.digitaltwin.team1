namespace SmartRoom.CommonBase.Transfer.Contracts
{
    public interface IDataSimulatorContext
    {
        Task SetAllBinariesForRoomByEqipmentType(Guid roomId, string equipmentType, bool binaryValue);
    }
}