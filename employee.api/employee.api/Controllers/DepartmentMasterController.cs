using employee.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employee.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentMasterController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public DepartmentMasterController(EmployeeDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllDepartments")]
        public IActionResult GetDepartments()
        {
            var deptList = _context.Departments.ToList();
            return Ok(deptList);
        }
        [HttpPost("AddDepartment")]

        public IActionResult AddDepartment([FromBody] Department dept)
        {
            if (dept == null)
            {
                return BadRequest("Department payload is required.");
            }

            if (string.IsNullOrWhiteSpace(dept.departmentName))
            {
                return BadRequest("Department name is required.");
            }

            var name = dept.departmentName.Trim();

            // Check uniqueness (case-insensitive)
            var exists = _context.Departments
                .Any(d => d.departmentName != null && d.departmentName.ToLower() == name.ToLower());

            if (exists)
            {
                return Conflict("Department name must be unique.");
            }

            dept.departmentName = name;
            _context.Departments.Add(dept);
            _context.SaveChanges();
            return Created("Department Added Successfully", dept);
        } 
        [HttpPut("UpdateDepartment")]
        public IActionResult UpdateDepartment([FromBody] Department dept)
        {
            var existingDept = _context.Departments.Find(dept.departmentId);
            if (existingDept == null)
            {
                return NotFound("Department not found");
            }
            existingDept.departmentName = dept.departmentName;
            existingDept.isActive = dept.isActive;
            _context.SaveChanges();
            return Created("Department Updated Successfully", dept);
        }
        [HttpDelete("DeleteDepartment/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var existingDept = _context.Departments.Find(id);
            if (existingDept == null)
            {
                return NotFound("Department not found");
            }
            _context.Departments.Remove(existingDept);
            _context.SaveChanges();
            return Created("Department deleted Successfully", existingDept);
        }
    }
}
