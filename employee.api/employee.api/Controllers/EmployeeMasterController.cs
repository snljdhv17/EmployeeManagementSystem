using employee.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace employee.api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("enableAll")]
    [ApiController]
    public class EmployeeMasterController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeMasterController(EmployeeDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Employee
        // Get all employees

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var data = await (
                    from emp in _context.Employees.AsNoTracking()
                    join des in _context.Designations.AsNoTracking()
                        on emp.designationId equals des.designationId
                    join dept in _context.Departments.AsNoTracking()
                        on des.departmentId equals dept.departmentId
                    select new
                    {
                        emp.employeeId,
                        emp.name,
                        emp.contact,
                        emp.email,
                        emp.city,
                        emp.state,
                        emp.pincode,
                        emp.altContactNo,
                        emp.address,
                        emp.designationId,
                        designationName = des.designationName,
                        departmentId = dept.departmentId,
                        departmentName = dept.departmentName,
                        emp.role,
                        emp.createdDate,
                        emp.modifiedDate
                    }
                ).ToListAsync();

                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving employees."
                });
            }
        }

        // 2. GET: api/Employee/5
        // Get employee by ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.employeeId == id);

                if (employee == null)
                    return NotFound(new { message = "Employee not found." });

                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving employee."
                });
            }
        }

        // 3. POST: api/Employee
        // Create employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(
            [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                employee.name = employee.name.Trim();
                employee.contact = employee.contact.Trim();
                employee.email = employee.email.Trim();
                employee.city = employee.city.Trim();
                employee.state = employee.state.Trim();
                employee.pincode = employee.pincode.Trim();
                employee.altContactNo = employee.altContactNo.Trim();
                employee.address = employee.address.Trim();

                // Unique contact check
                if (await _context.Employees.AnyAsync(
                    e => e.contact == employee.contact))
                {
                    return Conflict(new
                    {
                        message = "Contact number already exists."
                    });
                }

                // Unique email check (case-insensitive)
                if (await _context.Employees.AnyAsync(
                    e => e.email.ToLower() == employee.email.ToLower()))
                {
                    return Conflict(new
                    {
                        message = "Email already exists."
                    });
                }

                employee.createdDate = DateTime.UtcNow;
                employee.modifiedDate = DateTime.UtcNow;

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetEmployeeById),
                    new { id = employee.employeeId },
                    employee);
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message = "Could not save employee. Contact or email may already exist."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Error creating employee."
                });
            }
        }

        // 4. PUT: api/Employee/5
        // Update employee
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            [FromBody] Employee employee)
        {
            try
            {
                if (id != employee.employeeId)
                {
                    return BadRequest(new
                    {
                        message = "Route ID and employee ID do not match."
                    });
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existing = await _context.Employees
                    .FirstOrDefaultAsync(e => e.employeeId == id);

                if (existing == null)
                    return NotFound(new { message = "Employee not found." });

                employee.name = employee.name.Trim();
                employee.contact = employee.contact.Trim();
                employee.email = employee.email.Trim();
                employee.city = employee.city.Trim();
                employee.state = employee.state.Trim();
                employee.pincode = employee.pincode.Trim();
                employee.altContactNo = employee.altContactNo.Trim();
                employee.address = employee.address.Trim();

                // Unique contact check, excluding current employee
                if (await _context.Employees.AnyAsync(
                    e => e.employeeId != id &&
                         e.contact == employee.contact))
                {
                    return Conflict(new
                    {
                        message = "Contact number already exists."
                    });
                }

                // Unique email check, excluding current employee
                if (await _context.Employees.AnyAsync(
                    e => e.employeeId != id &&
                         e.email.ToLower() == employee.email.ToLower()))
                {
                    return Conflict(new
                    {
                        message = "Email already exists."
                    });
                }

                existing.name = employee.name;
                existing.contact = employee.contact;
                existing.email = employee.email;
                existing.city = employee.city;
                existing.state = employee.state;
                existing.pincode = employee.pincode;
                existing.altContactNo = employee.altContactNo;
                existing.address = employee.address;
                existing.designationId = employee.designationId;
                existing.modifiedDate = DateTime.UtcNow;

                // createdDate remains unchanged
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Employee updated successfully.",
                    data = existing
                });
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message = "Could not update employee. Contact or email may already exist."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Error updating employee."
                });
            }
        }

        // 5. DELETE: api/Employee/5
        // Delete employee
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.employeeId == id);

                if (employee == null)
                    return NotFound(new { message = "Employee not found." });

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Employee deleted successfully."
                });
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message = "Employee cannot be deleted because related records exist."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Error deleting employee."
                });
            }
        }

        // 6. GET: api/Employee/filter
        // Filter + sort + pagination
        // Example:
        // api/Employee/filter?city=Mumbai&state=Maharashtra
        // &designationId=2&sortBy=name&sortOrder=asc&pageNumber=1&pageSize=10
        [HttpGet("filter")]
        public async Task<IActionResult> FilterEmployees(
            [FromQuery] string? name,
            [FromQuery] string? city,
            [FromQuery] string? state,
            [FromQuery] int? designationId,
            [FromQuery] string? sortBy = "employeeId",
            [FromQuery] string? sortOrder = "asc",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
                {
                    return BadRequest(new
                    {
                        message = "Page number must be positive and page size must be between 1 and 100."
                    });
                }

                if (designationId.HasValue && designationId.Value <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Designation ID must be greater than zero."
                    });
                }

                var query = _context.Employees
                    .AsNoTracking()
                    .AsQueryable();

                // Filters
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var searchName = name.Trim();
                    query = query.Where(e =>
                        e.name.Contains(searchName));
                }

                if (!string.IsNullOrWhiteSpace(city))
                {
                    var searchCity = city.Trim();
                    query = query.Where(e =>
                        e.city.Contains(searchCity));
                }

                if (!string.IsNullOrWhiteSpace(state))
                {
                    var searchState = state.Trim();
                    query = query.Where(e =>
                        e.state.Contains(searchState));
                }

                if (designationId.HasValue)
                {
                    query = query.Where(e =>
                        e.designationId == designationId.Value);
                }

                // Total records before pagination
                var totalRecords = await query.CountAsync();

                // Sorting (allow-listed columns only)
                var ascending = string.Equals(
                    sortOrder, "asc",
                    StringComparison.OrdinalIgnoreCase);

                var descending = string.Equals(
                    sortOrder, "desc",
                    StringComparison.OrdinalIgnoreCase);

                if (!ascending && !descending)
                {
                    return BadRequest(new
                    {
                        message = "sortOrder must be 'asc' or 'desc'."
                    });
                }

                switch (sortBy?.Trim().ToLowerInvariant())
                {
                    case "name":
                        query = ascending
                            ? query.OrderBy(e => e.name)
                            : query.OrderByDescending(e => e.name);
                        break;

                    case "city":
                        query = ascending
                            ? query.OrderBy(e => e.city)
                            : query.OrderByDescending(e => e.city);
                        break;

                    case "state":
                        query = ascending
                            ? query.OrderBy(e => e.state)
                            : query.OrderByDescending(e => e.state);
                        break;

                    case "email":
                        query = ascending
                            ? query.OrderBy(e => e.email)
                            : query.OrderByDescending(e => e.email);
                        break;

                    case "createdDate":
                        query = ascending
                            ? query.OrderBy(e => e.createdDate)
                            : query.OrderByDescending(e => e.createdDate);
                        break;

                    case "employeeId":
                    case null:
                    case "":
                        query = ascending
                            ? query.OrderBy(e => e.employeeId)
                            : query.OrderByDescending(e => e.employeeId);
                        break;

                    default:
                        return BadRequest(new
                        {
                            message = "Invalid sortBy field. Allowed: employeeId, name, city, state, email, createdDate."
                        });
                }

                // Pagination
                var employees = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new
                {
                    totalRecords,
                    pageNumber,
                    pageSize,
                    totalPages = (int)Math.Ceiling(
                        totalRecords / (double)pageSize),
                    data = employees
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Error filtering employees."
                });
            }
        }

     
// POST: api/Employee/login
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _context.Employees
                    .FirstOrDefaultAsync(x =>x.email== model.email && x.contact == model.contactNo);

                if (user == null)
                {
                    return Unauthorized(new {message = "Invalid Credentials"});
                }
           
                return Ok(new
                {
                    message = "Login successful.",
                    data = new
                    {
                        user.employeeId,
                        user.name,
                        user.email,
                        user.designationId,
                        user.role
                    }
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred during login."
                });
            }
        }

    }
}
