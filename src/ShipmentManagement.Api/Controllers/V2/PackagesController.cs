using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipmentManagement.Api.Models;
using ShipmentManagement.Application.DTOs.Packages;
using ShipmentManagement.Application.DTOs.TrackingEvents;
using ShipmentManagement.Application.Interfaces;
using ShipmentManagement.Application.Mappings;

namespace ShipmentManagement.Api.Controllers.V2;

/// <summary>
/// Controller that provides endpoints for managing packages in version 2 of the API.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class PackagesController : ControllerBase
{
    private readonly IValidator<CreatePackageV2Dto> _createPackageValidator;
    private readonly IPackagesService _packagesService;
    private readonly IShipmentService _shipmentService;

    public PackagesController(IValidator<CreatePackageV2Dto> createPackageValidator, IPackagesService packagesService,
        IShipmentService shipmentService)
    {
        _createPackageValidator = createPackageValidator;
        _packagesService = packagesService;
        _shipmentService = shipmentService;
    }

    /// <summary>
    /// Retrieves a specific package by its ID for the authenticated user.
    /// </summary>
    /// <param name="id">The ID of the package to retrieve.</param>
    /// <returns>The package if found, otherwise a 404 Not Found response.</returns>
    /// <response code="200">Returns the package details.</response>
    /// <response code="404">If the package is not found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PackageV2Dto), StatusCodes.Status200OK)]
    public async Task<ActionResult<PackageV2Dto>> GetUserPackage(int id)
    {
        var packageResult = await _packagesService.GetUserPackageByIdAsync(id);
        if (packageResult == null)
        {
            return NotFound(new { Message = "Package not found" });
        }

        var package = packageResult.ToV2Dto();
        return Ok(package);
    }

    /// <summary>
    /// Retrieves a list of all packages for the authenticated user.
    /// </summary>
    /// <returns>The list of packages if found, otherwise a 404 Not Found response.</returns>
    /// <response code="200">Returns the list of packages.</response>
    /// <response code="404">If no packages are found.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<PackageV2Dto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PackageV2Dto>>> ListUserPackages()
    {
        var packageResults = await _packagesService.GetAllUserPackagesAsync();
        if (packageResults.Count == 0)
        {
            return NotFound(new { Message = "No packages found" });
        }

        var packages = packageResults.Select(r => r.ToV2Dto()).ToList();
        return Ok(packages);
    }

    /// <summary>
    /// Creates a new package for the authenticated user.
    /// </summary>
    /// <param name="createV2Dto">The data for creating the package.</param>
    /// <returns>The created package if successful, otherwise a 400 Bad Request response.</returns>
    /// <response code="201">Returns the created package details.</response>
    /// <response code="400">If the request body contains invalid data.</response>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PackageV2Dto), StatusCodes.Status201Created)]
    public async Task<ActionResult<PackageV2Dto>> CreatePackage([FromBody] CreatePackageV2Dto? createV2Dto)
    {
        if (createV2Dto == null)
        {
            return BadRequest(new
            {
                Message = "The request body contains invalid data structures or an unrecognized Enum string value."
            });
        }

        var validationResult = await _createPackageValidator.ValidateAsync(createV2Dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var createPackageRequest = createV2Dto.ToCreateRequest();
        var packageResult = await _packagesService.CreatePackageAsync(createPackageRequest);

        var package = packageResult.ToV2Dto();
        return CreatedAtAction(nameof(GetUserPackage), new { id = package.Id }, package);
    }

    /// <summary>
    /// Retrieves the tracking events for a specific package by its ID for the authenticated user.
    /// </summary>
    /// <param name="id">The ID of the package to track.</param>
    /// <returns>The list of tracking events if found, otherwise a 404 Not Found response.</returns>
    /// <response code="200">Returns the list of tracking events.</response>
    /// <response code="404">If the package is not found.</response>
    [HttpGet("{id:int}/tracking")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<UserTrackingEvent>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserTrackingEvent>>> TrackPackage(int id)
    {
        var trackingEvents = await _shipmentService.GetUserEventsByPackageIdAsync(id);
        if (trackingEvents.Count == 0)
        {
            return NotFound(new { Message = "No tracking events found for the specified package." });
        }
        var userPackageEvents = trackingEvents.Select(ToUserPackageTrackingEvent).ToList();
        return Ok(userPackageEvents);
    }

    /// <summary>
    /// Cancels a specific package by its ID for the authenticated user.
    /// </summary>
    /// <param name="id">The ID of the package to cancel.</param>
    /// <returns>A 204 No Content response if successful, otherwise a 422 Unprocessable Entity response.</returns>
    /// <response code="204">If the package is successfully canceled.</response>
    /// <response code="422">If the package cannot be canceled.</response>
    [HttpPatch("{id:int}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CancelPackage(int id)
    {
        var result = await _packagesService.CancelPackageAsync(id);
        if (!result)
        {
            return UnprocessableEntity(new { Message = "Could not cancel the package." });
        }

        return NoContent();
    }

    private static UserTrackingEvent ToUserPackageTrackingEvent(TrackingEventResult e)
    {
        return new UserTrackingEvent
        {
            PackageId = e.PackageId,
            PackageName = e.PackageName,
            FacilityName = e.FacilityName,
            Status = e.Status,
            OccuredAt = e.OccuredAt,
            Description = e.Description
        };
    }
}