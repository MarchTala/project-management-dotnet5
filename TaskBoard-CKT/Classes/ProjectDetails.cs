using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard_CKT.Classes
{
    public class ProjectDetails
    {
        public Guid ProjectGUID { get; set; }
        public string ProjectName { get; set; }
        public List<TaskBoard_CKT.Models.Task> ProjectTaskList { get; set; }
    }
}
