using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class UserService : IUserRepository
    {
        private readonly BlogDbContext db;

        public UserService(BlogDbContext dbContext)
        {
                db = dbContext;
        }
        public async Task<User?> GetUserAysnc(string username, string password)
        {
            if(username != null && password != null)
            {
                return await db.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);   
            }
            else
            {
                return null;
            }
        }
    }
}
