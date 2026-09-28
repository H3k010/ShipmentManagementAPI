using ShipmentManagement.Application.DTOs.Packages;
using ShipmentManagement.Domain.Entities;
using ShipmentManagement.Domain.Enums;

namespace ShipmentManagement.Application.Interfaces;

/// <summary>
/// Interface for managing packages in the shipment management system.
/// </summary>
public interface IPackagesService
{
    /// <summary>
    /// Retrieves a package by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the package.</param>
    /// <returns>The package result or null if not found.</returns>
    Task<PackageResult?> GetPackageByIdAsync(int id);

    /// <summary>
    /// Retrieves a user's package by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the package.</param>
    /// <returns>The package result or null if not found.</returns>
    Task<PackageResult?> GetUserPackageByIdAsync(int id);

    /// <summary>
    /// Retrieves all packages in the system.
    /// </summary>
    /// <returns>The list of all package results.</returns>
    Task<List<PackageResult>> GetAllPackagesAsync();

    /// <summary>
    /// Retrieves all packages associated with the current user.
    /// </summary>
    /// <returns>The list of package results.</returns>
    Task<List<PackageResult>> GetAllUserPackagesAsync();

    /// <summary>
    /// Creates a new package.
    /// </summary>
    /// <param name="createRequest">The request object containing the details for creating the package.</param>
    /// <returns>The created package result.</returns>
    Task<PackageResult> CreatePackageAsync(CreatePackageRequest createRequest);

    /// <summary>
    /// Cancels an existing package.
    /// </summary>
    /// <param name="id">The unique identifier of the package to cancel.</param>
    /// <returns>A boolean indicating whether the package was successfully canceled.</returns>
    Task<bool> CancelPackageAsync(int id);

    /// <summary>
    /// Retrieves a package by its tracking number.
    /// </summary>
    /// <param name="trackingNumber">The tracking number of the package to retrieve.</param>
    /// <returns>The package result or null if not found.</returns>
    Task<PackageResult?> GetPackageByTrackingNumberAsync(string trackingNumber);

    /// <summary>
    /// Determines whether the specified package can accept tracking events.
    /// </summary>
    /// <param name="package">The package to check.</param>
    /// <returns>true if the package can accept tracking events; otherwise, false.</returns>
    bool CanAcceptTrackingEvents(Package package);

    /// <summary>
    /// Determines whether a package can transition from its current status to a new status.
    /// </summary>
    /// <param name="currentStatus">The current status of the package.</param>
    /// <param name="newStatus">The new status to transition to.</param>
    /// <returns>true if the package can transition to the new status; otherwise, false.</returns>
    bool CanTransitionToStatus(Status currentStatus, Status newStatus);
}