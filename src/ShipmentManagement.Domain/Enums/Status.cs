namespace ShipmentManagement.Domain.Enums;

public enum Status
{
    Created,
    PickedUp,
    InTransit,
    ArrivedAtFacility,
    OutForDelivery,
    Delivered,
    Delayed,
    Cancelled
}