using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Models.Users
{
    public class TechLeader : User
    {
        public override UserType UserType => UserType.TechLeader;
        public TechLeader(string name, string email, string password) : base(name, email, password)
        {
        }
    }
}
