using ShipmentManagement.Application.DTOs.TrackingEvents;
using ShipmentManagement.Domain.Entities;

namespace ShipmentManagement.Application.Mappings;

public static class TrackingEventMappingExtensions
{
    public static TrackingEventResult ToResult(this TrackingEvent trackingEvent)
    {
        return new TrackingEventResult
        {
            Id = trackingEvent.Id,
            PackageId = trackingEvent.PackageId,
            PackageName = trackingEvent.PackageName,
            FacilityName = trackingEvent.FacilityName,
            Status = trackingEvent.Status.ToDto(),
            OccuredAt = trackingEvent.OccuredAt,
            Description = trackingEvent.Description
        };
    }
    
    public static TrackingEventDto ToDto(this TrackingEventResult result)
    {
        return new TrackingEventDto
        {
            Id = result.Id,
            PackageId = result.PackageId,
            PackageName = result.PackageName,
            FacilityName = result.FacilityName,
            Status = result.Status,
            OccuredAt = result.OccuredAt,
            Description = result.Description
        };
    }
    
    public static TrackingEvent ToEntity(this CreateTrackingEventRequest request)
    {
        return new TrackingEvent
        {
            FacilityId = request.FacilityId,
            Status = request.NewStatus.ToDomain(), 
            Description = request.Description
        };
    }
    
    public static CreateTrackingEventRequest ToCreateRequest(this CreateTrackingEventDto createDto)
    {
        return new CreateTrackingEventRequest
        {
            FacilityId = createDto.FacilityId,
            NewStatus = createDto.NewStatus,
            Description = createDto.Description
        };
    }
}