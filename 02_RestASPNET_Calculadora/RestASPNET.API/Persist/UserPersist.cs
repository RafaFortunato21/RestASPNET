using RestASPNET.API.Context;
using RestASPNET.API.Data.DTO;
using RestASPNET.API.Model;
using RestASPNET.API.Persist.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace RestASPNET.API.Persist
{
    public class UserPersist : IUserPersist
    {
        private readonly MySqlContext _context;

        public UserPersist(MySqlContext context)
        {
            _context = context;
        }


        public User ValidateCredentials(string userName, string password)
        {
            var pass = GetHash(SHA256.Create(), password);

            return _context.User.FirstOrDefault(user => user.UserName == userName
                                                     && user.Password == pass);

        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }




        private string GetHash(HashAlgorithm algorithm, string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(data);

            var sBuilder = new StringBuilder();

            foreach (byte item in hashedBytes)
            {
                sBuilder.Append(item.ToString("x2"));
            }

            return BitConverter.ToString(hashedBytes);

        }

        public bool UserExists(User user)
        {
            return !_context.User.Any(u => u.Id.Equals(user.Id));
        }


    }
}
