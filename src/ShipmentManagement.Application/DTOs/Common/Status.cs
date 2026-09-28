namespace ShipmentManagement.Application.DTOs.Common;

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