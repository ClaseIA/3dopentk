using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            screen screen = new screen(600, 500);
            screen.Title = "Renderin";
           

           
           // Console.ReadKey();
            screen.Run();
        }
    }
}
