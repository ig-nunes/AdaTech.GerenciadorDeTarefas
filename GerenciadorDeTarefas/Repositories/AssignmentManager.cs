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

        public static void SaveNewAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
            SaveAssignmentsToJson();
        }

        public static void SaveAssignmentEdits(Assignment editedAssignment)
        {
            var existingAssignment = _assignments.FirstOrDefault(a => a.Name == editedAssignment.Name);

            if (existingAssignment != null)
            {
                // Update properties
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
            if (_assignments == null) // Carrega apenas se a lista ainda não foi inicializada
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

