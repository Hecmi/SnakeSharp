using Snake.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Remoting.Contexts;

namespace Snake.images
{
    public partial class frmColorPicker : Form
    {
        float[] valuesSelected = new float[3];
        Form1 formPrincipal;
        int indexSnakeSelected;
        public float[] ValuesSeleted
        {
            get => valuesSelected;
        }
        public Form1 FormPrincipal
        {
            get => formPrincipal;
            set => formPrincipal = value;
        }
        public frmColorPicker()
        {
            InitializeComponent();
        }
        private int id_serpiente;
        private string nombre_jugador;
        public frmColorPicker(Form1 form, int indexSnake, int id, string name)
        {
            InitializeComponent();
            id_serpiente = id;
            nombre_jugador = name;
            txt_NombreJugador.Text = name;
            if (form != null)
            {
                FormPrincipal = form;
                this.indexSnakeSelected = indexSnake;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs eventMouse = e as MouseEventArgs;
            Bitmap b = (Bitmap)(pictureBox1.Image);
            int x = eventMouse.X * b.Width / pictureBox1.ClientSize.Width;
            int y = eventMouse.Y * b.Height / pictureBox1.ClientSize.Height;

            Color c = b.GetPixel(x, y);

            valuesSelected[0] = c.R;
            valuesSelected[1] = c.G;
            valuesSelected[2] = c.B;
            changeColorImage(c);

            // label1.Text = $"R = {c.R}";
            //  label2.Text = $"G = {c.G}";
            //  label3.Text = $"B = {c.B}";

            //txt_NombreJugador.Text = $"Color RGB = {c.R}, {c.G}, {c.B}";
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmColorPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (valuesSelected[0] != 0 || valuesSelected[1] != 0 || valuesSelected[2] != 0)
            {
                Color newColor = Color.FromArgb((int)valuesSelected[0], (int)valuesSelected[1], (int)valuesSelected[2]);
                FormPrincipal.pbSnakesColors[indexSnakeSelected].BackColor = newColor;
                FormPrincipal.changeColorSnakes();
                //229, 0, 0
                FormPrincipal.Colores_nombres(txt_NombreJugador.Text, id_serpiente, (int)valuesSelected[0], (int)valuesSelected[1], (int)valuesSelected[2], true);
            }
            else
            {
                FormPrincipal.Colores_nombres(txt_NombreJugador.Text, id_serpiente, (int)valuesSelected[0], (int)valuesSelected[1], (int)valuesSelected[2], false);
            }
            if (ia.Checked)
                formPrincipal.ActivarIA(id_serpiente, true);
            else
                formPrincipal.ActivarIA(id_serpiente, false);
        }
        private int conteo = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            conteo = conteo + 5;
            // txt_NombreJugador.Text = conteo.ToString();
            if (conteo == 5)
            {
                //conteo = 0;
                cola.Location = new Point(117, 155);
                torso1.Location = new Point(258, 175);
                torso2.Location = new Point(392, 175);
                cabeza.Location = new Point(528, 135);
            }
            else if (conteo == 10)
            {
                cola.Location = new Point(117, 175);
                torso1.Location = new Point(258, 155);
                torso2.Location = new Point(392, 155);
                cabeza.Location = new Point(528, 135);
            }
            else if (conteo == 15)
            {
                cola.Location = new Point(117, 155);
                torso1.Location = new Point(258, 135);
                torso2.Location = new Point(392, 135);
                cabeza.Location = new Point(528, 155);
            }
            else if (conteo == 20)
            {
                cola.Location = new Point(117, 155);
                torso1.Location = new Point(258, 155);
                torso2.Location = new Point(392, 155);
                cabeza.Location = new Point(528, 138);
                conteo = 0;
            }
        }

        private void changeColorImage(Color colorRGB)
        {
            torso1.Image = TintImages(Resources.snake_parte_22, colorRGB);
            torso2.Image = TintImages(Resources.snake_parte_22, colorRGB);
            cola.Image = TintImages(Resources.snake_parte_11, colorRGB);
            cabeza.Image = TintImages(Resources.snake_parte_33, colorRGB);
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
    }
}
