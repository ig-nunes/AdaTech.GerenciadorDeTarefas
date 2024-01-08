using GerenciadorDeTarefas.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.AccessStrategy
{
    public interface IAccessStrategy
    {
        public void Access(User user);
    }
}
