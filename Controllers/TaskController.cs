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
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public TaskController(ILogger<TaskController> logger, 
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
        public async Task<List<TaskItem>> GetAll()
        {
            try
            {
                return await _taskRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("{id}")]
        public async Task<TaskItem> Get(int id)
        {
            try
            {
                return await _taskRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}/Users")]
        public async Task<List<User>> GetUsersForTask(int id)
        {
            try
            {
                var task = await _taskRepository.Get(id);
                var userTasks = await _userTaskRepository.GetAllForTask(task.Id);

                List<User> users = new List<User>();
                foreach (var userTask in userTasks)
                {
                    var user = await _userRepository.Get(userTask.TaskId);
                    users.Add(user);
                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Users/{UserId}")]
        public async Task<List<TaskItem>> GetTasksForUser(int UserId)
        {
            try
            {
                var user = await _userRepository.Get(UserId);
                var userTasks = await _userTaskRepository.GetAllForUser(user.Id);

                List<TaskItem> tasks = new List<TaskItem>();
                foreach (var userTask in userTasks)
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

        [HttpPost]
        public async Task<TaskItem> Create([FromBody] TaskItem model)
        {
            try
            {
                return await _taskRepository.Create(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<TaskItem> Update(int id, [FromBody] TaskItem model)
        {
            try
            {
                return await _taskRepository.Update(model, id);
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
                var task = await _taskRepository.Get(id);
                if (task != null)
                {
                    var userTasks = await _userTaskRepository.GetAllForTask(id);
                    foreach (var userTask in userTasks)
                    {
                        await _userTaskRepository.Delete(userTask);
                    }
                    await _taskRepository.Delete(task);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
