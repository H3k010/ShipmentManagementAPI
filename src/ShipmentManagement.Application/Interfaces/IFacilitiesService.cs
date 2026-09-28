using ShipmentManagement.Application.DTOs.Facilities;

namespace ShipmentManagement.Application.Interfaces;

/// <summary>
/// Interface for managing facilities in the shipment management system.
/// </summary>
public interface IFacilitiesService
{
    /// <summary>
    /// Retrieves a facility by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the facility.</param>
    /// <returns>The facility result or null if not found.</returns>
    Task<FacilityResult?> GetFacilityByIdAsync(int? id);

    /// <summary>
    /// Retrieves all facilities in the system.
    /// </summary>
    /// <returns>The list of all facility results.</returns>
    Task<List<FacilityResult>> GetAllFacilitiesAsync();

    /// <summary>
    /// Adds a new facility to the system.
    /// </summary>
    /// <param name="createRequest">The request object containing the details for creating the facility.</param>
    /// <returns>The created facility result.</returns>
    Task<FacilityResult> AddFacilityAsync(CreateFacilityRequest createRequest);

    /// <summary>
    /// Updates an existing facility in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the facility to update.</param>
    /// <param name="updateRequest">The request object containing the details for updating the facility.</param>
    /// <returns>A boolean indicating whether the facility was successfully updated.</returns>
    Task<bool> UpdateFacilityAsync(int id, UpdateFacilityRequest updateRequest);

    /// <summary>
    /// Checks if a facility is available for operations based on its current active status.
    /// </summary>
    /// <param name="facilityResult">The facility result to check.</param>
    /// <returns>A boolean indicating whether the facility is available for operations.</returns>
    bool IsFacilityAvailableForOperations(FacilityResult facilityResult);

    /// <summary>
    /// Activates a facility in the system, making it available for operations.
    /// </summary>
    /// <param name="facilityResult">The facility result to activate.</param>
    /// <returns>A boolean indicating whether the facility was successfully activated.</returns>
    Task<bool> ActivateFacilityAsync(FacilityResult facilityResult);

    /// <summary>
    /// Deactivates a facility in the system, making it unavailable for operations.
    /// </summary>
    /// <param name="facilityResult">The facility result to deactivate.</param>
    /// <returns>A boolean indicating whether the facility was successfully deactivated.</returns>
    Task<bool> DeactivateFacilityAsync(FacilityResult facilityResult);
}