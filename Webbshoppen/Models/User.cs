using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }
        public int BirthDay { get; set; }
        public string Email { get; set; } //TODO: Unique i DB-context
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
