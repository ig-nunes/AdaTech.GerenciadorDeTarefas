using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Models.Users
{
    internal class Developer : User
    {
        public Developer(string name, string email, string password) : base(name, email, password)
        {
        }
    }
}
