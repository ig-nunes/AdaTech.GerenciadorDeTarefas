﻿using GerenciadorDeTarefas.Models.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GerenciadorDeTarefas.Models.Tasks
{

    public class Assignment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public User? Dev { get; set; }
        public List<Assignment> RelatedTasks { get;  set; }
        public AssignmentStatus Status { get;  set; }
        public DateTime? InitDate { get;  set; }
        public DateTime? EndDate { get;  set; }

        public Assignment(string name, string description, User? user = null)
        {
            Name = name;
            Description = description;
            Dev = user;
            RelatedTasks = new List<Assignment>();
        }

        public void ChangeStatus(AssignmentStatus status)
        {
            Status = status;
        }

        public void AssignUser(User user)
        {
            Dev = user;
        }

        public void AddRelatedTask(Assignment relatedTask)
        {
            foreach (var task in RelatedTasks)
            {
                if (task.Name == relatedTask.Name && task.InitDate == relatedTask.InitDate)
                {
                    Console.WriteLine("Essa tarefa já foi adicionada como 'relacionada'!");
                    return;
                }
                else if (task.Name == Name && task.InitDate == InitDate)
                {
                    Console.WriteLine("Não é possível adicionar a própria tarefa como 'relacionada'!");
                    return;
                }
            }

            RelatedTasks.Add(relatedTask);
            Console.WriteLine("Tarefa relacionada adicionada com sucesso!");
        }

        public string PrintRelatedTasks()
        {
            string tasks = "";
            int i = 1;
            foreach (var relatedTask in RelatedTasks)
            {
                tasks += $"{i} - {relatedTask.Name}\n";
                i++;
            }
            if (tasks.Equals(""))
            {
                return "Nenhuma atividade relacionada atribuída";
            }

            return tasks;
        }

        public void SetStartDate(DateTime startDate)
        {
            InitDate = startDate;
        }

        public void SetEndDate(DateTime? endDate)
        {
            EndDate = endDate;
        }

        public void StartTask()
        {
            if (Status != AssignmentStatus.NotStarted)
            {
                Console.WriteLine("A tarefa já foi iniciada!");
                return;
            }
            Status = AssignmentStatus.InProgress;
            Console.WriteLine("Tarefa iniciada com sucesso!");
        }

        public void EditDescription(string newDescription)
        {
            Description = newDescription;
        }
    }

}
