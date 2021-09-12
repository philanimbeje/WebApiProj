using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiProj.Interfaces.Models;
using WebApiProj.Interfaces.Repositories;
using WebApiProj.Models;

namespace WebApiProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTaskController : ControllerBase
    {
        private readonly ILogger<UserTaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public UserTaskController(ILogger<UserTaskController> logger,
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            IUserTaskRepository userTaskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _userTaskRepository = userTaskRepository;
        }
    }
}
