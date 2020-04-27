using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Responses;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/projects")] 
    public class ProjectsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ProjectsController(IDbService dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetProjects(int id)
        {
            var result = _dbService.GetTasks(id);
            if (result == null)
            {
                ObjectResult res = new ObjectResult(result);
                res.StatusCode = 400;
                return res;
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddTask(TaskRequest request)
        {


            var res = _dbService.AddTask(request);
            if (res == null)
                return BadRequest();
            return Created("", res);

        }
    }