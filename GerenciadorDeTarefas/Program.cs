using GerenciadorDeTarefas.Models.Users;
using GerenciadorDeTarefas.Repositories;
using GerenciadorDeTarefas.Authentication;
using GerenciadorDeTarefas.Utils;

namespace GerenciadorDeTarefas
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserManager.InitializeFile();
            AssignmentManager.InitializeFile();


            Console.WriteLine("Bem-vindo ao Sistema de Gerenciamento de Tarefas!");

            while (true)
            {
                Console.Write($"O que você deseja fazer?\n" +
                    $"  1 - Fazer Login\n" +
                    $"  2 - Sair\n");
                string? selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        var authenticatedUser = Authentication.Authentication.Login();

                        if (authenticatedUser != null)
                        {
                            Console.Clear();
                            authenticatedUser.GetAccessStrategy().Access(authenticatedUser);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Ocorreu algum erro ao fazer o login. Retornando para o menu...");
                        }

                        break;
                    case "2":
                        Console.WriteLine("Saindo do sistema...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Erro: não foi possível identificar.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            }

        }
    }
}
