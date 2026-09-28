using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipmentManagement.Api.Models;
using ShipmentManagement.Application.DTOs.Packages;
using ShipmentManagement.Application.DTOs.TrackingEvents;
using ShipmentManagement.Application.Interfaces;
using ShipmentManagement.Application.Mappings;

namespace ShipmentManagement.Api.Controllers.V1;

/// <summary>
/// Controller that Provides operations for managing packages.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class PackagesController : ControllerBase
{
    private readonly IValidator<CreatePackageV1Dto> _createPackageValidator;
    private readonly IPackagesService _packagesService;
    private readonly IShipmentService _shipmentService;

    public PackagesController(IValidator<CreatePackageV1Dto> createPackageValidator, IPackagesService packagesService,
        IShipmentService shipmentService)
    {
        _createPackageValidator = createPackageValidator;
        _packagesService = packagesService;
        _shipmentService = shipmentService;
    }

    /// <summary>
    /// Get a specific package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package to retrieve.</param>
    /// <returns>The requested package or a not found response.</returns>
    /// <response code="200">The package was successfully retrieved.</response>
    /// <response code="404">The package was not found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PackageV1Dto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PackageV1Dto>> GetUserPackage(int id)
    {
        var packageResult = await _packagesService.GetUserPackageByIdAsync(id);
        if (packageResult == null)
        {
            return NotFound(new { Message = "Package not found" });
        }

        var package = packageResult.ToV1Dto();
        return Ok(package);
    }

    /// <summary>
    /// List all packages for the current user.
    /// </summary>
    /// <returns>A list of packages or a not found response.</returns>
    /// <response code="200">The packages were successfully retrieved.</response>
    /// <response code="404">No packages were found.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<PackageV1Dto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<PackageV1Dto>>> ListUserPackages()
    {
        var packageResults = await _packagesService.GetAllUserPackagesAsync();
        if (packageResults.Count == 0)
        {
            return NotFound(new { Message = "No packages found" });
        }

        var packages = packageResults.Select(r => r.ToV1Dto()).ToList();
        return Ok(packages);
    }

    /// <summary>
    /// Create a new package.
    /// </summary>
    /// <param name="createV1Dto">The details for creating the package.</param>
    /// <returns>The created package or a bad request response.</returns>
    /// <response code="201">The package was successfully created.</response>
    /// <response code="400">The package creation request was invalid.</response>
    [HttpPost("create")]
    [ProducesResponseType(typeof(PackageV1Dto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PackageV1Dto>> CreatePackage([FromBody] CreatePackageV1Dto createV1Dto)
    {
        var validationResult = await _createPackageValidator.ValidateAsync(createV1Dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var createPackageRequest = createV1Dto.ToCreateRequest();
        var packageResult = await _packagesService.CreatePackageAsync(createPackageRequest);

        var package = packageResult.ToV1Dto();
        return CreatedAtAction(nameof(GetUserPackage), new { id = package.Id }, package);
    }

    /// <summary>
    /// Track a package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package to track.</param>
    /// <returns>A list of tracking events or a not found response.</returns>
    /// <response code="200">The tracking events were successfully retrieved.</response>
    /// <response code="404">No tracking events were found for the package.</response>
    [HttpGet("{id:int}/tracking")]
    [ProducesResponseType(typeof(List<UserTrackingEvent>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<UserTrackingEvent>>> TrackPackage(int id)
    {
        var trackingEvents = await _shipmentService.GetUserEventsByPackageIdAsync(id);
        if (trackingEvents.Count == 0)
        {
            return NotFound(new { Message = "No tracking events found for the package." });
        }

        var userPackageEvents = trackingEvents.Select(ToUserPackageTrackingEvent).ToList();
        return Ok(userPackageEvents);
    }

    /// <summary>
    /// Cancel a package by its ID.
    /// </summary>
    /// <param name="id">The ID of the package to cancel.</param>
    /// <returns>A no content response or an unprocessable entity response.</returns>
    /// <response code="204">The package was successfully canceled.</response>
    /// <response code="422">The package could not be canceled.</response>
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