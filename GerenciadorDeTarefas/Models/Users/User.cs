using GerenciadorDeTarefas.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Models.Users
{
    public abstract class User
    {
        private string _name;
        private string _email;
        private string _password;

        public string Name { get { return _name; } }
        public string Email { get { return _email; } }
        public string Password { get { return _password; } }

        public User(string name, string email, string password) 
        {
            this._name = name;
            this._email = email;
            this._password = HashClass.SetHashPassword(password);
        }

        public void UpdatePassword(string oldPassword, string password)
        {
            try 
            {
                _password = HashClass.UpdatePassword(oldPassword, Password, password);
                Console.WriteLine("Senha atualizada com sucesso!");
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar a senha: {ex.Message}");
            }
            
        }

        public void ForgotPassword(string name, string password)
        {
            try 
            {
                HashClass.ForgotPassword(name, Name, password);
                Console.WriteLine("Senha atualizada com sucesso!");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Erro ao atualizar a senha: {ex.Message}");
            }

        }

        public void UpdateEmail(string email)
        {
            _email = email;
        }

        public void UpdateName(string name)
        {
            _name = name;
        }

    }
}
