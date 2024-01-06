using GerenciadorDeTarefas.Models.Users;
using GerenciadorDeTarefas.Repositories;

namespace GerenciadorDeTarefas
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserManager.InitializeFile();


            Console.WriteLine("Bem-vindo ao Sistema de Gerenciamento de Tarefas!");

            Console.Write("Digite seu email: ");
            string email = Console.ReadLine();

            Console.Write("Digite sua senha: ");
            string password = Console.ReadLine();


            User authenticatedUser = UserManager.AuthenticateUser(email, password);


            if (authenticatedUser != null)
            {
                Console.WriteLine($"Usuário autenticado com sucesso. Usuario: {authenticatedUser.GetType()}");
            }
            else
            {
                Console.WriteLine("Falha na autenticação. Email ou senha incorretos.");
            }
        }
    }
}
