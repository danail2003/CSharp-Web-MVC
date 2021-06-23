namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class UsersService: IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string username, string email, string password, string userType)
        {
            this.dbContext.Users.Add(new User
            {
                Username = username,
                Email = email,
                Password = Hash(password),
                IsMechanic = userType == "Mechanic" ? true : false,
            });

            this.dbContext.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            return this.dbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == Hash(password)).Id;
        }

        public bool IsUserMechanic(string Userid)
        {
            return this.dbContext.Users.FirstOrDefault(x => x.Id == Userid).IsMechanic;
        }

        public bool IsUsernameAvailable(string username)
        {
            return this.dbContext.Users.Any(x => x.Username == username);
        }

        private static string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
