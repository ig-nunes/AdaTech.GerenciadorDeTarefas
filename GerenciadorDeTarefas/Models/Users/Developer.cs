﻿using GerenciadorDeTarefas.AccessStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Models.Users
{
    public class Developer : User
    {
        public override UserType UserType => UserType.Developer;
        public Developer(string name, string email, string password) : base(name, email, password)
        {
            this.SetAccessStrategy(new DeveloperAccessStrategy());
        }
    }
}
