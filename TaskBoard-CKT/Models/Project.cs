using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBoard_CKT.Models
{
    public class Project : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
