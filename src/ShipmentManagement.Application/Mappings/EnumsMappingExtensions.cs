using ShipmentManagement.Domain.Enums;

namespace ShipmentManagement.Application.Mappings;

public static class EnumsMappingExtensions
{
    public static Status ToDomain(this DTOs.Common.Status value)
    {
        return value switch
        {
            DTOs.Common.Status.Created => Status.Created,
            DTOs.Common.Status.PickedUp => Status.PickedUp,
            DTOs.Common.Status.InTransit => Status.InTransit,
            DTOs.Common.Status.ArrivedAtFacility => Status.ArrivedAtFacility,
            DTOs.Common.Status.OutForDelivery => Status.OutForDelivery,
            DTOs.Common.Status.Delivered => Status.Delivered,
            DTOs.Common.Status.Delayed => Status.Delayed,
            DTOs.Common.Status.Cancelled => Status.Cancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static DTOs.Common.Status ToDto(this Status value)
    {
        return value switch
        {
            Status.Created => DTOs.Common.Status.Created,
            Status.PickedUp => DTOs.Common.Status.PickedUp,
            Status.InTransit => DTOs.Common.Status.InTransit,
            Status.ArrivedAtFacility => DTOs.Common.Status.ArrivedAtFacility,
            Status.OutForDelivery => DTOs.Common.Status.OutForDelivery,
            Status.Delivered => DTOs.Common.Status.Delivered,
            Status.Delayed => DTOs.Common.Status.Delayed,
            Status.Cancelled => DTOs.Common.Status.Cancelled,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static Result ToDomain(this DTOs.Common.Result value)
    {
        return value switch
        {
            DTOs.Common.Result.Successful => Result.Successful,
            DTOs.Common.Result.Failed => Result.Failed,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static DTOs.Common.Result ToDto(this Result value)
    {
        return value switch
        {
            Result.Successful => DTOs.Common.Result.Successful,
            Result.Failed => DTOs.Common.Result.Failed,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    
    public static DeliveryType ToDomain(this DTOs.Common.DeliveryType value)
    {
        return value switch
        {
            DTOs.Common.DeliveryType.Standard => DeliveryType.Standard,
            DTOs.Common.DeliveryType.Express => DeliveryType.Express,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static DTOs.Common.DeliveryType ToDto(this DeliveryType value)
    {
        return value switch
        {
            DeliveryType.Standard => DTOs.Common.DeliveryType.Standard,
            DeliveryType.Express => DTOs.Common.DeliveryType.Express,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}