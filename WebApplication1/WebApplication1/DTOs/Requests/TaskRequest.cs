using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTOs.Responses
{
    public class TaskRequest
    {
        [Required]
        public int IdTask { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public string Deadline { get; set; }
        [Required]

        public int IdProject { get; set; }
        [Required]

        public int IdTaskType { get; set; }
        [Required]

        public int IdAssignedTo { get; set; }
        [Required]
        public int IdCreator { get; set; }

        public int newId { get; set; }
        

        public string taskNAme { get; set; }


    }
}
