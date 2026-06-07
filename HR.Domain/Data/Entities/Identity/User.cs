using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities.Identity
{
    public class User : IdentityUser
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
