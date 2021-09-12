using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.Interfaces.Models;
using WebApiProj.Models;

namespace WebApiProj.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Get(int id);
        Task<User> Create(User model);
        Task<User> Update(User newModelInfo, int id);
        System.Threading.Tasks.Task Delete(User model);

        Task<List<WebApiProj.Models.TaskItem>> GetUserTasks(int userId);
    }
}
