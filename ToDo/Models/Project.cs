using System;
using System.Collections.Generic;

namespace ToDo.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime CompleteDateTime { get; set; }
        public StatusProjectEnum Status { get; set; }
        public int Priority { get; set; }
        public List<Task> Tasks { get; set; }
    }
}