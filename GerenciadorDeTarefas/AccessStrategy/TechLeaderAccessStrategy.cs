using GerenciadorDeTarefas.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.AccessStrategy
{
    public class TechLeaderAccessStrategy : IAccessStrategy
    {
        public void Access(User user)
        {
            Console.WriteLine("TechLeader Access");
            Console.ReadKey();

            while (true) 
            {
                Console.WriteLine($"Olá, {user.Name}. Escolha o que fazer:\n" +
                    $"  1 - Visualizar todas as tarefas;\n" +
                    $"  2 - Criar nova tarefa;\n" +
                    $"  3 - Visualizar todos os Desenvolvedores;\n" +
                    $"  4 - Fazer Logout\n"
                    );
            }

        }
    }
}
