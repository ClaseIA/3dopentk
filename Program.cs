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
           

            string archivo = File.ReadAllText("cubo.obj");
            Char delimitador = ' ';
            int caras = 0;
            int verticies = 0;
            int normales = 0;
            string[] subcadenenas = Regex.Split(archivo, "\n");
            double[,] vertex = new double[subcadenenas.Length, 3];
            // int v=0;
            string[] vertexString = new string[0];
            for (int i = 0; i < subcadenenas.Length; i++)
            {
                string[] subcadenitas = Regex.Split(subcadenenas[i], " ");

                for (int j = 0; j < subcadenenas[i].Length; j++)
                {
                    if (subcadenenas[i][j] == 'v')
                    {
                        if (subcadenenas[i][j + 1] == 'n')
                        {
                            normales++;
                            break;
                        }


                        for (int k = 1; k <= 3; k++)
                        {
                            Console.WriteLine(subcadenitas[k]);
                            Console.WriteLine(Convert.ToDouble(subcadenitas[k]));
                            vertex[verticies, k - 1] = Convert.ToDouble(subcadenitas[k]);
                            Console.WriteLine(verticies);
                        }
                        verticies++;
                        break;
                    }
                    else
                        break;
                }

                //leer las caras
                for (int j = 0; j < subcadenenas[i].Length; j++)
                {
                    if (subcadenenas[i][j] == 'f')
                    {
                        caras++;
                        //Console.WriteLine(subcadenenas[i]);
                        break;
                    }
                    else
                        break;
                }


            }
            Console.WriteLine(vertex[5, 0]);
            Console.WriteLine("el numero de vertices son: " + verticies);
            Console.WriteLine("el numero decaras son: " + caras);
            Console.WriteLine("el numero normales son: " + normales);

           // Console.ReadKey();
            screen.Run();
        }
    }
}
