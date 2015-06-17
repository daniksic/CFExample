using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Claudia.Domain.Models.v3
{
    public class User : IdentityUser
    {
        public DateTime LastLogInDate { get; set; }

        public User()
            : base()
        {
            LastLogInDate = DateTime.Now;
        }

        public User(string userName)
            : base(userName)
        {
            LastLogInDate = DateTime.Now;
        }
    }
}