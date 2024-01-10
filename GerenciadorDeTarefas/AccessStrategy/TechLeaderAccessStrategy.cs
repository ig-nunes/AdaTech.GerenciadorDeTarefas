using GerenciadorDeTarefas.Models.Tasks;
using GerenciadorDeTarefas.Models.Users;
using GerenciadorDeTarefas.Repositories;
using GerenciadorDeTarefas.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                    $"  5 - Fazer Logout.\n"
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
                        ViewAllDevelopers();
                        break;
                    case "4":
                        Register();
                        break; ;
                    case "5":
                        Messages.ExitMenu("Voltando menu inicial...");
                        return;
                    default:
                        Console.WriteLine("Valor inválido.");
                        break;
                }
            }
        }

        private void ViewAllDevelopers()
        {
            var developersList = UserManager.LoadUsers();
            Console.WriteLine("Todos os usuários:\n");
            int position = 0;
            foreach (User user in developersList)
            {
                var type = user.UserType == UserType.Developer ? "Desenvolvedor" : "Tech Leader";
                Console.WriteLine($"{position} - {user.Name}:");
                Console.WriteLine($"    Tipo de usuário: {type} ");
                Console.WriteLine($"    E-mail: {user.Email}\n");
                position++;
            }

            Messages.PressAnyKeyToContinue();
            Console.Clear();
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
                    $"  3 - Alterar status da tarefa;\n" +
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
                        var allTasks = AssignmentManager.GetAllAssignments();
                        Console.WriteLine("Todas as tarefas:");
                        var number = 0;
                        foreach (var assignment in allTasks)
                        {
                            var name = assignment.Name;
                            var statusTaks = assignment.Status;
                            var description = assignment.Description;
                            var user = assignment.Dev != null ? assignment.Dev.ToString() : "Nenhum usuário atribuído";

                            Console.WriteLine($"{number} - {name}:");
                            Console.WriteLine($"    Status: {statusTaks}");
                            Console.WriteLine($"    Descrição: {description}");
                            Console.WriteLine($"    Usuário atribuído: {user}");

                            Console.WriteLine();
                            number++;
                        }

                        Console.WriteLine("Digite os nomes das tarefas que você deseja adicionar como relacionadas, separados por uma vírgula e um espaço (', ') :");
                        var taskNamesInput = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(taskNamesInput))
                        {
                            var listNames = taskNamesInput.Split(", ");
                            var assignmentsFound = AssignmentManager.GetAssignmentsByNames(listNames);

                            if (assignmentsFound.Count > 0)
                            {
                                var relatedTaskNames = task.RelatedTasks ?? new List<string>();
                                relatedTaskNames.AddRange(listNames);

                                task.RelatedTasks = relatedTaskNames;

                                AssignmentManager.SaveAssignmentEdits(task);

                                Console.WriteLine("Tarefas relacionadas adicionadas com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Não foi encontrado tarefa(s) com esse(s) nome(s).");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Argumento inválido ou vazio");
                        }

                        break;
                    case "5":
                        Messages.ExitMenu("Saindo menu de edição...");
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
            User? user;
            string name, email, password;
            
            while (true)
            {
                Console.Write("Digite seu nome: ");
                name = Console.ReadLine()!;

                Console.Write("Digite seu email: ");
                email = Console.ReadLine()!;

                Console.Write("Digite uma senha: ");
                password = Console.ReadLine()!;

                if (name == null || email == null || password == null)
                {
                    Console.WriteLine("Por favor, nenhum dos campos podem ser nulos!");
                }
                else
                {
                    break;
                }                
            }

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
                $"Tipo: {userType.ToString()}\n");              

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
            Messages.PressAnyKeyToContinue("Usuário criado com sucesso! Aperte qualquer botão para continuar.");
            
        }
    }
}

