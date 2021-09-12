using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.Interfaces.Models;

namespace WebApiProj.Models
{
    public class UserTask: IUserTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCompleted { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("TaskId")]
        public virtual TaskItem Task { get; set; }

        public static explicit operator UserTask(EntityEntry<UserTask> v)
        {
            throw new NotImplementedException();
        }
    }
}
