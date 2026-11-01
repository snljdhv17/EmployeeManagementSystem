using employee.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employee.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

        public class DesignationMasterController : ControllerBase
        {
            private readonly EmployeeDbContext _context;

            public DesignationMasterController(EmployeeDbContext context)
            {
                _context = context;
            }

            // 1. GET: api/Designation
            // Get all designations
            [HttpGet("GetAllDesignations")]
            public async Task<IActionResult> GetAllDesignations()
            {
                try
                {
                    var designations = await _context.Designations.ToListAsync();

                    return Ok(designations);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error retrieving designations.",
                        error = ex.Message
                    });
                }
            }

            // 2. GET: api/Designation/5
            // Get designation by ID
            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetDesignationById(int id)
            {
                try
                {
                    var designation = await _context.Designations
                        .FirstOrDefaultAsync(d => d.designationId == id);

                    if (designation == null)
                        return NotFound(new { message = "Designation not found." });

                    return Ok(designation);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error retrieving designation.",
                        error = ex.Message
                    });
                }
            }

            // 3. POST: api/Designation
            // Create designation
            [HttpPost("AddDesignation")]
            public async Task<IActionResult> CreateDesignation(
                [FromBody] Designation designation)
            {
                try
                {
                    if (designation == null)
                        return BadRequest(new { message = "Invalid request body." });

                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    // Validate designation name
                    if (string.IsNullOrWhiteSpace(designation.designationName))
                    {
                        ModelState.AddModelError(
                            nameof(designation.designationName),
                            "Designation name is required.");

                        return BadRequest(ModelState);
                    }

                    designation.designationName =
                        designation.designationName.Trim();

                    _context.Designations.Add(designation);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(
                        nameof(GetDesignationById),
                        new { id = designation.designationId },
                        designation);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error creating designation.",
                        error = ex.Message
                    });
                }
            }

            // 4. PUT: api/Designation/5
            // Update designation
            [HttpPut("UpdateDesignation/{id:int}")]
            public async Task<IActionResult> UpdateDesignation(
                int id,
                [FromBody] Designation designation)
            {
                try
                {
                    if (designation == null)
                        return BadRequest(new { message = "Invalid request body." });

                    if (id != designation.designationId)
                    {
                        return BadRequest(new
                        {
                            message = "Route ID and designation ID do not match."
                        });
                    }

                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    if (string.IsNullOrWhiteSpace(designation.designationName))
                    {
                        ModelState.AddModelError(
                            nameof(designation.designationName),
                            "Designation name is required.");

                        return BadRequest(ModelState);
                    }

                    var existingDesignation = await _context.Designations
                        .FirstOrDefaultAsync(d => d.designationId == id);

                    if (existingDesignation == null)
                        return NotFound(new { message = "Designation not found." });

                    existingDesignation.departmentId = designation.departmentId;
                    existingDesignation.designationName =
                        designation.designationName.Trim();

                    await _context.SaveChangesAsync();

                return Created("Designation updated Successfully", existingDesignation);
            }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error updating designation.",
                        error = ex.Message
                    });
                }
            }

            // 5. DELETE: api/Designation/5
            // Delete designation
            [HttpDelete("DeleteDesignation/{id:int}")]
            public async Task<IActionResult> DeleteDesignation(int id)
            {
                try
                {
                    var designation = await _context.Designations
                        .FirstOrDefaultAsync(d => d.designationId == id);

                    if (designation == null)
                        return NotFound(new { message = "Designation not found." });

                    _context.Designations.Remove(designation);
                    await _context.SaveChangesAsync();

                return Created("Designation deleted Successfully", designation);
            }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error deleting designation.",
                        error = ex.Message
                    });
                }
            }

            // 6. GET: api/Designation/filter/2
            // Filter designations by department ID
            [HttpGet("filter/{departmentId:int}")]
            public async Task<IActionResult> GetDesignationsByDepartment(
                int departmentId)
            {
                try
                {
                    if (departmentId <= 0)
                    {
                        return BadRequest(new
                        {
                            message = "Department ID must be greater than zero."
                        });
                    }

                    var designations = await _context.Designations
                        .Where(d => d.departmentId == departmentId)
                        .ToListAsync();

                    return Ok(designations);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = "Error filtering designations.",
                        error = ex.Message
                    });
                }
            }
        }
    }

