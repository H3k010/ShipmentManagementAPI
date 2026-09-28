using ShipmentManagement.Application.DTOs.Common;
using ShipmentManagement.Domain.ValueObjects;

namespace ShipmentManagement.Application.Mappings;

public static class AddressMappingExtensions
{
    public static Address ToOwnedType(this AddressDto dto)
    {
        return new Address
        {
            Street = dto.Street,
            City = dto.City,
            PostalCode = dto.PostalCode
        };
    }
    
    public static AddressDto ToDto(this Address address)
    {
        return new AddressDto
        {
            Street = address.Street,
            City = address.City,
            PostalCode = address.PostalCode
        };
    }
}