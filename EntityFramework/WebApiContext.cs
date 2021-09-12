using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.Models;
using TaskItem = WebApiProj.Models.TaskItem;

namespace WebApiProj.EntityFramework
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options)
        : base(options)
        {
            SeedData();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        private void SeedData()
        {
            //Users.Add(new User { Name = "Philani", Surname = "Mbeje" });
            //Tasks.Add(new TaskItem {Title = "First Task", Description = "This is the first task" });
            //UserTasks.Add(new UserTask { TaskId = 1, UserId = 1 });
            //SaveChanges();
        }
    }
}
