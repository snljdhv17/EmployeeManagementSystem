using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace employee.api.Models
{
    [Table("employeeTbl")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }
        [Required, MaxLength(50)]
        public String name { get; set; } = string.Empty;
        [Required, MaxLength(10)]
        public string contact { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string email { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string city { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string state { get; set; } = string.Empty;
        [Required, MaxLength(6)]
        public string pincode { get; set; } = string.Empty;
        [Required, MaxLength(10)]
        public string altContactNo { get; set; } = string.Empty;
        [Required, MaxLength(2000)]
        public string address { get; set; } = string.Empty;
        public int designationId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }

        public string role { get; set; } = string.Empty;

    }
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        public string contactNo { get; set; } = string.Empty;
    }
}
