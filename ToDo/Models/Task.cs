using System;

namespace ToDo.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusTaskEnum Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}