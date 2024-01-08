using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Models.Tasks
{
    public enum AssignmentStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Late = 3, 
        Abandoned = 4,
    }
}
