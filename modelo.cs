using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class modelo
    {
        double[,] vertex;
        int[,] faces;
        int[,] facesText;
        double[,] VerText;
        Punto[] vertices;
        int[] caritas;
        Punto[] Texturas;

        public modelo()
        {

        }

        public void crear(string nombremodelo)
        {
            string archivo = File.ReadAllText(nombremodelo);
            int caras = 0;
            int verticies = 0;
            int texturas = 0;
            int normales = 0;
            string[] subcadenenas = Regex.Split(archivo, "\n");
            vertex = new double[subcadenenas.Length, 3];
            faces = new int[subcadenenas.Length, 100];
            facesText = new int[subcadenenas.Length, 100];
            VerText = new double[subcadenenas.Length, 2];
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
                        if (subcadenenas[i][j + 1] == 't')
                        {

                            for (int k = 1; k <= 2; k++)
                            {
                                
                                VerText[texturas, k - 1] = Convert.ToDouble(subcadenitas[k]);
                            }
                            texturas++;
                            break;
                        }


                        for (int k = 1; k <= 3; k++)
                        {
                            vertex[verticies, k - 1] = Convert.ToDouble(subcadenitas[k]);
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



                        string[] subsubcadenitas = Regex.Split(subcadenenas[i], "/|\\ ");
                        int x = 1;
                        int f = 2;
                        for (int k = 1; k <= 3; k++)
                        {
                            facesText[caras, k - 1] = Int32.Parse(subsubcadenitas[f]);
                        
                            faces[caras, k - 1] = Int32.Parse(subsubcadenitas[x]);
                            x += 3;
                            f += 3;

                        }
                        caras++;

                        break;
                    }
                    else
                        break;
                }


            }

            Console.WriteLine("el numero de vertices son: " + verticies);
            Console.WriteLine("el numero de caras son: " + caras);
            Console.WriteLine("el numero Vertices de texturas son: " + texturas);
            vertices = new Punto[verticies];
            caritas = new int[caras];
            Texturas = new Punto[texturas];


            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Punto(); vertices[i].valores(vertex[i, 0], vertex[i, 1], vertex[i, 2]);
            }

             for (int i = 0; i < Texturas.Length; i++)
           {
               Texturas[i] = new Punto(); Texturas[i].valores(VerText[i, 0], VerText[i, 1],0);
           }
        }


        public void dibujar(Texture2D texturamodelo,double angle,double fScale,double fTrans)
        {
          double angulo= Math.PI * angle / 180;
            for (int i = 0; i < caritas.Length; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D, texturamodelo.ID);
                GL.Begin(PrimitiveType.Polygon);

                for (int j = 0; j < 3; j++)
                {

                    GL.TexCoord2(Texturas[facesText[i, j] - 1].x, Texturas[facesText[i, j] - 1].y);
                   //  GL.Vertex3(vertices[faces[i, j] - 1].x, vertices[faces[i, j] - 1].y, vertices[faces[i, j] - 1].z);

                     

                     GL.Vertex3(((vertices[faces[i, j] - 1].x * Math.Cos(angulo) + Math.Sin(angulo) * vertices[faces[i, j] - 1].z) * fScale) ,
                     vertices[faces[i, j] - 1].y * fScale,
                    (vertices[faces[i, j] - 1].x * -Math.Sin(angulo) + vertices[faces[i, j] - 1].z * Math.Cos(angulo)) * fScale);
                }
                GL.End();

            }
        }

    }
}
