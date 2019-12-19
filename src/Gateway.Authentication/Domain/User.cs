using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Authentication.Domain
{
    public class User
    {
        protected User() { }

        public User(string login, string name)
        {
            Login = login;
            Name = name;
        }

        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Salt { get; set; }

        public string PasswordHash { get; set; }
    }
}
