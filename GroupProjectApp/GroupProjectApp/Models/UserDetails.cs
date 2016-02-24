using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectApp.Models
{
    public class UserDetails
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string String { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Boolean Notifications { get; set; }
        public byte PrivacyLevel { get; set; }
        public List<string> Roles { get; set; }
    }
}
