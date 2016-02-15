using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace publicAutoTutorWebApi.Models
{
    public class Users
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public int Role { get; set; }
        public Nullable<System.DateTime> RegistrationTime { get; set; }
        public Nullable<System.DateTime> ApprovalTime { get; set; }
        public string Password { get; set; }
    }
}