using GerenciadorDeTarefas.Models.Tasks;
using GerenciadorDeTarefas.Models.Users;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GerenciadorDeTarefas.Repositories
{
    public class AssignmentManager
    {
        private const string JsonFileName = "tasks.json";
        private static List<Assignment> _assignments;

        public static void InitializeFile()
        {
            if (!File.Exists(JsonFileName) || new FileInfo(JsonFileName).Length == 0)
            {
                // Create some default assignments if the file is empty or doesn't exist.
                var defaultAssignment1 = new Assignment("Task1", "Description1");
                var defaultAssignment2 = new Assignment("Task2", "Description2");

                _assignments = new List<Assignment> { defaultAssignment1, defaultAssignment2 };
                SaveAssignmentsToJson();
            }
        }

        public static List<Assignment> GetAllAssignments()
        {
            LoadAssignmentsFromJson(); // Carrega os dados do JSON antes de obter todas as tarefas
            return _assignments;
        }

        public static Assignment GetAssignmentByName(string name)
        {
            LoadAssignmentsFromJson(); // Carrega os dados do JSON antes de buscar por nome
            return _assignments.FirstOrDefault(a => a.Name == name);
        }

        public static List<Assignment> GetAssignmentsByNames(string[] taskNames)
        {
            LoadAssignmentsFromJson();
            List<Assignment> tasks = _assignments;

            List<Assignment> matchingTasks = tasks
                .Where(task => taskNames.Contains(task.Name))
                .ToList();

            return matchingTasks;
        }

        public static List<Assignment> GetAssignmentsByUser(User user)
        {
            LoadAssignmentsFromJson();
            List<Assignment> tasks = _assignments;

            List<Assignment> userTasks = tasks
                .Where(task => task.Dev != null && task.Dev.Name == user.Name)
                .ToList();

            return userTasks;
        }

        public static List<Assignment> GetAssignmentsByUserAndRelatedTasks(User user)
        {
            List<Assignment> tasksByUser = GetAssignmentsByUser(user);
            List<Assignment> finalAssignments = new List<Assignment>(tasksByUser);

            foreach (var taskByUser in tasksByUser)
            {
                if (taskByUser.RelatedTasks != null)
                {
                    foreach (var relatedTaskName in taskByUser.RelatedTasks)
                    {
                        List<Assignment> relatedTasks = GetAssignmentsByNames(new string[] { relatedTaskName });

                        foreach (var relatedTask in relatedTasks)
                        {
                            if (!finalAssignments.Contains(relatedTask))
                            {
                                finalAssignments.Add(relatedTask);
                            }
                        }
                    }
                }
            }

            return finalAssignments;
        }

        public static void SaveNewAssignment(Assignment assignment)
        {
            LoadAssignmentsFromJson();
            _assignments.Add(assignment);
            SaveAssignmentsToJson();
        }

        public static void SaveAssignmentEdits(Assignment editedAssignment)
        {
            LoadAssignmentsFromJson();
            var existingAssignment = _assignments.FirstOrDefault(a => a.Name == editedAssignment.Name);

            if (existingAssignment != null)
            {
                existingAssignment.Description = editedAssignment.Description;
                existingAssignment.Dev = editedAssignment.Dev;
                existingAssignment.Status = editedAssignment.Status;
                existingAssignment.InitDate = editedAssignment.InitDate;
                existingAssignment.EndDate = editedAssignment.EndDate;

                SaveAssignmentsToJson();
            }
        }

        private static void LoadAssignmentsFromJson()
        {
            if (_assignments == null)
            {
                if (File.Exists(JsonFileName))
                {
                    string jsonContent = File.ReadAllText(JsonFileName);
                    _assignments = JsonConvert.DeserializeObject<List<Assignment>>(jsonContent) ?? new List<Assignment>();
                }
                else
                {
                    _assignments = new List<Assignment>();
                }
            }
        }

        private static void SaveAssignmentsToJson()
        {
            string jsonContent = JsonConvert.SerializeObject(_assignments, Formatting.Indented);
            File.WriteAllText(JsonFileName, jsonContent);
        }
    }
}

