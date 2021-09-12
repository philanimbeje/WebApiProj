using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.EntityFramework;
using WebApiProj.Interfaces.Models;
using WebApiProj.Interfaces.Repositories;
using WebApiProj.Models;

namespace WebApiProj.Repository
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly WebApiContext _dbContext;
        public UserTaskRepository(WebApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserTask> Create(UserTask model)
        {
            var userTask = await _dbContext.UserTasks.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return (UserTask)userTask;
        }

        public async System.Threading.Tasks.Task Delete(UserTask model)
        {
           _dbContext.UserTasks.Remove(model);
           await  _dbContext.SaveChangesAsync();
        }

        public async Task<UserTask> Get(int id)
        {
            var userTask =  _dbContext.UserTasks.FirstOrDefault(x => x.Id == id);
            return userTask;
        }

        public async Task<List<UserTask>> GetAllForTask(int taskId)
        {
            var userTasks = _dbContext.UserTasks.Where(x => x.TaskId == taskId)
                .Include(x => x.User);
            return userTasks.ToList();
        }

        public async Task<List<UserTask>> GetAllForUser(int userId)
        {
            var userTasks = _dbContext.UserTasks.Where(x => x.UserId == userId)
                .Include(x=>x.Task);
            return userTasks.ToList();
        }

        public async Task<UserTask> Update(UserTask newModelInfo, int id)
        {
            var userTask = _dbContext.UserTasks.Update(newModelInfo);
            await _dbContext.SaveChangesAsync();
            return (UserTask)userTask;
        }
    }
}
