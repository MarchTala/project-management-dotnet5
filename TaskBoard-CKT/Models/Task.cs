using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskBoard_CKT.Models
{
    public class Task : BaseModel
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public Type Type { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Assignee { get; set; }

        public int CreatedBy { get; set; }

        public string Tags { get; set; } //Screen #

        [DisplayName("Date Estimated")]
        public DateTime DateEstimated { get; set; }

        [DisplayName("Date Completed")]
        public DateTime DateCompleted { get; set; }

    }

    public enum Priority
    {
        Critical,
        Urgent,
        High,
        Mid,
        Low
    }

    public enum Type
    {
        NewFeatures,
        Bug,
        Improvements
    }

    public enum Status
    {
        Open,
        InProgress,
        ForTesting,
        ForUpload,
        Completed,
        Hold
    }
}
