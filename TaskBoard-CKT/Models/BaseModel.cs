using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBoard_CKT.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreatedAt = DateTime.Now;
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        public DateTime? UpdatedAt
        {
            get;
            set;
        }
    }
}
