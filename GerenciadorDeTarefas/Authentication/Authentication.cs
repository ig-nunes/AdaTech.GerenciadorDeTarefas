using GerenciadorDeTarefas.Models.Users;
using GerenciadorDeTarefas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Authentication
{
    public static class Authentication
    {
        public static User? Login()
        {
            Console.Write("Digite seu email: ");
            string email = Console.ReadLine();

            Console.Write("Digite sua senha: ");
            string password = Console.ReadLine();


            User authenticatedUser = UserManager.AuthenticateUser(email, password);


            if (authenticatedUser != null)
            {
                Console.WriteLine($"Usuário autenticado com sucesso!");
                Thread.Sleep(2000);
                return authenticatedUser;
            }
            else
            {
                Console.WriteLine("Falha na autenticação. Email ou senha incorretos.");
                Thread.Sleep(2000);
                return null;
            }
        }
    }
}
