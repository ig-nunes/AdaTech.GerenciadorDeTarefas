using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Models.Users
{
    public abstract class User
    {
        private string _name;
        private string _email;
        private string _password;

        public string Name { get { return _name; } }
        public string Email { get { return _email; } }
        public string Password { get { return _password; } }

        public User(string name, string email, string password) 
        {
            this._name = name;
            this._email = email;
            this._password = password;
        }

        public void UpdatePassword(string password)
        {
            _password = password;
        }

        public void UpdateEmail(string email)
        {
            _email = email;
        }

        public void UpdateName(string name)
        {
            _name = name;
        }

    }
}
