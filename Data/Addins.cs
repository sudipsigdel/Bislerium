using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Data
{

    public class Addins
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AddinsType { get; set; }
        public double Price { get; set; }

    }
}
