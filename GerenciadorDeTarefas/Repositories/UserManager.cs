using GerenciadorDeTarefas.Models.Users;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GerenciadorDeTarefas.Utils;

namespace GerenciadorDeTarefas.Repositories
{
    internal class UserManager
    {
        private const string FilePath = "users.json";

        public static void InitializeFile()
        {
            if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
            {
                string hashPassword = HashClass.SetHashPassword("senha123");
                // Criar um TechLeader padrão se o arquivo estiver vazio ou não existir:
                var defaultTechLeader = new TechLeader("techLeader", "tech@email.com", hashPassword);

                var initialUser = new List<User> { defaultTechLeader };
                SaveUsers(initialUser);
            }

        }

        public static void SaveUser(User user)
        {
            List<User> users = LoadUsers();
            users.Add(user);

            SaveUsers(users);
        }

        public static User? AuthenticateUser(string email, string password)
        {
            List<User> users = LoadUsers();
            string hashPassword = HashClass.SetHashPassword(password);
            return users.Find(u => u.Email == email && u.Password == hashPassword);
        }

        private static List<User> LoadUsers()
        {
            List<User> users = new List<User>();

            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var userDtoList = JsonConvert.DeserializeObject<List<dynamic>>(json);

                foreach (var userDto in userDtoList)
                {
                    UserType userType = Enum.Parse<UserType>((string)userDto.Type);

                    switch (userType)
                    {
                        case UserType.TechLeader:
                            users.Add(new TechLeader(
                                (string)userDto.Name,
                                (string)userDto.Email,
                                (string)userDto.Password
                            ));
                            break;

                        case UserType.Developer:
                            users.Add(new Developer(
                                (string)userDto.Name,
                                (string)userDto.Email,
                                (string)userDto.Password
                            ));
                            break;


                        default:
                            throw new NotSupportedException("Unsupported user type.");
                    }
                }
            }

            return users;
        }

        private static void SaveUsers(List<User> users)
        {
            var userDtoList = users.Select(u => new
            {
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                Type = u.UserType.ToString(),
            }).ToList();

            string json = JsonConvert.SerializeObject(userDtoList, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

    }
}
