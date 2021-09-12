using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.Interfaces.Models;
using WebApiProj.Models;

namespace WebApiProj.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAll();
        Task<TaskItem> Get(int id);
        Task<TaskItem> Create(TaskItem model);
        Task<TaskItem> Update(TaskItem newModelInfo, int id);
        Task Delete(TaskItem model);

        Task<List<User>> GetUsersForTask(int taskId);
        Task AssignUserToTask(int userId, int taskId);
    }
}
