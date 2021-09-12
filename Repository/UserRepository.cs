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
    public class UserRepository : IUserRepository
    {
        private readonly WebApiContext _dbContext;
        private readonly IUserTaskRepository _userTaskRepository;

        public UserRepository(WebApiContext dbContext,
            IUserTaskRepository userTaskRepository)
        {
            _dbContext = dbContext;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<User> Create(User model)
        {
            var user = await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return user.Entity;
        }

        public async Task Delete(User model)
        {
            _dbContext.Users.Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> Get(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            var users = _dbContext.Users;
            return users.ToList();
        }

        public async Task<List<TaskItem>> GetUserTasks(int userId)
        {
            var userTasks = await _userTaskRepository.GetAllForUser(userId);
            var taskList = userTasks.Select(x => x.Task).ToList();
            return taskList;
        }

        public async Task<User> Update(User newModelInfo, int id)
        {
            var user = _dbContext.Users.Update(newModelInfo);
            await _dbContext.SaveChangesAsync();
            return user.Entity;
        }
    }
}
