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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public UserController(ILogger<UserController> logger,
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            IUserTaskRepository userTaskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _userTaskRepository = userTaskRepository;
        }

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            try
            {
                return await _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            try
            {
                return await _userRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}/Tasks")]
        public async Task<List<TaskItem>> GetTasksForUser(int id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                var userTasks = await _userTaskRepository.GetAllForUser(user.Id);

                List<TaskItem> tasks = new List<TaskItem>();
                foreach(var userTask in userTasks)
                {
                    var task = await _taskRepository.Get(userTask.TaskId);
                    tasks.Add(task);
                }
                return tasks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Tasks/{TaskId}")]
        public async Task<List<User>> GetUsersForTask(int TaskId)
        {
            try
            {
                var task = await _taskRepository.Get(TaskId);
                var userTasks = await _userTaskRepository.GetAllForTask(task.Id);

                List<User> users = new List<User>();
                foreach (var userTask in userTasks)
                {
                    var user = await _userRepository.Get(userTask.UserId);
                    users.Add(user);
                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<User> Create([FromBody] User model)
        {
            try
            {
                return await _userRepository.Create(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("AssignTask")]
        public async Task<UserTask> AssignUserToTask([FromBody] UserTask model)
        {
            try
            {
                return await _userTaskRepository.Create(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("UnassignTask")]
        public async Task UnassignUserFromTask([FromBody] UserTask model)
        {
            try
            {
                await _userTaskRepository.Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<User> Update(int id, [FromBody] User model)
        {
            try
            {
                return await _userRepository.Update(model, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                if (user != null)
                {
                    var userTasks = await _userTaskRepository.GetAllForUser(id);
                    foreach(var userTask in userTasks)
                    {
                        await _userTaskRepository.Delete(userTask);
                    }
                    await _userRepository.Delete(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
