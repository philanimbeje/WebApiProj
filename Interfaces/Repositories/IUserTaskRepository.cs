using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.Interfaces.Models;
using WebApiProj.Models;

namespace WebApiProj.Interfaces.Repositories
{
    public interface IUserTaskRepository
    {
        Task<List<UserTask>> GetAllForUser(int userId);
        Task<List<UserTask>> GetAllForTask(int taskId);
        Task<UserTask> Get(int id);
        Task<UserTask> Create(UserTask model);
        Task<UserTask> Update(UserTask newModelInfo, int id);
        System.Threading.Tasks.Task Delete(UserTask mode);
    }
}
