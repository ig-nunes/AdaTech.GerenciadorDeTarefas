using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GerenciadorDeTarefas.Utils
{
    public static class HashClass
    {
        public static string UpdatePassword(string oldPassword, string passwordSaved, string newPassword)
        {
            if (VerifyHashPassword(oldPassword, passwordSaved))
            {
                return SetHashPassword(newPassword);
            }
            else
            {
                throw new InvalidOperationException("Não foi possível atualizar a senha (senha antiga errada).");
            }
        }

        public static void ForgotPassword(string property, string propertySaved, string newPassword)
        {
            if (VerifyProperty(property, propertySaved))
            {
                SetHashPassword(newPassword);
            }
            else
            {
                throw new InvalidOperationException("Não foi possível mudar a senha (nome errado).");
            }
        }

        private static bool VerifyProperty(string property, string propertySaved)
        {
            return propertySaved == property;
        }

        private static string SetHashPassword(string newPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(newPassword));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                String hashPassword = builder.ToString();

                return hashPassword;
            }
        }

        public static bool VerifyHashPassword(string password, string passwordSaved)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString() == passwordSaved;
            }
        }
    }
}
