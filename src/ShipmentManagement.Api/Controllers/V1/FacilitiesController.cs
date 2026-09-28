using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipmentManagement.Application.DTOs.Facilities;
using ShipmentManagement.Application.Interfaces;
using ShipmentManagement.Application.Mappings;

namespace ShipmentManagement.Api.Controllers.V1;

/// <summary>
/// Controller for managing facilities in the shipment management system. Accessible only with administrative privileges.
/// </summary>
[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/v{version:apiVersion}/admin/[controller]")]
public class FacilitiesController : ControllerBase
{
    private readonly IValidator<CreateFacilityDto> _createFacilityValidator;
    private readonly IValidator<UpdateFacilityDto> _updateFacilityValidator;
    private readonly IFacilitiesService _facilitiesService;

    public FacilitiesController(IValidator<CreateFacilityDto> createFacilityValidator,
        IValidator<UpdateFacilityDto> updateFacilityValidator, IFacilitiesService facilitiesService)
    {
        _createFacilityValidator = createFacilityValidator;
        _updateFacilityValidator = updateFacilityValidator;
        _facilitiesService = facilitiesService;
    }

    /// <summary>
    /// Retrieves a facility by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the facility to retrieve.</param>
    /// <returns>The facility information or a not found response.</returns>
    /// <response code="200">The facility information was successfully retrieved.</response>
    /// <response code="404">The facility was not found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FacilityDto>> GetFacility(int id)
    {
        var facilityResult = await _facilitiesService.GetFacilityByIdAsync(id);
        if (facilityResult == null)
        {
            return NotFound(new { Message = $"Facility with Id {id} is not found" });
        }

        var facility = facilityResult.ToDto();
        return Ok(facility);
    }

    /// <summary>
    /// Lists all facilities in the system.
    /// </summary>
    /// <returns>The list of facilities or a not found response.</returns>
    /// <response code="200">Returns the list of facilities.</response>
    /// <response code="404">If no facilities are found.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<FacilityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<FacilityDto>>> ListFacilities()
    {
        var facilityResults = await _facilitiesService.GetAllFacilitiesAsync();
        if (facilityResults.Count == 0)
        {
            return NotFound(new { Message = "No facilities found." });
        }

        var facilities = facilityResults.Select(f => f.ToDto()).ToList();
        return Ok(facilities);
    }

    /// <summary>
    /// Adds a new facility to the system.
    /// </summary>
    /// <param name="facilityDto">The data for the new facility.</param>
    /// <returns>The created facility information.</returns>
    /// <response code="201">The facility was successfully created.</response>
    /// <response code="400">If the provided facility data is invalid.</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FacilityDto>> AddFacility([FromBody] CreateFacilityDto facilityDto)
    {
        var validationResult = await _createFacilityValidator.ValidateAsync(facilityDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var createFacilityRequest = facilityDto.ToCreateRequest();
        var createdFacility = await _facilitiesService.AddFacilityAsync(createFacilityRequest);
        return CreatedAtAction(nameof(GetFacility), new { id = createdFacility.Id }, createdFacility);
    }

    /// <summary>
    /// Updates an existing facility in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the facility to update.</param>
    /// <param name="facilityDto">The updated facility data.</param>
    /// <returns>A success or error response.</returns>
    /// <response code="204">The facility was successfully updated.</response>
    /// <response code="400">If the provided facility data is invalid.</response>
    /// <response code="422">If the facility could not be updated due to business rules.</response>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateFacility(int id, [FromBody] UpdateFacilityDto facilityDto)
    {
        var validationResult = await _updateFacilityValidator.ValidateAsync(facilityDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var updateFacilityRequest = facilityDto.ToUpdateRequest();
        var isUpdated = await _facilitiesService.UpdateFacilityAsync(id, updateFacilityRequest);
        if (!isUpdated)
        {
            return UnprocessableEntity(new { Message = "Could not update the facility" });
        }

        return NoContent();
    }

    /// <summary>
    /// Activates a facility in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the facility to activate.</param>
    /// <returns>A success or error response.</returns>
    /// <response code="204">The facility was successfully activated.</response>
    /// <response code="422">If the facility could not be activated.</response>
    /// <response code="404">If the facility is not found.</response>
    [HttpPatch("{id:int}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActivateFacility(int id)
    {
        var facility = await _facilitiesService.GetFacilityByIdAsync(id);
        if (facility == null)
        {
            return NotFound(new { Message = $"Facility with Id {id} is not found" });
        }

        var isActivated = await _facilitiesService.ActivateFacilityAsync(facility);
        if (!isActivated)
        {
            return UnprocessableEntity(new { Message = "Could not activate the facility" });
        }

        return NoContent();
    }

    /// <summary>
    /// Deactivates a facility in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the facility to deactivate.</param>
    /// <returns>A success or error response.</returns>
    /// <response code="204">The facility was successfully deactivated.</response>
    /// <response code="422">If the facility could not be deactivated due to business rules.</response>
    /// <response code="404">If the facility is not found.</response>
    [HttpPatch("{id:int}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> DeactivateFacility(int id)
    {
        var facility = await _facilitiesService.GetFacilityByIdAsync(id);
        if (facility == null)
        {
            return NotFound(new { Message = $"Facility with Id {id} is not found" });
        }

        var isDeactivated = await _facilitiesService.DeactivateFacilityAsync(facility);
        if (!isDeactivated)
        {
            return UnprocessableEntity(new { Message = "Could not deactivate the facility" });
        }

        return NoContent();
    }
}