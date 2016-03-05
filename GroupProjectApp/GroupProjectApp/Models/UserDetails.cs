using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectApp.Models
{

    //public class UserDetails
    //{
    //    public string UserID { get; set; }
    //    public string Email { get; set; }
    //    public string StudentID { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public bool Notifications { get; set; }
    //    public byte PrivacyLevel { get; set; }
    //    public IList<string> Roles { get; set; }
    //}

    //public class Example
    //{
    //    public IList<UserDetails> userDetails { get; set; }
    //}

    public class UserDetails
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Notifications { get; set; }
        public byte? PrivacyLevel { get; set; }
        public List<string> Roles { get; set; }

    }
    //public class RootObject
    //{
    //    public List<UserDetails> userDetails { get; set; }
    //}
}
