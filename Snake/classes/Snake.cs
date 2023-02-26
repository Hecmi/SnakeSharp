using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace Snake.classes
{
    class SnakeLite
    {
        public int i;

        public int x = 0;
        public int y = 0;

        public bool f_busy = false;

        //public List<Point> positions = new List<Point>();
        public List<Tuple<Point, Tuple<int, int, int, int, int, int>>> positions = new List<Tuple<Point, Tuple<int, int, int, int, int, int>>>();
        //public List<Position> positions = new List<Position>();
        //public List<tuple.t3<int, int, int>> tmp = new List<tuple.t3<int, int, int>>();

    }

    class Snake : SnakeLite
    {
        public bool AI;

        //public int i;

        //public int x = 0;
        //public int y = 0;
        public int x2;
        public int y2;
        public int x3;
        public int y3;
        //public bool f_busy = false;

        //public List<Point> positions = new List<Point>();

        public Point FakeApple = new Point(-1, -1);

        public Bitmap snake;
        Image apple;
        //Image head;
        Image head_u;
        Image head_l;
        Image head_r;
        Image head_d;

        Image body_o;
        Image body_v;
        Image body_1;
        Image body_2;
        Image body_3;
        Image body_4;

        Image tail_u;
        Image tail_l;
        Image tail_r;
        Image tail_d;

        public Color color_RGB = Color.FromArgb(25, 45, 225);

        public Image head
        {
            get
            {
                int tmp = int.Parse(((this.x + (Program.radius * 2)) / Program.radius) + "" + ((this.y + (Program.radius * 2)) / Program.radius));

                switch (tmp)
                {
                    case 21:
                    case 22:
                        return this.head_u;
                    case 12:
                        return this.head_l;
                    case 32:
                        return this.head_r;
                    case 23:
                        return this.head_d;
                    default:
                        return new Bitmap(1, 1);
                }
            }
        }

        public Image tail
        {
            get
            {
                //string tmp = ((this.x2 + (Program.radius * 2)) / Program.radius) + "" + ((this.y2 + (Program.radius * 2)) / Program.radius);
                //label3.Text = tmp;
                int tmp = int.Parse(((this.x2 + (Program.radius * 2)) / Program.radius) + "" + ((this.y2 + (Program.radius * 2)) / Program.radius));

                switch (tmp)
                {
                    case 21:
                    case 24:
                        return this.tail_d;
                    case 12:
                    case 42:
                        return this.tail_r;
                    case 02:
                    case 32:
                        return this.tail_l;
                    case 20:
                    case 23:
                        return this.tail_u;
                    default:
                        return new Bitmap(1, 1);
                }
            }
        }

        public Image body
        {
            get
            {
                int tmp = int.Parse(((this.x2 + (Program.radius * 2)) / Program.radius) + "" + ((this.y2 + (Program.radius * 2)) / Program.radius) + "" + ((this.x3 + (Program.radius * 2)) / Program.radius) + "" + ((this.y3 + (Program.radius * 2)) / Program.radius));
                //label3.Text = tmp;

                switch (tmp)
                {
                    case 1212:
                    case 3232:
                    case 4212:
                    case 1242:
                    case 0232:
                    case 3202:
                        ///////////////////////////
                        //case 3231:
                        //case 3233:
                        return this.body_o;
                    case 2323:
                    case 2121:
                    case 2421:
                    case 2124:
                    case 2320:
                    case 2023:
                        ///////////////////////////
                        return this.body_v;
                    case 1223:
                    case 2132:
                    case 4223:
                    case 2102:
                    case 2432:
                    case 1220:
                    case 2402:
                    case 4220:
                        return this.body_1;
                    case 2332:
                    case 1221:
                    case 2302:
                    case 4221:
                    case 1224:
                    case 2032:
                    case 2002:
                    case 4224:
                        return this.body_2;
                    case 2112:
                    case 3223:
                    case 2142:
                    case 3220:
                    case 0223:
                    case 2412:
                    case 2442:
                    case 0220:
                        //
                        return this.body_3;
                    case 3221:
                    case 2312:
                    case 3224:
                    case 2012:
                    case 2342:
                    case 0221:
                    case 2042:
                    case 0224:
                        return this.body_4;
                    default:
                        //label3.Text = "xy:" + (this.x2 + (this.y2 * 5) + this.x3 + (this.y3 * 10));
                        System.Diagnostics.Debug.WriteLine("tmp " + tmp);
                        return new Bitmap(1, 1);
                }
            }
        }

        public void refreshSpritesSnake()
        {
            this.snake = new Bitmap(Image.FromFile(@"images\snake0" + ".png"));

            //chanceColorSnake(Color.FromArgb(100,100,250), this.snake);
            this.snake = TintImages(this.snake, color_RGB);
            Rectangle rect;

            rect = new Rectangle(195, 0, 60, 60);
            //Bitmap head_uu = this.snake.Clone(rect, PixelFormat.Undefined);
            //head_uu.SetResolution(62, 62);
            this.head_u = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(195, 65, 60, 60);
            this.head_l = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 0, 60, 60);
            this.head_r = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 65, 60, 60);
            this.head_d = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));

            rect = new Rectangle(195, 130, 60, 60);
            this.tail_u = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(195, 195, 60, 60);
            this.tail_l = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 130, 60, 60);
            this.tail_r = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 195, 60, 60);
            this.tail_d = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));

            rect = new Rectangle(60, 0, 60, 60);
            this.body_o = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(130, 65, 60, 60);
            this.body_v = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(0, 0, 60, 60);
            this.body_1 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(0, 65, 60, 60);
            this.body_2 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(130, 0, 60, 60);
            this.body_3 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(130, 130, 60, 60);
            this.body_4 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));

            this.apple = (Image)new Bitmap(Image.FromFile(@"images\apple.png"), new Size(Program.radius, Program.radius));//Image.FromFile(@"images\apple.png"); 

        }
        
        public Snake(int i)
        {
            this.i = i;
            this.snake = new Bitmap(Image.FromFile(@"images\snake0" + ".png"));
            
            //chanceColorSnake(Color.FromArgb(100,100,250), this.snake);
            this.snake = TintImages(this.snake, color_RGB);
            Rectangle rect;

            rect = new Rectangle(195, 0, 60, 60);
            //Bitmap head_uu = this.snake.Clone(rect, PixelFormat.Undefined);
            //head_uu.SetResolution(62, 62);
            this.head_u = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(195, 65, 60, 60);
            this.head_l = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 0, 60, 60);
            this.head_r = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 65, 60, 60);
            this.head_d = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));

            rect = new Rectangle(195, 130, 60, 60);
            this.tail_u = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(195, 195, 60, 60);
            this.tail_l = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 130, 60, 60);
            this.tail_r = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(256, 195, 60, 60);
            this.tail_d = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));

            rect = new Rectangle(60, 0, 60, 60);
            this.body_o = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(130, 65, 60, 60);
            this.body_v = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(0, 0, 60, 60);
            this.body_1 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(0, 65, 60, 60);
            this.body_2 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(130, 0, 60, 60);
            this.body_3 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));
            rect = new Rectangle(130, 130, 60, 60);
            this.body_4 = new Bitmap(this.snake.Clone(rect, this.snake.PixelFormat), new Size(Program.radius, Program.radius));

            this.apple = (Image)new Bitmap(Image.FromFile(@"images\apple.png"), new Size(Program.radius, Program.radius));//Image.FromFile(@"images\apple.png"); 

        }
        public Bitmap TintImages(Image snakeImage, Color colorRGB)
        {
            Bitmap bmp = (Bitmap)snakeImage;
            Size snakeImgSize = snakeImage.Size;
            float f = 255f;

            float r = colorRGB.R / f - 0.1f;
            float g = colorRGB.G / f - 0.1f;
            float b = colorRGB.B / f - 0.1f;

            float[][] colorMatrixElements = {
                    new float[] {r,  0,  0,  0, 0},       
                    new float[] {0,  g,  0,  0, 0},         
                    new float[] {0,  0,  b,  0, 0},       
                    new float[] {0,  0,  0,  1, 0},     
                    new float[] {0,  0,  0,  0, 0}
            };          
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);
            Bitmap bmpResult = new Bitmap(snakeImgSize.Width, snakeImgSize.Height);
            using (Graphics gr = Graphics.FromImage(bmpResult))
            {
                gr.DrawImage(snakeImage, new Rectangle(0, 0, snakeImgSize.Width, snakeImgSize.Height),
                0, 0, snakeImgSize.Width, snakeImgSize.Height, GraphicsUnit.Pixel, imageAttributes);                
            }
            return (Bitmap)bmpResult;
        }
        public void chanceColorSnake(Color colorRGB, Image snakeImage)
        {
            //Color de la cabeza => Color.FromArgb(39, 201, 76)
            //Borde 1 de la cabeza => Color.FromArgb(118, 64, 32)
            int marginError = 30;
            Bitmap bmp = (Bitmap)snakeImage;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for(int i = 0; i < marginError; i++)
                    {
                        if (bmp.GetPixel(x, y) == Color.FromArgb(39 + i, 201 + i, 76 + i) || bmp.GetPixel(x, y) == Color.FromArgb(39 - i, 201 - i, 76 - i))
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(bmp.GetPixel(x, y).A, colorRGB.R, colorRGB.G, colorRGB.B));
                        }

                        //BORDE 1 
                        if (bmp.GetPixel(x, y) == Color.FromArgb(106, 158, 104))
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(bmp.GetPixel(x, y).A, 190, 190, 190));
                        }

                        //BORDE 2
                        if (bmp.GetPixel(x, y) == Color.FromArgb(76 + i, 112 + i, 75 + i) || bmp.GetPixel(x, y) == Color.FromArgb(76 - i, 112 - i, 75 - i))
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(bmp.GetPixel(x, y).A, 108 + i, 20 + i, 20 + i));
                        }

                        //CONTORO OJOS
                        if (bmp.GetPixel(x, y) == Color.FromArgb(94 + i, 129 + i, 82 + i) || bmp.GetPixel(x, y) == Color.FromArgb(94 - i, 129 - i, 93 - i))
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(bmp.GetPixel(x, y).A, 70 + i, 70 + i, 70 + i));
                        }
                    }
                }

                
            }
        }
    }

}
