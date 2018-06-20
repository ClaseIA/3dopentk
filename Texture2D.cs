using System;

namespace ConsoleApplication1
{
    public class Texture2D
    {
        private int id;
        private int width, height;

        public int ID { get { return id; } }
        public int WIDTH { get { return width; } }
        public int HEIGHT { get { return height; } }

        public Texture2D(int id, int width, int height)
        {
            this.id = id;
            this.width = width;
            this.height = height;
        }
    }
}

