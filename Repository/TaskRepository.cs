using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.EntityFramework;
using WebApiProj.Interfaces.Repositories;
using WebApiProj.Models;

namespace WebApiProj.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly WebApiContext _dbContext;
        private readonly IUserTaskRepository _userTaskRepository;

        public TaskRepository(WebApiContext dbContext,
            IUserTaskRepository userTaskRepository)
        {
            _dbContext = dbContext;
            _userTaskRepository = userTaskRepository;
        }

        public async Task AssignUserToTask(int userId, int taskId)
        {
            var newUserTask = new UserTask
            {
                TaskId = taskId,
                UserId = userId,
            };

            var userTask = _userTaskRepository.Create(newUserTask);
        }

        public async Task<TaskItem> Create(TaskItem model)
        {
            var task = await _dbContext.Tasks.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return task.Entity;
        }

        public async Task Delete(TaskItem model)
        {
            _dbContext.Tasks.Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TaskItem> Get(int id)
        {
            var task = _dbContext.Tasks.FirstOrDefault(x => x.Id == id);
            return task;
        }

        public async Task<List<TaskItem>> GetAll()
        {
            var tasks = _dbContext.Tasks;
            return tasks.ToList();
        }

        public async Task<List<User>> GetUsersForTask(int taskId)
        {
            var userTasks = await _userTaskRepository.GetAllForTask(taskId);
            var userList = userTasks.Select(x => x.User).ToList();
            return userList;
        }

        public async Task<TaskItem> Update(TaskItem newModelInfo, int id)
        {
            var task = _dbContext.Tasks.Update(newModelInfo);
            await _dbContext.SaveChangesAsync();
            return task.Entity;
        }
    }
}
