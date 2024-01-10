using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Utils
{
    public class Messages
    {
        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Aperte qualquer botão para continuar.");
            Console.ReadKey();
        }

        public static void PressAnyKeyToContinue(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public static void ExitMenu(string message) 
        {
            Console.WriteLine(message);
            Thread.Sleep(2000);
            Console.Clear();
        }

    }
}
