using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipmentManagement.Application.DTOs.DeliveryAttempts;
using ShipmentManagement.Application.DTOs.Packages;
using ShipmentManagement.Application.DTOs.TrackingEvents;
using ShipmentManagement.Application.Interfaces;
using ShipmentManagement.Application.Mappings;

namespace ShipmentManagement.Api.Controllers.V1;

/// <summary>
/// Controller for managing packages and retrieving package details. Accessible only with administrative privileges.
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
    /// <returns>The list of packages or a not found response.</returns>
    /// <response code="200">Returns the list of packages.</response>
    /// <response code="404">If no packages are found.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<PackageV1Dto>>> ListPackages()
    {
        var packageResults = await _packageService.GetAllPackagesAsync();
        if (packageResults.Count == 0)
        {
            return NotFound(new { Message = "No packages found" });
        }

        var packages = packageResults.Select(p => p.ToV1Dto()).ToList();
        return Ok(packages);
    }

    /// <summary>
    /// Retrieves a specific package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package to retrieve.</param>
    /// <returns>The package information or a not found response.</returns>
    /// <response code="200">The package information was successfully retrieved.</response>
    /// <response code="404">The package was not found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PackageV1Dto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PackageV1Dto>> GetPackage(int id)
    {
        var packageResult = await _packageService.GetPackageByIdAsync(id);
        if (packageResult == null)
        {
            return NotFound(new { Message = $"Package with Id {id} not found" });
        }

        var package = packageResult.ToV1Dto();
        return Ok(package);
    }

    /// <summary>
    /// Retrieves the tracking events for a specific package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package for which to retrieve tracking events.</param>
    /// <returns>The list of tracking events or a not found response.</returns>
    /// <response code="200">The tracking events were successfully retrieved.</response>
    /// <response code="404">The package was not found.</response>
    [HttpGet("{id:int}/events")]
    [ProducesResponseType(typeof(List<TrackingEventDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// Records a new tracking event for a specific package.
    /// </summary>
    /// <param name="id">The ID of the package for which to record an event.</param>
    /// <param name="dto">The details of the tracking event to record.</param>
    /// <returns>The recorded tracking event or a bad request response.</returns>
    /// <response code="200">The tracking event was successfully recorded.</response>
    /// <response code="400">The provided data is invalid.</response>
    [HttpPost("{id:int}/events")]
    [ProducesResponseType(typeof(TrackingEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    /// <returns>The list of delivery attempts or a not found response.</returns>
    /// <response code="200">The delivery attempts were successfully retrieved.</response>
    /// <response code="404">The package was not found.</response>
    [HttpGet("{id:int}/delivery-attempts")]
    [ProducesResponseType(typeof(List<DeliveryAttemptDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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