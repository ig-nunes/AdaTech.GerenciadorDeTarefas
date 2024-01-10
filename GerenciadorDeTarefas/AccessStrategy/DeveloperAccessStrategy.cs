using GerenciadorDeTarefas.Models.Tasks;
using GerenciadorDeTarefas.Models.Users;
using GerenciadorDeTarefas.Repositories;
using GerenciadorDeTarefas.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerenciadorDeTarefas.AccessStrategy
{
    internal class DeveloperAccessStrategy : IAccessStrategy
    {
        public void Access(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Olá, {user.Name}. Escolha o que fazer:\n" +
                    $"  1 - Criar nova tarefa;\n" +
                    $"  2 - Visualizar suas tarefas;\n" +
                    $"  3 - Visualizar todas tarefas relacionadas;\n" +
                    $"  4 - Fazer logout;\n"
                    );

                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        CreateNewAssignment(user);
                        break;
                    case "2":
                        ViewAllAssignmentsByUser(user);
                        break;
                    case "3":
                        ViewAllRelatedAssignments(user);
                        break;
                    case "4":
                        Messages.ExitMenu("Voltando menu inicial...");
                        return;
                    default:
                        Console.WriteLine("Valor inválido.");
                        break;
                }
            }

        }

        private static void ViewAllRelatedAssignments(User user)
        {
            var allRelatedAssignments = AssignmentManager.GetAssignmentsByUserAndRelatedTasks(user);

            if (allRelatedAssignments != null)
            {
                var i = 0;
                foreach (var task in allRelatedAssignments)
                {
                    var name = task.Name;
                    var statusTaks = task.Status;
                    var description = task.Description;
                    var assignedUser = task.Dev != null ? task.Dev.ToString() : "Nenhum usuário atribuído";
                    var initDate = task.InitDate != null ? task.InitDate.ToString() : "Tarefa não iniciada";
                    var endDate = task.EndDate != null ? task.EndDate.ToString() : "Data ainda não definida";
                    var relatedTasks = task.PrintRelatedTasks();

                    Console.WriteLine($"{i} - {name}:");
                    Console.WriteLine($"    Status: {statusTaks}");
                    Console.WriteLine($"    Descrição: {description}");
                    Console.WriteLine($"    Usuário atribuído: {assignedUser}");
                    Console.WriteLine($"    Data Início: {initDate}");
                    Console.WriteLine($"    Data Final: {endDate}");
                    Console.WriteLine($"    Tarefas relacionadas: {relatedTasks}");

                    Console.WriteLine();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhuma tarefa encontrada!");
            }

            Messages.PressAnyKeyToContinue();

        }

        private static void ViewAllAssignmentsByUser(User user)
        {
            var assignmentsList = AssignmentManager.GetAssignmentsByUser(user);

            if (assignmentsList != null)
            {
                var i = 0;
                foreach (var task in assignmentsList)
                {
                    var name = task.Name;
                    var statusTaks = task.Status;
                    var description = task.Description;
                    var assignedUser = task.Dev != null ? task.Dev.ToString() : "Nenhum usuário atribuído";
                    var initDate = task.InitDate != null ? task.InitDate.ToString() : "Tarefa não iniciada";
                    var endDate = task.EndDate != null ? task.EndDate.ToString() : "Data ainda não definida";
                    var relatedTasks = task.PrintRelatedTasks();

                    Console.WriteLine($"{i} - {name}:");
                    Console.WriteLine($"    Status: {statusTaks}");
                    Console.WriteLine($"    Descrição: {description}");
                    Console.WriteLine($"    Usuário atribuído: {assignedUser}");
                    Console.WriteLine($"    Data Início: {initDate}");
                    Console.WriteLine($"    Data Final: {endDate}");
                    Console.WriteLine($"    Tarefas relacionadas: {relatedTasks}");

                    Console.WriteLine();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhuma tarefa encontrada!");
            }

            Messages.PressAnyKeyToContinue();
        }

        private static void CreateNewAssignment(User user)
        {
            Console.WriteLine("Digite o nome da nova tarefa: ");
            var name = Console.ReadLine();

            Console.WriteLine("Digite a descrição da nova tarefa: ");
            var description = Console.ReadLine();

            Assignment newAssignment = new Assignment(name, description, user);
            AssignmentManager.SaveNewAssignment(newAssignment);
            Console.WriteLine("Nova tarefa criada com sucesso:");
            newAssignment.PrintAssignment();
            Messages.PressAnyKeyToContinue();

        }
    }
}
