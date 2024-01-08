using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Data
{
    public class UserServices
    {

        private List<User> _UsersList = new()
        {
            new User()
            {
                Password = "admin",
                Role = Role.Admin,
            },

            new User()
            {
                Password = "staff",
                Role = Role.Staff
            }
        };

        public User loginCheck(string Password)
        {
            User user = _UsersList.FirstOrDefault(_user => _user.Password == Password);
            if (user == null)
            {
                throw new Exception("Invalid Password");

            }
            return user;

        }


    }
}