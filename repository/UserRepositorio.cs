using APIClientes.Data;
using APIClientes.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace APIClientes.repository
{
    public class UserRepositorio : IUserRepositorio
    {

        private readonly ApplicationDbContext _db;

        public UserRepositorio(ApplicationDbContext db)
        {
                _db = db;
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(userName.ToLower()));
            if (user == null)
            {
                return "nouser";
            }
            else if (!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return "wrongpassword";
            }
            else
            {
                return "ok";
            }
        
        }

        public async Task<int> Register(User user, string password)
        {
            try
            {
                if (await UserExist(user.UserName))
                {
                    return -1;
                }

                CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception ex)
            {
                return -500;
            }
        }

        public async Task<bool> UserExist(string userName)
        {
            if (await _db.Users.AnyAsync(x => x.UserName.ToLower().Equals(userName.ToLower())))
            {
                return true;
            }
            return false;
        }


        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); 

            }
        }

        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

    }
}
