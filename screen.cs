﻿using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Text.RegularExpressions;


namespace ConsoleApplication1
{
    class screen:GameWindow
    {
        Texture2D Texture;
        Texture2D Texture2;

        double[,] vertex;   
        int[,] faces;
        int[,] facesText;
        double[,] VerText;
        
        Punto[] punto = new Punto[10000];
        Punto foco= new Punto();
        Punto []vertices;
        Punto[] Texturas;

        int[] caritas;
        int[] texturitas;
        Random aleatorio= new Random();
        double intensidad;
        double radio;
        double posx;
        double posy;
        double distancia;
        double angulo;
        double fScale;
        double fTrans;

        public screen(int ancho, int alto): base (ancho,alto)
        {
            GL.Enable(EnableCap.Texture2D);
        }

          protected override void OnLoad(EventArgs e)
          {
            cargar();
            fScale = 1;
            fTrans = 0;
            base.OnLoad(e);
            radio = 0.2f;
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            GL.Ortho(0, 2, 0, 2, -1, 1);
           Texture = PathTexture.LoadTexture("panda.png");
            Texture2 = PathTexture.LoadTexture("cubincolorin.png");
            Matrix4 matriz = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1, 50.0f);
            GL.LoadMatrix(ref matriz);
            GL.Enable(EnableCap.DepthTest);
              
              foco.valores(0.5, 0.5, 0);
            for(int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Punto(); vertices[i].valores(vertex[i,0], vertex[i, 1], vertex[i, 2]);
            }
            for (int i = 0; i < Texturas.Length; i++)
            {
                Texturas[i] = new Punto(); Texturas[i].valores(VerText[i, 0], VerText[i, 1],0);
            }

            //Console.WriteLine(faces[0, 1]);



        }

          protected override void OnUpdateFrame(FrameEventArgs e)
          {
              base.OnUpdateFrame(e);
              GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
              //GL.ClearColor(0f, 0f, 0f, 0f);
          }
          protected override void OnRenderFrame(FrameEventArgs e)
          {

            base.OnRenderFrame(e);
            GL.LoadIdentity();
            GL.Scale(0.5, 0.5, 0.5);
          

           GL.BindTexture(TextureTarget.Texture2D, Texture.ID);
            GL.Begin(PrimitiveType.Quads);

           // GL.Color3(1.0, 0.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-2, -2,1.5f);

           // GL.Color3(1.0, 0, 1.0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(2f, -2,1.5f);

           // GL.Color3(1f, 1f, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(2f, 2f,1.5f);

          // GL.Color3(0, 1f, 1f);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-2, 2f,1.5f);

            GL.End();

            GL.Rotate(angulo, 0, 1, 0);

            // Console.WriteLine("ddddd: " + caritas.Length);
           
            for (int i = 0; i <caritas.Length; i++)
            {
             GL.BindTexture(TextureTarget.Texture2D, Texture2.ID);
                GL.Begin(PrimitiveType.Polygon);
               
                for (int j = 0; j < 3; j++)
                {
                   
                   GL.TexCoord2(Texturas[facesText[i, j]-1 ].x, Texturas[facesText[i, j]-1 ].y);
                  // GL.Vertex3(vertices[faces[i, j] - 1].x, vertices[faces[i, j] - 1].y, vertices[faces[i, j] - 1].z);

                   /*  GL.Vertex3(((vertices[faces[i, j] - 1].x * Math.Cos(angulo)  + Math.Sin(angulo)* vertices[faces[i, j] - 1].z)*fScale)+fTrans,
                     vertices[faces[i, j] - 1].y*fScale,
                    ( vertices[faces[i, j] - 1].x * Math.Sinh(angulo)+  vertices[faces[i, j] - 1].z * Math.Cos(angulo))*fScale);*/
                }
                GL.End();

            }

           





                    if (angulo > 360)
            {
                angulo = 0;
            }
           


            SwapBuffers();
          }

          protected override void OnKeyPress(KeyPressEventArgs e)
          {
              base.OnKeyPress(e);
              if (e.KeyChar == 'a')
              {
                fTrans -= 0.1;
            }
              if (e.KeyChar == 'd')
              {
                fTrans += 0.1;
              }
            if (e.KeyChar == 'r')
            {
                angulo += 1;
               // Console.WriteLine(angulo);
            }
            if (e.KeyChar == 's')
            {
               fScale += 0.01;
            }
        }

          protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e)
          {
              base.OnMouseMove(e);
              posx = 0.001 * e.Mouse.X;
              posy = 0.001 * e.Mouse.Y;
            foco.valores(posx, posy, 0);
        }

        void cargar()
        {
            string archivo = File.ReadAllText("carrito.obj");
            Char delimitador = ' ';
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
                                //Console.WriteLine(subcadenitas[k]);
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
                        {//1,3,5,7
                            facesText[caras, k - 1] = Int32.Parse(subsubcadenitas[f]);
                            //Console.WriteLine(subsubcadenitas[f]);
                            //if(subcadenitas[k][1]==)
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
            texturitas = new int[texturas];
            Texturas= new Punto[texturas];
        }



    }
}
