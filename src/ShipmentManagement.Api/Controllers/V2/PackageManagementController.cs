using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipmentManagement.Application.DTOs.DeliveryAttempts;
using ShipmentManagement.Application.DTOs.Packages;
using ShipmentManagement.Application.DTOs.TrackingEvents;
using ShipmentManagement.Application.Interfaces;
using ShipmentManagement.Application.Mappings;

namespace ShipmentManagement.Api.Controllers.V2;

/// <summary>
/// Controller for managing packages and their related operations in the Shipment Management system.
/// Accessible only with administrative privileges.
/// </summary>
[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/v{version:apiVersion}/admin/packages")]
public class PackageManagementController : ControllerBase
{
    private readonly IValidator<CreateTrackingEventDto> _createTrackingEventValidator;
    private readonly IPackagesService _packageService;
    private readonly IShipmentService _shipmentService;

    public PackageManagementController(IValidator<CreateTrackingEventDto> createTrackingEventValidator,
        IPackagesService packageService, IShipmentService shipmentService)
    {
        _createTrackingEventValidator = createTrackingEventValidator;
        _packageService = packageService;
        _shipmentService = shipmentService;
    }

    /// <summary>
    /// Lists all packages in the system.
    /// </summary>
    /// <returns>The list of packages if found, otherwise a 404 Not Found response.</returns>
    /// <response code="200">Returns the list of packages.</response>
    /// <response code="404">If no packages are found.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<PackageV2Dto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PackageV2Dto>>> ListPackages()
    {
        var packageResults = await _packageService.GetAllPackagesAsync();
        if (packageResults.Count == 0)
        {
            return NotFound(new { Message = "No packages found" });
        }

        var packages = packageResults.Select(p => p.ToV2Dto()).ToList();
        return Ok(packages);
    }

    /// <summary>
    /// Retrieves a specific package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package to retrieve.</param>
    /// <returns>The package if found, otherwise a 404 Not Found response.</returns>
    /// <response code="200">Returns the package.</response>
    /// <response code="404">If the package is not found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PackageV2Dto), StatusCodes.Status200OK)]
    public async Task<ActionResult<PackageV2Dto>> GetPackage(int id)
    {
        var packageResult = await _packageService.GetPackageByIdAsync(id);
        if (packageResult == null)
        {
            return NotFound(new { Message = $"Package with Id {id} not found" });
        }

        var package = packageResult.ToV2Dto();
        return Ok(package);
    }

    /// <summary>
    /// Retrieves the tracking events for a specific package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package for which to retrieve tracking events.</param>
    /// <returns>The list of tracking events if found, otherwise a 404 Not Found response.</returns>
    /// <response code="200">Returns the list of tracking events.</response>
    /// <response code="404">If no tracking events are found for the package.</response>
    [HttpGet("{id:int}/events")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<TrackingEventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TrackingEventDto>>> TrackPackage(int id)
    {
        var eventResults = await _shipmentService.GetEventsByPackageIdAsync(id);
        if (eventResults.Count == 0)
        {
            return NotFound(new { Message = $"Tracking events for Package with Id {id} not found" });
        }

        var trackingEvents = eventResults.Select(e => e.ToDto()).ToList();
        return Ok(trackingEvents);
    }

    /// <summary>
    /// Records a new tracking event for a specific package by its ID. Validates the input data and returns appropriate responses based on the outcome of the operation.
    /// </summary>
    /// <param name="id">The ID of the package for which to record a tracking event.</param>
    /// <param name="dto">The data for the new tracking event.</param>
    /// <returns>The created tracking event if successful, otherwise an error response.</returns>
    /// <response code="200">Returns the created tracking event.</response>
    /// <response code="400">If the input data is invalid.</response>
    [HttpPost("{id:int}/events")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(TrackingEventDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<TrackingEventDto>> RecordEvent(int id, [FromBody] CreateTrackingEventDto dto)
    {
        var validationResult = await _createTrackingEventValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var createEventRequest = dto.ToCreateRequest();
        var eventResult = await _shipmentService.RecordEventAsync(id, createEventRequest);
        if (!eventResult.IsSuccess)
        {
            return BadRequest(new { eventResult.Message });
        }

        var trackingEvent = eventResult.TrackingEventResult.ToDto();
        return Ok(trackingEvent);
    }

    /// <summary>
    /// Lists all delivery attempts for a specific package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package for which to list delivery attempts.</param>
    /// <returns>The list of delivery attempts if found, otherwise a 404 Not Found response.</returns>
    /// <response code="200">Returns the list of delivery attempts.</response>
    /// <response code="404">If no delivery attempts are found for the package.</response>
    [HttpGet("{id:int}/delivery-attempts")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<DeliveryAttemptDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DeliveryAttemptDto>>> ListDeliveryAttempts(int id)
    {
        var attemptResults = await _shipmentService.GetDeliveryAttemptsByPackageIdAsync(id);
        if (attemptResults.Count == 0)
        {
            return NotFound(new { Message = $"No delivery attempts found for Package with Id {id}" });
        }

        var deliveryAttempts = attemptResults.Select(a => a.ToDto()).ToList();
        return Ok(deliveryAttempts);
    }
}