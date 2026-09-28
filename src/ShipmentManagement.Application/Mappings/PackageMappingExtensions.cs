using ShipmentManagement.Application.DTOs.Packages;
using ShipmentManagement.Domain.Entities;

namespace ShipmentManagement.Application.Mappings;

public static class PackageMappingExtensions
{
    // Entity --> Result
    public static PackageResult ToResult(this Package package)
    {
        return new PackageResult
        {
            Id = package.Id,
            TrackingNumber = package.TrackingNumber,
            Name = package.Name,
            SenderName = package.SenderName,
            RecipientName = package.RecipientName,
            OriginAddress = package.OriginAddress.ToDto(),
            DestinationAddress = package.DestinationAddress.ToDto(),
            CurrentStatus = package.CurrentStatus.ToDto(),
            DeliveryType = package.DeliveryType.ToDto(),
            EstimatedDeliveryDate = package.EstimatedDeliveryDate,
            CreatedAt = package.CreatedAt
        };
    }
    
    // Result --> V1 Dto
    public static PackageV1Dto ToV1Dto(this PackageResult result)
    {
        return new PackageV1Dto
        {
            Id = result.Id,
            TrackingNumber = result.TrackingNumber,
            Name = result.Name,
            SenderName = result.SenderName,
            RecipientName = result.RecipientName,
            OriginAddress = result.OriginAddress,
            DestinationAddress = result.DestinationAddress,
            CurrentStatus = result.CurrentStatus,
            EstimatedDeliveryDate = result.EstimatedDeliveryDate,
            CreatedAt = result.CreatedAt
        };
    }
    
    // Result --> V2 Dto
    public static PackageV2Dto ToV2Dto(this PackageResult result)
    {
        return new PackageV2Dto
        {
            Id = result.Id,
            TrackingNumber = result.TrackingNumber,
            Name = result.Name,
            SenderName = result.SenderName,
            RecipientName = result.RecipientName,
            OriginAddress = result.OriginAddress,
            DestinationAddress = result.DestinationAddress,
            CurrentStatus = result.CurrentStatus,
            DeliveryType = result.DeliveryType,
            EstimatedDeliveryDate = result.EstimatedDeliveryDate,
            CreatedAt = result.CreatedAt
        };
    }

    // Create Request --> Entity
    public static Package ToEntity(this CreatePackageRequest createRequest)
    {
        return new Package
        {
            Name = createRequest.Name,
            SenderName = createRequest.SenderName,
            RecipientName = createRequest.RecipientName,
            DeliveryType = createRequest.DeliveryType.ToDomain(),
            OriginAddress = createRequest.OriginAddress.ToOwnedType(),
            DestinationAddress = createRequest.DestinationAddress.ToOwnedType(),
        };
    }
    
    // Create V1 Dto --> Create Request
    public static CreatePackageRequest ToCreateRequest(this CreatePackageV1Dto v1Dto)
    {
        return new CreatePackageRequest
        {
            Name = v1Dto.Name,
            SenderName = v1Dto.SenderName,
            RecipientName = v1Dto.RecipientName,
            OriginAddress = v1Dto.OriginAddress,
            DestinationAddress = v1Dto.DestinationAddress
        };
    }
    
    // Create V2 Dto --> Create Request
    public static CreatePackageRequest ToCreateRequest(this CreatePackageV2Dto v2Dto)
    {
        return new CreatePackageRequest
        {
            Name = v2Dto.Name,
            SenderName = v2Dto.SenderName,
            RecipientName = v2Dto.RecipientName,
            OriginAddress = v2Dto.OriginAddress,
            DeliveryType = v2Dto.DeliveryType,
            DestinationAddress = v2Dto.DestinationAddress
        };
    }
}