using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Data
{
    public class User
    {
        public string UserName { get; set; }

        public Role Role { get; set; }

        public string Password { get; set; }

        public bool HasInitialPasswordChanged { get; set; } = false;
    }
}