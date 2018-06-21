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
    class screen:GameWindow
    {
        Texture2D Texture;
        Texture2D Texture2;
        
        Punto[] punto = new Punto[10000];
       
     
        modelo auto = new modelo();
        modelo mono = new modelo();
       
        Random aleatorio= new Random();
    
   
       
        double angulo;
        double angulor;
        double factorScale;
        double factorTrans;

        public screen(int ancho, int alto): base (ancho,alto)
        {
            GL.Enable(EnableCap.Texture2D);
        }

          protected override void OnLoad(EventArgs e)
          {
            base.OnLoad(e);

      
            Matrix4 matriz = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 100, Width / (float)Height, 1, 5.0f);
            GL.LoadMatrix(ref matriz);
            GL.Enable(EnableCap.DepthTest);


            auto.crear("carrito.obj");
           // mono.crear("mono.obj");
            Texture = PathTexture.LoadTexture("panda.png");
            Texture2 = PathTexture.LoadTexture("carrito.png");
           
            factorScale = 1;
            factorTrans = 0;

           
              
          
     
        }

          protected override void OnUpdateFrame(FrameEventArgs e)
          {
              base.OnUpdateFrame(e);
              GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
              GL.ClearColor(0f, 0f, 0f, 0f);
          }
          protected override void OnRenderFrame(FrameEventArgs e)
          {

            base.OnRenderFrame(e);
            GL.LoadIdentity();
            GL.Scale(0.5, 0.5, 0.5);
            fondo(Texture);


            //GL.Rotate(angulor, 0, 1, 0);

            auto.dibujar(Texture2,angulo,factorScale,factorScale);
           // mono.dibujar(Texture2, 0, fScale,0);

           


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
                factorTrans -= 0.1;
            }
              if (e.KeyChar == 'd')
              {
                factorTrans += 0.1;
              }
            if (e.KeyChar == 'r')
            {
                angulo += 1;
                Console.WriteLine(angulo);
            }
            if (e.KeyChar == 's')
            {
               factorScale += 0.1;
            }
        }

          protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e)
          {
              base.OnMouseMove(e);
             angulor = 1 * e.Mouse.X;
            // angulo = 0.001 * e.Mouse.Y;

        }

        

        public void fondo(Texture2D fondito)
        {
            GL.BindTexture(TextureTarget.Texture2D, fondito.ID);
            GL.Begin(PrimitiveType.Quads);


            GL.TexCoord2(0, 1);
            GL.Vertex3(-2, -2, 1.5f);


            GL.TexCoord2(1, 1);
            GL.Vertex3(2f, -2, 1.5f);


            GL.TexCoord2(1, 0);
            GL.Vertex3(2f, 2f, 1.5f);

            GL.TexCoord2(0, 0);
            GL.Vertex3(-2, 2f, 1.5f);

            GL.End();
        }



    }
}
