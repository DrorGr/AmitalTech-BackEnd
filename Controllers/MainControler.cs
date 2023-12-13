using AmitalBE.Models;
using AmitalBE.Request;
using AmitalBE.Services;
using Microsoft.AspNetCore.Mvc;
using Task = AmitalBE.Models.Task;

namespace AmitalBE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;
        private readonly MainLogic _service;

        public MainController ( ILogger<MainController> logger, MainLogic service )
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers ( )
        {

            return _service.GetUsers();
        }

        [HttpGet("GetTasks")]
        public IEnumerable<Task> GetTasks ( int? id )
        {

            return _service.GetTasks(id);
        }

        [HttpGet("GetTomorowTasks")]
        public IEnumerable<Task> GetTomorowTasks ( )
        {
            return _service.GetTomorowTasks();
        }

        [HttpPost("AddTask")]
        public IActionResult AddTask ( [FromBody] NewTaskRequest task )
        {
            if (task == null)
            {
                return BadRequest("Bad input, check fields");
            }

            var result = _service.AddTask(task);

            if (result.Success)
            {
                return Ok("Task added successfully.");
            }
            else
            {
                var errorResponse = new
                {
                    Title = "Error Adding Task",
                    Errors = new string[] { result.ErrorMessage }
                };
                return BadRequest(errorResponse);
            }
        }
    }
}

