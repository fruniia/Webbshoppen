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
        public string EMail { get; set; } //TODO: Unique i DB-context
        public string Password { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
