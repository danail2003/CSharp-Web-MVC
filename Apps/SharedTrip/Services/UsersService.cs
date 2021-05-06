using System;
using System.Linq;
using System.Text;
using SharedTrip.Data;
using SharedTrip.ViewModels;

namespace SharedTrip.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string GetUserId(string username, string password)
        {
            return this.db.Users.FirstOrDefault(x => x.Username == username && x.Password == Hash(password)).Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return this.db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAndPasswordMatch(string username, string password)
        {
            return this.db.Users.Any(x => x.Username == username && x.Password == Hash(password));
        }

        public bool IsUsernameAvailable(string username)
        {
            return this.db.Users.Any(x => x.Username == username);
        }

        public void Register(RegisterInputModel model)
        {
            this.db.Users.Add(new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = Hash(model.Password)
            });

            this.db.SaveChanges();
        }

        private string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
