using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Utils
{
    public static class CompareProperties
    {
        public static bool VerifyProperty(string property, string propertySaved)
        {
            return propertySaved == property;
        }
    }
}
