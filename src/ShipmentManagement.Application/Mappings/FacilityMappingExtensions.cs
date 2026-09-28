using ShipmentManagement.Application.DTOs.Facilities;
using ShipmentManagement.Domain.Entities;

namespace ShipmentManagement.Application.Mappings;

public static class FacilityMappingExtensions
{
    public static FacilityResult ToResult(this Facility facility)
    {
        return new FacilityResult
        {
            Id = facility.Id,
            Name = facility.Name,
            Address = facility.Address,
            City = facility.City,
            IsActive = facility.IsActive
        };
    }
    
    public static FacilityDto ToDto(this FacilityResult result)
    {
        return new FacilityDto
        {
            Id = result.Id,
            Name = result.Name,
            Address = result.Address,
            City = result.City,
            IsActive = result.IsActive
        };
    }

    public static Facility ToEntity(this CreateFacilityRequest request)
    {
        return new Facility
        {
            Name = request.Name,
            Address = request.Address,  
            City = request.City,
        };
    }
    
    public static CreateFacilityRequest ToCreateRequest(this CreateFacilityDto dto)
    {
        return new CreateFacilityRequest
        {
            Name = dto.Name,
            Address = dto.Address,  
            City = dto.City,
        };
    }
    
    public static Facility ToEntity(this UpdateFacilityRequest request)
    {
        return new Facility
        {
            Name = request.Name,
            Address = request.Address,
            City = request.City,
        };
    }
    
    public static UpdateFacilityRequest ToUpdateRequest(this UpdateFacilityDto dto)
    {
        return new UpdateFacilityRequest
        {
            Name = dto.Name,
            Address = dto.Address,  
            City = dto.City,
        };
    }
    
    // for service-to-service
    public static Facility ToEntity(this FacilityResult dto)
    {
        return new Facility
        {
            Id = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            IsActive = dto.IsActive
        };
    }
}