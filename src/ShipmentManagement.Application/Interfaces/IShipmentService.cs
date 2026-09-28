using ShipmentManagement.Application.DTOs.DeliveryAttempts;
using ShipmentManagement.Application.DTOs.TrackingEvents;

namespace ShipmentManagement.Application.Interfaces;

/// <summary>
/// Represents a service for managing shipments, including tracking events and delivery attempts.
/// </summary>
public interface IShipmentService
{
    /// <summary>
    /// Retrieves a list of tracking events associated with a specific tracking number.
    /// </summary>
    /// <param name="tracingNumber">The tracking number for which to retrieve events.</param>
    /// <returns>A list of tracking events.</returns>
    Task<List<TrackingEventResult>> GetEventsByTrackingNumberAsync(string tracingNumber);

    /// <summary>
    /// Retrieves a list of tracking events associated with a specific package ID.
    /// </summary>
    /// <param name="packageId">The package ID for which to retrieve events.</param>
    /// <returns>A list of tracking events.</returns>
    Task<List<TrackingEventResult>> GetEventsByPackageIdAsync(int packageId);

    /// <summary>
    /// Retrieves a list of tracking events associated with a specific package ID for the current user.
    /// </summary>
    /// <param name="packageId">The package ID for which to retrieve events.</param>
    /// <returns>A list of tracking events.</returns>
    Task<List<TrackingEventResult>> GetUserEventsByPackageIdAsync(int packageId);

    /// <summary>
    /// Retrieves a list of delivery attempts associated with a specific package ID.
    /// </summary>
    /// <param name="packageId">The package ID for which to retrieve delivery attempts.</param>
    /// <returns>A list of delivery attempts.</returns>
    Task<List<DeliveryAttemptResult>> GetDeliveryAttemptsByPackageIdAsync(int packageId);

    /// <summary>
    /// Records a new tracking event for a specific package ID.
    /// </summary>
    /// <param name="packageId">The package ID for which to record the event.</param>
    /// <param name="createRequest">The request object containing the details for creating the tracking event.</param>
    /// <returns>The result of the event recording operation.</returns>
    Task<RecordEventResult> RecordEventAsync(int packageId, CreateTrackingEventRequest createRequest);
}