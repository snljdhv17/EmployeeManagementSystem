using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace employee.api.Models
{

    [Table("departmentTbl")]
public class Department
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int departmentId { get; set; }
    [Required, MaxLength(50)]
    public String departmentName { get; set; } = string.Empty;

    public Boolean isActive { get; set; }


    }
}

