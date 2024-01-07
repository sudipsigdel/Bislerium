using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Data
{
    public class UserServices
    {

        private List<User> _seedUsersList = new()
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

        public User loginCheck(String Password)
        {
            User user = _seedUsersList.FirstOrDefault(_user => _user.Password == Password);
            if (user == null)
            {
                throw new Exception("Invalid Password");

            }
            return user;

        }


    }
}