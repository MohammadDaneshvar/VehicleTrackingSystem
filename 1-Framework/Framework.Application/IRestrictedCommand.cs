using Framework.Domain.Enum;

namespace Framework.Application
{
    public interface IRestrictedCommand
    {
        public CommandPermission Permission { get; set; }
    }
    public enum CommandPermission:byte
    {
        RegisterVehicle,
        RecordVehiclePosition,
        GetVehiclePositions,
        GetVehicleCurrentPosition
    }
}