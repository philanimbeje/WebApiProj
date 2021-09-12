using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProj.Interfaces.Models;

namespace WebApiProj.Models
{
    public class User: IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public static explicit operator User(EntityEntry<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
