using GerenciadorDeTarefas.Models.Tasks;
using GerenciadorDeTarefas.Models.Users;
using GerenciadorDeTarefas.Repositories;
using GerenciadorDeTarefas.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GerenciadorDeTarefas.AccessStrategy
{
    public class TechLeaderAccessStrategy : IAccessStrategy
    {
        public void Access(User user)
        {
            var condition = true;

            while (condition)
            {
                Console.WriteLine($"Olá, {user.Name}. Escolha o que fazer:\n" +
                    $"  1 - Editar tarefas;\n" +
                    $"  2 - Criar nova tarefa;\n" +
                    $"  3 - Visualizar todos os Desenvolvedores;\n" +
                    $"  4 - Adicionar Desenvolvedor;\n" +
                    $"  5 - Fazer Logout\n"
                    );
                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        ViewAllTasks();
                        break;
                    case "2":
                        CreateNewAssignment();
                        break;
                    case "3":
                        break;
                    case "4":
                        Register();
                        Console.WriteLine("Usuário Criado com sucesso!");
                        Console.ReadKey();
                        break; ;
                    case "5":
                        Console.WriteLine("Voltando menu inicial...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        return;
                    default:
                        break;
                }
            }
        }

        private static void CreateNewAssignment()
        {
            Console.WriteLine("Digite o nome da nova tarefa: ");
            string name = Console.ReadLine();

            Console.WriteLine("Digite a descrição da nova tarefa: ");
            string description = Console.ReadLine();

            Assignment newAssignment = new Assignment(name, description);
            AssignmentManager.SaveNewAssignment(newAssignment);
            Console.WriteLine("Nova tarefa criada com sucesso!");
        }

        private static void ViewAllTasks()
        {
            Console.WriteLine("Todas as tarefas:\n");
            var listTasks = AssignmentManager.GetAllAssignments();
            int number = 0;

            foreach (var task in listTasks)
            {
                var name = task.Name;
                var statusTaks = task.Status;
                var description = task.Description;
                var user = task.Dev != null ? task.Dev.ToString() : "Nenhum usuário atribuído";
                var initDate = task.InitDate != null ? task.InitDate.ToString() : "Tarefa não iniciada";
                var endDate = task.EndDate != null ? task.EndDate.ToString() : "Data ainda não definida";
                var relatedTasks = task.PrintRelatedTasks();

                Console.WriteLine($"{number} - {name}:");
                Console.WriteLine($"    Status: {statusTaks}");
                Console.WriteLine($"    Descrição: {description}");
                Console.WriteLine($"    Usuário atribuído: {user}");
                Console.WriteLine($"    Data Início: {initDate}");
                Console.WriteLine($"    Data Final: {endDate}");
                Console.WriteLine($"    Tarefas relacionadas: {relatedTasks}");

                Console.WriteLine();
                number++;
            }

            var condition = true;

            while (condition)
            {
                try
                {
                    Console.WriteLine("Escolha uma tarefa:");
                    int selection;
                    var valid = int.TryParse(Console.ReadLine(), out selection);

                    if (valid)
                    {
                        if (selection < 0 || selection >= listTasks.Count)
                        {
                            throw new ArgumentOutOfRangeException("Valor inválido da lista de tarefas.");
                        }

                        Console.Clear();
                        EditTask(listTasks[selection]);
                        break;
                    }
                    else
                    {
                        throw new ArgumentException("Valor não numérico.");
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
            }
        }

        private static void EditTask(Assignment task)
        {
            while (true)
            {
                Console.WriteLine($"Escolha o que deseja fazer:\n" +
                    $"  1 - Editar Descricao;\n" +
                    $"  2 - Atribuir a usuário;\n" +
                    $"  3 - Aterar status da tarefa;\n" +
                    $"  4 - Adicionar tarefas relacionadas;\n" +
                    $"  5 - Sair menu de edição;\n");
                var selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Console.WriteLine("Escreva a nova descrição: ");
                        var newDescription = Console.ReadLine();
                        task.EditDescription(newDescription);
                        AssignmentManager.SaveAssignmentEdits(task);
                        break;
                    case "2":
                        Console.WriteLine("Digite o nome do usuário: ");
                        var userName = Console.ReadLine();
                        User? userToAssign = UserManager.LoadUsers().FirstOrDefault(u => u.Name == userName);
                        if (userToAssign != null)
                        {
                            task.AssignUser(userToAssign);
                            AssignmentManager.SaveAssignmentEdits(task);
                            Console.WriteLine($"Usuário '{userName}' atribuído à tarefa '{task.Name}'.");
                        }
                        else
                        {
                            Console.WriteLine($"Usuário com o nome '{userName}' não encontrado.");
                        }
                        break;
                    case "3":
                        Console.WriteLine($"Escolha o novo status:\n" +
                            $"  0 - Não iniciada;\n" +
                            $"  1 - Em Andamento;\n" +
                            $"  2 - Concluída;\n" +
                            $"  3 - Atrasada.\n" +
                            $"  4 - Abandonada\n");
                        var statusSelection = Console.ReadLine();
                        if (Enum.TryParse(statusSelection, out AssignmentStatus newStatus))
                        {
                            task.ChangeStatus(newStatus);
                            AssignmentManager.SaveAssignmentEdits(task);
                            Console.WriteLine($"Status da tarefa '{task.Name}' alterado para '{newStatus}'.");
                        }
                        else
                        {
                            Console.WriteLine("Opção de status inválida.");
                        }
                        break;
                    case "4":
                        break;
                    case "5":
                        Console.WriteLine("Saindo menu de edição...");
                        Thread.Sleep(2000);
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Valor inválido.Tente novamente:");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
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

                var condition = true;

                while (condition)
                {
                    Console.Write($"Escolha o tipo de usuário:\n" +
                        $"  1 - Tech Leader;\n" +
                        $"  2 - Developer;\n");
                    string type = Console.ReadLine()!;

                    switch (type)
                    {
                        case "1":
                            userType = UserType.TechLeader;
                            condition = false;
                            break;
                        case "2":
                            userType = UserType.Developer;
                            condition = false;
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
                    $"Password: {password}\n" +
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

