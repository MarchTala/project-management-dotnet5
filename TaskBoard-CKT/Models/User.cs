using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBoard_CKT.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
