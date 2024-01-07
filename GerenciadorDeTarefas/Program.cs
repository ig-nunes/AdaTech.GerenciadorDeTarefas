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


            Console.WriteLine("Bem-vindo ao Sistema de Gerenciamento de Tarefas!");

            while (true)
            {
                Console.Write($"O que você deseja fazer?\n" +
                    $"  1 - Fazer Login\n" +
                    $"  2 - Cadastrar novo usuário;\n" +
                    $"  3 - Sair\n");
                string? escolha = Console.ReadLine();

                switch (escolha)
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
                        Register();
                        Console.WriteLine("Usuário Criado com sucesso!");
                        Console.ReadKey();
                        break;
                    case "3":
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

        private static void Register()
        {
            while (true)
            {
                User? user;
                Console.Write("Digite seu nome: ");
                string name = Console.ReadLine()!;

                Console.Write("Digite seu email: ");
                string email = Console.ReadLine()!;

                Console.Write("Digite uma senha: ");
                string password = Console.ReadLine()!;

                UserType? userType = null;

                var continuar = true;

                while (continuar)
                {
                    Console.Write($"Escolha o tipo de usuário:\n" +
                        $"  1 - Tech Leader;\n" +
                        $"  2 - Developer;\n");
                    string type = Console.ReadLine()!;

                    switch(type)
                    {
                        case "1":
                            userType = UserType.TechLeader;
                            continuar = false;
                            break;
                        case "2":
                            userType = UserType.Developer;
                            continuar = false;
                            break;
                        default:
                            Console.WriteLine("Erro: Valor não válido.");
                            Console.ReadKey();
                            break;
                    }
                    Console.Clear();
                }

                Console.WriteLine($"Usuário:\n" +
                    $"Nome: {name}\n" +
                    $"E-mail: {email}\n" +
                    $"Password: {password}" +
                    $"Tipo: {userType.ToString()}");

                Console.ReadKey();

                string hashPassword = HashClass.SetHashPassword(password);

                if (userType == UserType.TechLeader)
                {
                    user = new TechLeader(name, email, hashPassword);
                    UserManager.SaveUser(user);
                }
                else if (userType == UserType.Developer)
                {
                    user = new Developer(name, email, hashPassword);
                    UserManager.SaveUser(user);
                }
                break;
            }
            
        }
    }
}
