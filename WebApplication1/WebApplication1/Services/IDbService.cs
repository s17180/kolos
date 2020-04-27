using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Responses;
using WebApplication1.Models;
using Task = WebApplication1.Models.Task;

namespace WebApplication1.Services
{
    public interface IDbService
    {
        public bool AddTask(TaskRequest animal);

        public List<Task> GetTasks(int id);
    }
}
