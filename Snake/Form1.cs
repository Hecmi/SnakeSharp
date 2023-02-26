
using SharpDX.DirectInput;
using Snake.classes;
using Snake.images;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer sw;
        //Abrir el formulario de configuracion del jugador 
        
        //bool clock = false;

        //int[] speed = new int[] { 150, 250, 70 };
        tuple.t2<int, Color>[] speed = new tuple.t2<int, Color>[] { new tuple.t2<int, Color>(150, Color.FromArgb(100, Color.LightBlue)), new tuple.t2<int, Color>(250, Color.FromArgb(100, Color.Yellow)), new tuple.t2<int, Color>(70, Color.FromArgb(100, Color.Salmon)) };
        //Tuple<int, Color>[] speed = new Tuple<int, Color>[] { new Tuple<int, Color>(150, Color.FromArgb(100, Color.LightBlue)), new Tuple<int, Color>(250, Color.FromArgb(100, Color.Yellow)), new Tuple<int, Color>(70, Color.FromArgb(100, Color.Salmon)) };
        int[] speed_choose = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 2, 1 };
        int speed_selected = 0;

        System.Windows.Forms.Timer TimerSnake = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerJoystick = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerAI = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerNetwork = new System.Windows.Forms.Timer();
        //posizions[0] -> nuova posizione (nn viene mostrata a video, serve solo x nn perdere la posizione precedente)
        //posizions[1] -> testa del serpente
        //List<Point> positions = new List<Point>();
        List<Snake.classes.Snake> snakes;
        List<int> snakesAI;

        Random rnd = new Random();

        //int radius = 20;
        //int s;
        List<int> killed;// = new List<Point>();
        
        Point Apple;
        Image apple;
        int f_AppleInSquare;

        Dictionary<Point,int> Hole;
        Dictionary<int, Point> HoleTras;
        Image hole;

        List<Point> points;
        Point[] pointsAI;

        bool f_Start;// { get { return (this.snakes.Where(f => f != null && f.x != f.y).Count() > 0 ? true : false); } }
        //bool f_Busy;
        Brush brsh;

        int[] difficultLevels = new int[4];
        int difficultLevelSelected = 100;

        public Form1()
        {            
            this.sw = new System.Windows.Forms.Timer();
            this.sw.Start();

            InitializeComponent();
           // checkBox5.Location = new Point(0, 0); 
            //checkBox5.BringToFront();
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text += string.Format(" - {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);

            label1.Text = "CPU";
            label2.Text = "Players";
            label3.Text = "Score";

            btnNewGame.Text = "New Game!";
            btnBackgroundImage.Text = "Background image";
            btnSetKeys.Text = "Set Keys";
            btnPlayWeb.Text = "Network mode";

            checkBox5.Checked = true;

            /*
            for (int i = 1; i <= 4; i++)
            {
                comboBox1.Items.Add(i);
            }

            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            */
            //this.Focus();
            //this.positions.Add(new Point());
            //Snake();

            Program.Joystick = new List<Joystick>();
            var di = new DirectInput();

            foreach (DeviceInstance device in di.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AllDevices))
            {
                Joystick joystick = new Joystick(di, device.InstanceGuid);
                joystick.Acquire();
                Program.Joystick.Add(joystick);
            }

            Bitmap snake = new Bitmap(@"images\snake.png");

            Rectangle rect;
            rect = new Rectangle(0, 195, 60, 60);
            this.apple = new Bitmap(snake.Clone(rect, snake.PixelFormat), new Size(Program.radius, Program.radius));
            //this.f_AppleInSquare = false;

            this.hole = new Bitmap(new Bitmap(@"images\hole.png"), new Size(Program.radius, Program.radius));

            this.brsh = new SolidBrush(ColorTranslator.FromHtml("#55ffffff"));

            Snake();

            //System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            this.TimerSnake.Interval = this.speed[0].I1;   // milliseconds
            this.TimerSnake.Tick += DrawSnake;  // set handler
            this.TimerSnake.Start();

            this.TimerJoystick.Interval = 50;
            this.TimerJoystick.Tick += JoystickTrigger;
            this.TimerJoystick.Start();

            this.TimerAI.Interval = 50;
            this.TimerAI.Tick += MoveSnakeAI;
            this.TimerAI.Start();
            this.TimerAI.Enabled = false;

            cmbDifficulty.Items.Add("Easy");
            cmbDifficulty.Items.Add("Normal");
            cmbDifficulty.Items.Add("Intermediate");
            cmbDifficulty.Items.Add("Hard");

            difficultLevels[0] = 50;
            difficultLevels[1] = 60;
            difficultLevels[2] = 80;
            difficultLevels[3] = 100;

            cmbDifficulty.SelectedIndex = cmbDifficulty.Items.Count - 1;

            pbSnakesColors.Add(pbColorSnakeOne);
            pbSnakesColors.Add(pbColorSnakeTwo);
            pbSnakesColors.Add(pbColorSnakeThree);
            pbSnakesColors.Add(pbColorSnakeFour);

            
            pbColorSnakeOne.Click += pbColorSnake_Click;
            pbColorSnakeTwo.Click += pbColorSnakeTwo_Click;
            pbColorSnakeThree.Click += pbColorSnakeThree_Click;
            pbColorSnakeFour.Click += pbColorSnakeFour_Click;
            

            //config_1


        }

        private void Snake()
        {
            checkBox1.Text = "";
            checkBox2.Text = "";
            checkBox3.Text = "";
            checkBox4.Text = "";
           
            checkBox5.Text = "# 1 :";
            checkBox6.Text = "# 2 :";
            checkBox7.Text = "# 3 :";
            checkBox8.Text = "# 4 :";

            //checkBox1.Controls.Add(new Label() { Visible = false, Text = checkBox5.Name });
            //checkBox2.Controls.Add(new Label() { Visible = false, Text = checkBox6.Name });
            //checkBox3.Controls.Add(new Label() { Visible = false, Text = checkBox7.Name });
            //checkBox4.Controls.Add(new Label() { Visible = false, Text = checkBox8.Name });
            checkBox5.Controls.Add(new Label() { Visible = false, Text = checkBox1.Name });
            checkBox6.Controls.Add(new Label() { Visible = false, Text = checkBox2.Name });
            checkBox7.Controls.Add(new Label() { Visible = false, Text = checkBox3.Name });
            checkBox8.Controls.Add(new Label() { Visible = false, Text = checkBox4.Name });


           // checkBox9.Controls.Add(new Label() { Visible = false, Text = checkBox1.Name });
            //checkBox10.Controls.Add(new Label() { Visible = false, Text = checkBox2.Name });
           // checkBox11.Controls.Add(new Label() { Visible = false, Text = checkBox3.Name });
            //checkBox12.Controls.Add(new Label() { Visible = false, Text = checkBox4.Name });

            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";

            //this.snakes = new List<classes.Snake>() { new classes.Snake(0), new classes.Snake(1), new classes.Snake(2) };
            this.snakes = new List<classes.Snake>();
            this.snakesAI = new List<int>();

            //List<Point> points = new List<Point>();
            this.points = new List<Point>();

            //for (int i = 0; i < this.snakes.Count; i++)
            //for (int i = 0; i < (int)comboBox1.SelectedItem; i++)

            //int n = -1;

            for (int i = 0; i < 4; i++)
            {
                CheckBox chb = (CheckBox)pnlSnakeOptions.Controls["checkBox" + (i + 5)];

                this.snakes.Add(null);
                
                if (chb.Checked)
                {
                    //n++;

                    this.snakes[i] = new classes.Snake(i);
                    //this.snakes[i]=new classes.Snake();
                    Point p = GetFreePoint();// this.points);
                    //posizione di partenza
                    //this.snakes[i].positions.Add(new Point(((this.rnd.Next(pictureBox1.Width) / Program.radius) + 0) * Program.radius, ((this.rnd.Next(pictureBox1.Height) / Program.radius) + 0) * Program.radius));
                    this.snakes[i].positions.Add(new Tuple<Point, Tuple<int, int, int, int, int, int>>(p, null));//p);
                    //posizione vuota x gestire lo spostamento
                    this.snakes[i].positions.Add(new Tuple<Point, Tuple<int, int, int, int, int, int>>(new Point(), null));
                    this.snakes[i].AI = ((CheckBox)pnlSnakeOptions.Controls["checkBox" + (i + 1)]).Checked;

                    this.points.Add(p);

                    if (this.snakes[i].AI == true)
                    {
                        this.snakesAI.Add(i);
                    }
                    //chb.Text = "Player " + (n + 1);
                }
            }
            this.Apple = GetFreePoint();// this.points);

            //if (classes.NetServer.f_Connect == true)
            //{
            //    SendToClientApple();
            //}

            this.HoleTras = new Dictionary<int, Point>();
            this.HoleTras.Add(0, GetFreePoint());
            this.HoleTras.Add(1, GetFreePoint());
            
            this.Hole = new Dictionary<Point, int>();// { GetFreePoint(), GetFreePoint() };// this.points);
            this.Hole.Add(this.HoleTras[0], 1);
            this.Hole.Add(this.HoleTras[1], 0);

            //this.pointsAI = new Point[this.snakesAI.Count];
            this.pointsAI = new Point[this.snakes.Count];

            //this.f_Busy = false;
            this.f_Start = false;
            //this.snakes[0].AI = true;
            this.TimerAI.Enabled = false;
        }

        private void KillSnake(object sender, EventArgs e)
        {
            //TimerAI.Enabled = false;
            this.TimerAI.Stop();
            KillSnake();
            //TimerAI.Enabled = this.f_Start;
        }
        private void KillSnake()
        {
            //IsFreePoint(new Point(100, 100));
            //classes.Snake s = this.snakes[this.s];

            for (int i = 0; i < this.killed.Count; i++)
            {
                int s = this.killed[i];

                if (this.snakes[s].positions.Count > 0)
                {
                    Point p = new Point(this.snakes[s].positions[0].Item1.X + (Program.radius / 4), this.snakes[s].positions[0].Item1.Y + (Program.radius / 4));

                    using (Graphics G = getGraphics((Bitmap)pictureBox1.Image))
                    {
                        //using (Brush brsh = new SolidBrush(ColorTranslator.FromHtml("#55ffffff")))
                        {
                            G.FillEllipse(this.brsh, p.X, p.Y, Program.radius / 2, Program.radius / 2);
                            this.snakes[s].positions.RemoveAt(0);
                        }
                    }
                    if (classes.NetServer.f_Connect == true)
                    {
                        SendToClientKill(p);
                    }

                    pictureBox1.Image = pictureBox1.Image;
                }
                else
                {
                    this.killed.Remove(s);
                    this.snakes[s] = null;
                }
            }

            //return;

            if (this.killed.Count == 0)
            {
                //this.snakes.RemoveAt(0);

                //this.Timer.Tick -= KillSnake;
                //this.Timer.Tick += DrawSnake;
                //this.Timer.Interval = this.speed[0].I1;

                if (this.snakes.Where(f => f != null).ToList().Count == 0)
                {
                    //Snake();
                }
                else
                {
                    this.TimerSnake.Tick -= KillSnake;
                    this.TimerSnake.Tick += DrawSnake;
                    this.TimerSnake.Interval = this.speed[0].I1;
                    //this.TimerAI.Enabled = this.f_Start;
                }

                //this.f_busy = false;
            }

        }
        private void KillSnake2(Point p)
        {
            using (Graphics G = getGraphics((Bitmap)pictureBox1.Image))
            {
                G.FillEllipse(this.brsh, p.X, p.Y, Program.radius / 2, Program.radius / 2);
                pictureBox1.Image = pictureBox1.Image;
            }
        }

        private void DrawSnake(object sender, EventArgs e)//int x, int y)
        {
            //this.Apple = new Point(420, 60);
            //this.snakes[0].positions = new List<Point>() { new Point(40, 0), new Point(40, 0), new Point(40, 20), new Point(40, 40), new Point(40, 60), new Point(40, 80), new Point(60, 80), new Point(60, 60), new Point(60, 40), new Point(60, 20), new Point(60, 0), new Point(80, 0), new Point(80, 20), new Point(80, 40), new Point(80, 60), new Point(80, 80), new Point(80, 100), new Point(80, 120), new Point(80, 140), new Point(80, 160), new Point(80, 180), new Point(80, 200), new Point(80, 220), new Point(60, 220), new Point(60, 240), new Point(60, 260), new Point(40, 260), new Point(20, 260), new Point(0, 260), new Point(0, 280), new Point(0, 0), new Point(0, 20), new Point(0, 40), new Point(0, 60), new Point(20, 60), new Point(20, 80), new Point(20, 100), new Point(0, 100), new Point(380, 100), new Point(360, 100), new Point(340, 100), new Point(80, 260) };
            //this.snakes[0].positions = new List<Point>() { new Point(140, 140), new Point(140, 140), new Point(160, 140), new Point(180, 140), new Point(200, 140), new Point(220, 140), new Point(240, 140), new Point(260, 140), new Point(280, 140), new Point(280, 160), new Point(280, 180), new Point(280, 200), new Point(280, 220), new Point(280, 240), new Point(280, 260), new Point(300, 260), new Point(320, 260), new Point(340, 260), new Point(360, 260), new Point(380, 260), new Point(400, 260), new Point(420, 260), new Point(420, 280), new Point(420, 0), new Point(420, 20), new Point(420, 40), new Point(420, 60), new Point(420, 80), new Point(420, 100), new Point(420, 120) };
            //this.snakes[0].positions = new List<Point>() { new Point(320, 140), new Point(320, 140), new Point(320, 120), new Point(320, 100), new Point(340, 100), new Point(360, 100), new Point(380, 100), new Point(400, 100), new Point(400, 120), new Point(380, 120), new Point(360, 120), new Point(360, 140), new Point(380, 140), new Point(400, 140), new Point(420, 140), new Point(440, 140), new Point(440, 160), new Point(440, 180), new Point(440, 200), new Point(460, 200), new Point(480, 200), new Point(500, 200), new Point(520, 200), new Point(520, 220), new Point(520, 240), new Point(520, 260), new Point(520, 280), new Point(0, 280), new Point(20, 280), new Point(40, 280), new Point(60, 280), new Point(80, 280) };
            //this.snakes[0].positions = new List<Point>() { new Point(400, 0), new Point(400, 0), new Point(380, 0), new Point(360, 0), new Point(340, 0), new Point(320, 0), new Point(300, 0), new Point(280, 0), new Point(260, 0), new Point(240, 0), new Point(220, 0), new Point(220, 280), new Point(220, 260), new Point(220, 240), new Point(220, 220), new Point(240, 220), new Point(260, 220), new Point(280, 220), new Point(300, 220), new Point(320, 220), new Point(340, 220), new Point(360, 220), new Point(380, 220), new Point(400, 220), new Point(420, 220), new Point(440, 220), new Point(440, 240), new Point(440, 260), new Point(440, 280), new Point(440, 0), new Point(440, 20), new Point(440, 40), new Point(440, 60) };

            //x evitare che AI valuti su una lista di punti vuota, lo metto in pausa finchè sposto i serpenti
            //spostato all interno del metodo, x fare in modo che si possa riattivare dopo lo spostamento dei serpenti
            //this.TimerAI.Enabled = false;
            DrawSnake();
            //this.TimerAI.Enabled = this.f_Start;
        }
        private void DrawSnake()//int x, int y)
        {
            if (pictureBox1.Height <= 0 || pictureBox1.Width <= 0)
            {
                return;
            }

            //bool yy = this.f_start;
            //label1.Select();

            //this.f_busy = true;
            this.killed = new List<int>();
            
            classes.Snake snake;
            Bitmap t_bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height); //(Bitmap)bmp.Clone();
                                                                              //List<Point> points = new List<Point>();

            //x evitare che AI valuti su una lista di punti vuota, lo metto in pausa finchè sposto i serpenti
            this.TimerAI.Stop();
            //this.TimerAI.Enabled = false;

            this.points = new List<Point>();
            this.pointsAI = new Point[this.pointsAI.Length];

            using (Graphics G = getGraphics(t_bmp))
            {
                //List<Point> points = 
                for (int s = 0; s < this.snakes.Count; s++)
                {
                    snake = this.snakes[s];

                    if (snake == null)
                    {
                        continue;
                    }

                    //snake.f_busy = true;

                    //snake.positions[0] = PointCheck(snake.positions[0], snake.x, snake.y);
                    snake.positions[0] = new Tuple<Point, Tuple<int, int, int, int, int, int>>(PointCheck(snake.positions[0].Item1, snake.x, snake.y), snake.positions[0].Item2);
                    
                    //prima disegno a ritroso, prendendo la posizione dell elemento predente
                    for (int i = snake.positions.Count - 1; i >= 1; i--)
                    {
                        snake.positions[i] = snake.positions[i - 1];

                        if (i >= 2)
                        {
                            Point tmp;

                            //se passo attraverso il tunnel sbaglio a calcolare la direzione,
                            // x cui se il punto del serpente è uno dei due buchi, allora uso il buco
                            // precedente come punto di riferimento x la direzione
                            if (this.Hole.ContainsKey(snake.positions[i - 2].Item1))
                            {
                                tmp = this.HoleTras[this.Hole[snake.positions[i - 2].Item1]];
                            }
                            else
                            {
                                tmp = snake.positions[i - 2].Item1;
                            }

                            //guardo la posizione che avrà il punto successivo, nn qlla attuale che verrà cambiata
                            snake.x2 = snake.positions[i].Item1.X - tmp.X;//snake.positions[i - 2].X;
                            snake.y2 = snake.positions[i].Item1.Y - tmp.Y;//snake.positions[i - 2].Y;

                            snake.x2 = (snake.x2 < 0 ? Math.Max(-Program.radius * 2, snake.x2) : Math.Min(Program.radius * 2, snake.x2));
                            snake.y2 = (snake.y2 < 0 ? Math.Max(-Program.radius * 2, snake.y2) : Math.Min(Program.radius * 2, snake.y2));

                            if (snake.positions.Count - 1 == i)
                            {
                                G.DrawImage(snake.tail, snake.positions[i].Item1.X, snake.positions[i].Item1.Y);
                            }
                            else
                            {
                                snake.x3 = snake.positions[i + 1].Item1.X - snake.positions[i].Item1.X;
                                snake.y3 = snake.positions[i + 1].Item1.Y - snake.positions[i].Item1.Y;

                                snake.x3 = (snake.x3 < 0 ? Math.Max(-Program.radius * 2, snake.x3) : Math.Min(Program.radius * 2, snake.x3));
                                snake.y3 = (snake.y3 < 0 ? Math.Max(-Program.radius * 2, snake.y3) : Math.Min(Program.radius * 2, snake.y3));

                                //G.FillEllipse(brsh, this.positions[i].X, this.positions[i].Y, radius, radius);
                                G.DrawImage(snake.body, snake.positions[i].Item1.X, snake.positions[i].Item1.Y);
                            }

                            //G.FillEllipse(brsh, this.positions[i].X, this.positions[i].Y, radius, radius);
                        }
                        else
                        {
                            G.DrawImage(snake.head, snake.positions[i].Item1.X, snake.positions[i].Item1.Y);
                        }

                        this.points.Add(snake.positions[i].Item1);
                        snake.positions[i] = new Tuple<Point, Tuple<int, int, int, int, int, int>>(snake.positions[i].Item1, new Tuple<int, int, int, int, int, int>(snake.x, snake.y, snake.x2, snake.y2, snake.x3, snake.y3));
                    }

                    //this.points.AddRange(snake.positions.Select(f => f.Item1).ToList());

                    //ora posso permettere ai giocatori di fare una nuova mossa
                    snake.f_busy = false;
                    this.TimerAI.Enabled = this.f_Start;

                    if (this.TimerAI.Enabled)
                    {
                        this.TimerAI.Start();
                    }
                }
                
                //invio ai client le info sui serpenti al momento in cui anche a sè stesso mostro il risultato
                if (classes.NetServer.f_Connect == true)
                {
                    SendToClientDraw();
                }
                //dopo aver spostato i serpenti aggiorno l immagine
                pictureBox1.Image = t_bmp;

                //il gioco viene gestito dal server, i client inviano gli spostamenti e ricevono il risultato
                //if (Program.Server != null)
                //if(classes.NetClient.f_Connect == true)
                //{
                //    return;
                //}

                //ora controllo se ci sono stati schianti o han preso mele
                for (int s = 0; s < this.snakes.Count; s++)
                {
                    snake = this.snakes[s];

                    if (snake == null)
                    {
                        continue;
                    }

                    int c;

                    c = (this.Apple.X == snake.positions[1].Item1.X && this.Apple.Y == snake.positions[1].Item1.Y == true ? 1 : 0);

                    if (c > 0)
                    {
                        snake.positions.Add(new Tuple<Point, Tuple<int, int, int, int, int, int>>(new Point(), null));

                        //((Label)this.Controls["label" + (snake.i + 1)]).Text = "Player " + (snake.i + 1) + ": " + (snake.positions.Count - 2);
                        //((Label)this.panel1.Controls["label" + (snake.i + 5)]).Text = " : " + (snake.positions.Count - 2);

                        //cambio velocità
                        this.speed_selected = this.rnd.Next(0, this.speed_choose.Length);
                        this.TimerSnake.Interval = this.speed[this.speed_choose[this.speed_selected]].I1;

                        this.Apple = GetFreePoint();// this.points);

                        //if (classes.NetServer.f_Connect == true)
                        //{
                        //    SendToClientApple();
                        //}

                        this.f_AppleInSquare = IsInSquare(this.Apple);
                    }

                    //((Label)this.pnlSnakeOptions.Controls["puntos0"]).Text = "mierda";
                    // CheckBox chb = (CheckBox)pnlSnakeOptions.Controls["checkBox" + (i + 5)];
                    // Label lbl = ((Label)pnlSnakeOptions.Controls["puntos"+(snake.i)]);
                    //lbl.Text = (snake.positions.Count -2).ToString();

                     ((Label)this.pnlSnakeOptions.Controls["label" + (snake.i + 5)]).Text = (snake.positions.Count - 2).ToString();
                    puntos0.Text = label5.Text;
                    puntos1.Text = label6.Text;
                    puntos2.Text = label7.Text;
                    puntos3.Text = label8.Text;
                    //Console.WriteLine(snake.positions.Count-2);

                    



                    G.DrawImage(this.apple, this.Apple.X, this.Apple.Y);

                    c = this.points.Where(f => snake.positions[1].Item1.X == f.X && snake.positions[1].Item1.Y == f.Y).Count();

                    if (c > 1 && snake.x != snake.y)
                    {
                        this.killed.Add(s);

                        //this.Timer.Tick -= DrawSnake;
                        //this.Timer.Tick += KillSnake;
                        //this.Timer.Interval = 100;

                        //return;
                        //Thread.Sleep(2000);
                    }
                }


                G.DrawImage(this.hole, this.HoleTras[0].X, this.HoleTras[0].Y);
                G.DrawImage(this.hole, this.HoleTras[1].X, this.HoleTras[1].Y);

                if (this.killed.Count > 0)
                {
                    this.TimerSnake.Tick -= DrawSnake;
                    this.TimerSnake.Tick += KillSnake;
                    this.TimerSnake.Interval = 100;
                    //this.TimerAI.Enabled = false;
                }

            }

            //if (classes.NetServer.f_Connect == true)
            //{
            //    SendToClient();
            //}
            //this.f_busy = false;
        }

        private void DrawSnake2()
        {
            //Console.WriteLine("DrawSnake2: " + this.sw.Elapsed.Milliseconds);
            this.sw.Stop();
            this.sw.Start();

            //return;

            if (pictureBox1.Height <= 0 || pictureBox1.Width <= 0)
            {
                return;
            }

            classes.Snake snake;
            Bitmap t_bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height); //(Bitmap)bmp.Clone();

            using (Graphics G = getGraphics(t_bmp))//(Bitmap)pictureBox1.Image))
            {
                for (int s = 0; s < this.snakes.Count; s++)
                {
                    snake = this.snakes[s];

                    if (snake == null)
                    {
                        continue;
                    }

                    //Console.WriteLine("DrawSnake2 count : " + snake.positions.Count);

                    for (int i = 1; i < snake.positions.Count; i++)
                    {
                        //if (snake.positions[i].Item2 != null)
                        {                            
                            if (i == 1)
                            {
                                G.DrawImage(snake.head, snake.positions[i].Item1.X, snake.positions[i].Item1.Y);
                                continue;
                            }
                            else
                            {
                                if (i < snake.positions.Count - 1) 
                                {
                                    snake.x2 = snake.positions[i].Item2.Item3;
                                    snake.y2 = snake.positions[i].Item2.Item4;
                                    snake.x3 = snake.positions[i].Item2.Item5;
                                    snake.y3 = snake.positions[i].Item2.Item6;

                                    G.DrawImage(snake.body, snake.positions[i].Item1.X, snake.positions[i].Item1.Y);
                                }
                                else
                                {
                                    snake.x2 = snake.positions[i].Item2.Item3;
                                    snake.y2 = snake.positions[i].Item2.Item4;

                                    G.DrawImage(snake.tail, snake.positions[i].Item1.X, snake.positions[i].Item1.Y);
                                }
                            }
                        }
                    }

                    //((Label)this.pnlSnakeOptions.Controls["puntos" + 1]).Text = " : " + (snake.positions.Count - 2);
                }

                G.DrawImage(this.apple, this.Apple.X, this.Apple.Y);
                /*
                G.DrawImage(this.hole, this.HoleTras[0].X, this.HoleTras[0].Y);
                G.DrawImage(this.hole, this.HoleTras[1].X, this.HoleTras[1].Y);
                */
            }

            pictureBox1.Image = t_bmp;
        }

        private void MoveSnake(int key)
        {
            this.f_Start = true;

            if (Program.keys.ContainsKey(key))
            {
                tuple.t2<int, int> k = Program.keys[key];

                if (this.snakes.Count > k.I1)
                {
                    classes.Snake s = this.snakes[k.I1];

                    if (s != null)
                    {
                        CheckBox chb = (CheckBox)pnlSnakeOptions.Controls["checkBox" + (s.i + 5)];

                        if (s.f_busy == false && chb.Enabled == true)
                        {
                            //nn posso cambiare direzione finchè nn viene spostato il serpente
                            s.f_busy = true;

                            switch (k.I2)
                            {
                                case 0:
                                    {
                                        s.x = 0;
                                        s.y = (s.y == +Program.radius ? s.y : -Program.radius);
                                    }
                                    break;
                                case 1:
                                    {
                                        s.x = (s.x == +Program.radius ? s.x : -Program.radius);
                                        s.y = 0;
                                    }
                                    break;
                                case 2:
                                    {
                                        s.x = (s.x == -Program.radius ? s.x : +Program.radius);
                                        s.y = 0;
                                    }
                                    break;
                                case 3:
                                    {
                                        s.x = 0;
                                        s.y = (s.y == -Program.radius ? s.y : +Program.radius);
                                    }
                                    break;
                            }

                            if (classes.NetClient.f_Connect == true)
                            {
                                /*
                                SendToServerDraw(s);
                                */
                            }
                        }

                        //Console.WriteLine("s " + k.I1 + "x " + s.x + " y " + s.y);
                    }
                }
            }
        }

        private void JoystickTrigger(object sender, EventArgs e)
        {
            for (int i = 0; i < Program.Joystick.Count; i++)
            {
                int key = Keys.GetJoykey(i);
                MoveSnake(key);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, System.Windows.Forms.Keys keyData)
        {
            MoveSnake((int)keyData);

            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            //label1.Select();

            if (keyData == System.Windows.Forms.Keys.Up || keyData == System.Windows.Forms.Keys.Left || keyData == System.Windows.Forms.Keys.Right || keyData == System.Windows.Forms.Keys.Down)
            {
                return false;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void MoveSnakeAI(object sender, EventArgs e)
        {
            //Verificar que existan serpientes con el modo AI activado.
            if (this.snakesAI != null && this.snakesAI.Count > 0)
            {
                classes.Snake snake;// = this.snakes[0];

                //creo una lista ordinata causalmente x evitare di prendere i serpenti sempre nello stesso ordine                
                int[] rnd = Enumerable.Range(0, this.snakesAI.Count).OrderBy(f => Guid.NewGuid()).ToArray();

                //Recorrer cada uno de los valores enteros que representan una serpiente
                foreach (int s in rnd)
                {
                    snake = this.snakes[this.snakesAI[s]];

                    if (snake == null || snake.AI == false)
                    {
                        continue;
                    }

                    Point ApplePosition = this.Apple;

                    //se qlcno è vicino alla mela cambio direzione...è inutile provarci se è + vicino di me
                    bool f_NearApple = false; //NearApple(snake.i);

                    
                    if (f_NearApple)
                    {
                        if (snake.FakeApple.X == -1)
                        {

                            snake.FakeApple = PointCheck(GetFreePoint(), 0, 0);
                        }
                        ApplePosition = snake.FakeApple;
                    }
                    else
                    {
                        snake.FakeApple = new Point(-1, -1);
                        //NearHole(snake.i);

                        ApplePosition = this.Apple;
                    }

                    //classes.SnakeAI snakeAI = new classes.SnakeAI();
                    int[] xy = new int[2];
                    //bool[] f_IsInSquare = new bool[xy.Length];
                    int[] f_IsTunnel = new int[xy.Length];
                    //bool f_AppleInSquare = false;

                    int[] f_X = new int[] { +1, +0, -1, +0 };
                    int[] f_Y = new int[] { +0, -1, +0, +1 };


                    xy[0] = pictureBox1.Width + pictureBox1.Height;
                    //xy[1] = xy[0];

                    f_IsTunnel[0] = 99;
                    f_IsTunnel[1] = f_IsTunnel[0];

                    //f_AppleInSquare = (IsInSquare(this.Apple) >= 4 ? true : false);

                    System.Diagnostics.Debug.WriteLine("***************************");
                    System.Diagnostics.Debug.WriteLine("snake " + snake.i);
                    System.Diagnostics.Debug.WriteLine("snake " + snake.positions[1]);
                    System.Diagnostics.Debug.WriteLine("");

                    //int uu = 0;
                    Point tmp_Point = new Point();

                    //Recorrer las posiciones de movimientos en X.
                    for (int i = 0; i < f_X.Length; i++)
                    {
                        //Point tmp_Point = new Point(snake.positions[1].X + (f_X[i] * Program.radius), snake.positions[1].Y + (f_Y[i] * Program.radius));
                        tmp_Point = PointCheck(snake.positions[1].Item1, f_X[i] * Program.radius, f_Y[i] * Program.radius);

                        if (IsFreePoint(tmp_Point) && IsFreePointAI(tmp_Point, snake.i))
                        {
                            //if (IsTunnel(tmp_Point, f_X[i], f_Y[i]) == false)
                            {
                                //f_IsInSquare[1] = (IsInSquare(tmp_Point) >= 4 ? true : false);
                                f_IsTunnel[1] = IsTunnel(tmp_Point, f_X[i], f_Y[i]);
                                //x evaluar la distancia entre la manzana y la serpiente muevo la manzana en x / y
                                for (int j = 0; j < 2; j++)
                                {
                                
                                    xy[1] = Math.Abs(ApplePosition.X - tmp_Point.X) + Math.Abs(ApplePosition.Y - tmp_Point.Y);

                                    System.Diagnostics.Debug.WriteLine("j " + j);
                                    System.Diagnostics.Debug.WriteLine("tmp_Point " + tmp_Point);
                                    System.Diagnostics.Debug.WriteLine("xy[0] " + xy[0] + " xy[1] " + xy[1]);
                                    System.Diagnostics.Debug.WriteLine("f_IsTunnel[0] " + f_IsTunnel[0] + " f_IsTunnel[1] " + f_IsTunnel[1]);

                                    //Minimo 50, máximo 100
                                    int dificultad = difficultLevelSelected;
                                    Random rndA = new Random();
                                    var value = rndA.Next(1, 100 + 1);
                                    bool canMove = false;
                                    canMove = value > 100 - dificultad  ? true : false;
                                    //if (f_IsTunnel[1] != 3 && canMove)
                                    int [] registro = new int[2];
                                    if (f_IsTunnel[1] != 3)
                                    {
                                        if (xy[0] >= xy[1])// && f_IsTunnel[0] > f_IsTunnel[1])
                                        {
                                            if (canMove)
                                            {
                                                if(!(registro[0] == f_X[i]) || !(registro[1] == f_Y[i]))
                                                {
                                                    snake.x = (f_X[i] * Program.radius);
                                                    snake.y = (f_Y[i] * Program.radius);
                                                }                                                
                                            }
                                            

                                            registro[0] = f_X[i];
                                            registro[1] = f_Y[i];

                                            
                                            xy[0] = xy[1];
                                            f_IsTunnel[0] = f_IsTunnel[1];

                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //quando calcolo una posizione libera nn ho idea che lo sia anche x qlcn altro, x cui
                    // rischio che alla fine + serpenti si scontrino
                    //selezionando casualmente i serpenti faccio in modo che i successivi 'sappiano' la mossa dei precenti
                    // come se un giocatore 'intuisse' la mossa dell avversario
                    //x fare questo aggiungo nella lista dei punti occupati la posizione in cui andrà a posizionarsi
                    this.pointsAI[snake.i] = PointCheck(snake.positions[0].Item1, snake.x, snake.y);

                    System.Diagnostics.Debug.WriteLine("ApplePosition " + ApplePosition);
                    System.Diagnostics.Debug.WriteLine("appleSquare " + f_AppleInSquare);
                    //System.Diagnostics.Debug.WriteLine("tmp_Point " + tmp_Point);
                    System.Diagnostics.Debug.WriteLine("xy " + snake.x + " " + snake.y);
                }

            }
            
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        //label1.Text = this.positions.Count + "";                
                    }
                    break;
                case MouseButtons.Right:
                    {
                        //break; 
                        //this.snakes[0].positions.Add(new Point());
                        //this.snakes[1].positions.Add(new Point());

                        foreach (Snake.classes.Snake snake in this.snakes)
                        {
                            if (snake != null)
                            {
                                snake.positions.Add(new Tuple<Point, Tuple<int, int, int, int, int, int>>(new Point(), null));
                            }
                        }
                    }
                    break;
                case MouseButtons.Middle:
                    {
                        //this.speed[0] = new Tuple<int, Color>(500, this.speed[0].Item2);
                    }
                    break;
            }
        }

        //private void SendToClient(object sender, EventArgs e)//int x, int y)
        //{
        //    SendToClientDraw();
        //}
        private void SendToClientSettings()
        {
            classes.NetWorkSettings settings = new classes.NetWorkSettings(classes.NetWorkType.Settings);

            //
            //settings.checkboxes = new bool[8];
            settings.checkboxes = new Tuple<string, bool, string>[8];

            for (int i = 0; i < 8; i++)
            {
                CheckBox chb = (CheckBox)pnlSnakeOptions.Controls["checkBox" + (i + 1)];

                if (chb.Checked && chb.Tag == null)//!chb.Tag.Equals(Program.id))
                {
                    chb.Tag = Program.id;
                }

                settings.checkboxes[i] = new Tuple<string, bool, string>((string)chb.Tag, chb.Checked, chb.Name);//chb.Checked;
            }

            string s = Newtonsoft.Json.JsonConvert.SerializeObject(settings);

            if (Program.NetPlayers != null)
            {
                for (int i = 0; i < Program.NetPlayers.Count; i++)
                {
                    classes.NetWork.Send(Program.NetPlayers[i], s);
                }
            }
        }
        private void SendToClientDraw()
        {
            //Console.WriteLine("SendToClientDraw: " + this.sw.Elapsed.Milliseconds);
            this.sw.Stop();
            this.sw.Start();

            classes.NetWorkDraw draw = new classes.NetWorkDraw(classes.NetWorkType.Draw);
            
            draw.Snakes = new List<classes.SnakeLite>();

            for (int j = 0; j < this.snakes.Count; j++)
            {
                classes.Snake snake = snakes[j];

                if (snake != null)
                {
                    draw.Snakes.Add(new classes.SnakeLite() { i = snake.i, x = snake.x, y = snake.y, positions = snake.positions, f_busy = snake.f_busy });
                }
                else
                {
                    draw.Snakes.Add(new classes.SnakeLite() { i = -1 });
                }
            }

            draw.Apple = this.Apple;
            draw.Hole = this.Hole;
            draw.HoleTras = this.HoleTras;
            draw.pictureBox1Height = this.Height;
            draw.pictureBox1Width = this.Width;

            string s = Newtonsoft.Json.JsonConvert.SerializeObject(draw);

            for (int i = 0; i < Program.NetPlayers.Count; i++)
            {
                classes.NetWork.Send(Program.NetPlayers[i], s);
            }

        }
        private void SendToClientKill(Point p)
        {
            classes.NetWorkKill kill = new classes.NetWorkKill(classes.NetWorkType.Kill);

            kill.p = p;

            string s = Newtonsoft.Json.JsonConvert.SerializeObject(kill);
            Console.WriteLine(s);

            for (int i = 0; i < Program.NetPlayers.Count; i++)
            {
                classes.NetWork.Send(Program.NetPlayers[i], s);
            }
        }

        private void ReceiveFromClient(object sender, EventArgs e)//int x, int y)
        {
            ReceiveFromClient();
        }
        private void ReceiveFromClient()
        {
            //string[][] s = new string[Program.NetPlayers.Count][];

            for (int x = 0; x < Program.NetPlayers.Count; x++)
            {
                string[] s;
                //string s;

                s = classes.NetWork.Receive(Program.NetPlayers[x]);
            
                int min = Math.Min(10, s.Length);

                for (int j = 0; j < min; j++)
                {
                    string v = s[s.Length - min + j];

                    if (v != "")
                    {
                        classes.NetWorkLite NetWorkLite = Newtonsoft.Json.JsonConvert.DeserializeObject<classes.NetWorkLite>(v);

                        switch ((int)NetWorkLite.NetWorkType)
                        {
                            case (int)classes.NetWorkType.Settings:
                                {
                                    classes.NetWorkSettings players = Newtonsoft.Json.JsonConvert.DeserializeObject<classes.NetWorkSettings>(v);

                                    for (int i = 0; i < players.checkboxes.Length; i++)
                                    {
                                        //if (players.checkboxes[i] != null)
                                        {
                                            CheckBox chb = (CheckBox)pnlSnakeOptions.Controls[players.checkboxes[i].Item3];

                                            //disattivo la propagazione di eventi 
                                            chb.Enabled = false;
                                            chb.Checked = players.checkboxes[i].Item2;
                                            chb.Tag = players.checkboxes[i].Item1;

                                            if (chb.Tag == null)
                                            {
                                                chb.Enabled = true;
                                            }
                                        }
                                    }

                                    for (int i = 4; i < 8; i++)
                                    {
                                        CheckBox chb = (CheckBox)pnlSnakeOptions.Controls["checkBox" + (i + 1)];
                                        //if (chb.Controls.Count > 0)
                                        {
                                            ((CheckBox)pnlSnakeOptions.Controls[((Label)chb.Controls[0]).Text]).Enabled = chb.Enabled;
                                        }
                                    }

                                    SendToClientSettings();
                                }
                                break;
                            case (int)classes.NetWorkType.Draw:
                                {
                                    classes.NetWorkDraw snakes = Newtonsoft.Json.JsonConvert.DeserializeObject<classes.NetWorkDraw>(v);

                                    //                    
                                    for (int i = 0; i < snakes.Snakes.Count; i++)
                                    {
                                        //this.sw.Restart();
                                        classes.SnakeLite snakelite = snakes.Snakes[i];
                                        //System.Diagnostics.Debug.WriteLine("b " + this.sw.Elapsed.Milliseconds);

                                        //this.sw.Restart();
                                        if (this.snakes[snakelite.i] != null)
                                        {
                                            this.snakes[snakelite.i].x = snakelite.x;
                                            this.snakes[snakelite.i].y = snakelite.y;
                                            this.snakes[snakelite.i].f_busy = false;
                                        }
                                        //System.Diagnostics.Debug.WriteLine("c " + i + " - " + this.sw.Elapsed.Milliseconds);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        //private void SendToServer(object sender, EventArgs e)//int x, int y)
        //{
        //    SendToServer();
        //}
        private void SendToServerSettings(CheckBox chb)
        {
            classes.NetWorkSettings settings = new classes.NetWorkSettings(classes.NetWorkType.Settings);

            //
            settings.checkboxes = new Tuple<string, bool, string>[1];

            //le prime 4 chb le passo sempre attive, saranno le chb dal 5 all 8 che le gestioscono 
            settings.checkboxes[0] = new Tuple<string, bool, string>((string)chb.Tag, chb.Checked, chb.Name);//chb.Checked;
            
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(settings);

            classes.NetWork.Send(Program.Server, s);
            
        }

        private void SendToServerDraw(classes.Snake snake)
        {
            classes.NetWorkDraw player = new classes.NetWorkDraw(classes.NetWorkType.Draw);

            player.Snakes = new List<classes.SnakeLite> { (classes.SnakeLite)snake };
            
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(player);
            
            classes.NetWork.Send(Program.Server, s);

        }

        private void ReceiveFromServer(object sender, EventArgs e)//int x, int y)
        {
            //this.sw.Restart();
            this.sw.Start();

            ReceiveFromServer();

            //Console.WriteLine("ReceiveFromServer: " + this.sw.Elapsed.Milliseconds);
        }
        private void ReceiveFromServer()
        {
            //while (true)
            {
                //Thread.Sleep(10000);
                string[] s;
                //string s;

                s = classes.NetWork.Receive(Program.Server);

                //if (s == null)
                //{
                //    button4_Click(null, null);
                //}

                int min = Math.Min(10, s.Length);

                //Console.WriteLine("ReceiveFromServer - min : " + min);

                for (int j = 0; j < min; j++)
                {
                    string v = s[s.Length - min + j];

                    //Console.WriteLine("ReceiveFromServer - v : " + v);

                    if (v != "")
                    {
                        try
                        {
                            classes.NetWorkLite NetWorkLite = Newtonsoft.Json.JsonConvert.DeserializeObject<classes.NetWorkLite>(v);
                            //Console.WriteLine("ReceiveFromServer - NetWorkType : " + NetWorkLite.NetWorkType);

                            switch ((int)NetWorkLite.NetWorkType)
                            {
                                case (int)classes.NetWorkType.Settings:
                                    {
                                        classes.NetWorkSettings players = Newtonsoft.Json.JsonConvert.DeserializeObject<classes.NetWorkSettings>(v);

                                        for (int i = 0; i < players.checkboxes.Length; i++)
                                        {
                                            CheckBox chb = (CheckBox)pnlSnakeOptions.Controls["checkBox" + (i + 1)];

                                            //disattivo la propagazione di eventi
                                            chb.Enabled = false;
                                            chb.Checked = players.checkboxes[i].Item2;
                                            chb.Tag = players.checkboxes[i].Item1;

                                            if (chb.Tag == null || ((string)chb.Tag).Equals(Program.id) || chb.Controls.Count == 0)
                                            {
                                                chb.Enabled = true;
                                            }

                                            if (chb.Controls.Count > 0)
                                            {
                                                ((CheckBox)pnlSnakeOptions.Controls[((Label)chb.Controls[0]).Text]).Enabled = chb.Enabled;
                                            }
                                        }
                                    }
                                    break;

                                case (int)classes.NetWorkType.Draw:
                                    {
                                        classes.NetWorkDraw draw = Newtonsoft.Json.JsonConvert.DeserializeObject<classes.NetWorkDraw>(v);

                                        //                    
                                        for (int i = 0; i < this.snakes.Count; i++)
                                        {
                                            //this.sw.Restart();
                                            classes.SnakeLite snakelite = draw.Snakes[i];
                                            //System.Diagnostics.Debug.WriteLine("b " + this.sw.Elapsed.Milliseconds);

                                            //this.sw.Restart();
                                            if (i == snakelite.i)
                                            {
                                                if (this.snakes[i] == null)
                                                {
                                                    this.snakes[i] = new classes.Snake(i);
                                                }

                                                this.snakes[i].i = snakelite.i;
                                                this.snakes[i].x = snakelite.x;
                                                this.snakes[i].y = snakelite.y;
                                                this.snakes[i].positions = snakelite.positions;
                                                this.snakes[i].f_busy = snakelite.f_busy;
                                            }
                                            else
                                            {
                                                this.snakes[i] = null;
                                            }
                                            //System.Diagnostics.Debug.WriteLine("c " + i + " - " + this.sw.Elapsed.Milliseconds);
                                        }

                                        this.Apple = draw.Apple;
                                        this.Hole = draw.Hole;
                                        this.HoleTras = draw.HoleTras;
                                        this.Height = draw.pictureBox1Height;
                                        this.Width = draw.pictureBox1Width;

                                        DrawSnake2();
                                    }
                                    break;

                                case (int)classes.NetWorkType.Kill:
                                    {
                                        classes.NetWorkKill kill = Newtonsoft.Json.JsonConvert.DeserializeObject<classes.NetWorkKill>(v);

                                        KillSnake2(kill.p);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("exception.!!:");
                            Console.WriteLine(e.ToString());
                            Console.WriteLine("******************");
                            Console.WriteLine(v);
                            Console.WriteLine("******************");
                        }
                    }
                }
                
                //DrawSnake2();

                //Thread.Sleep(20);
            }
        }

        private Graphics getGraphics(Bitmap image)
        {
            Graphics G;
            G = Graphics.FromImage(image);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.InterpolationMode = InterpolationMode.HighQualityBicubic;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return G;
        }

        //verifico se il punto è all interno del box, se no lo traslo x farlo passare da una parte all altra
        //private Point PointCheck(Point p, PictureBox pb, int x, int y, int radius)
        private Point PointCheck(Point p, int x, int y)
        {
            p.X += x;

            if (p.X < 0)
            {
                p.X = pictureBox1.Width-1;
            }
            else
            {
                if (p.X >= pictureBox1.Width)
                {
                    p.X = 0;
                }
            }

            p.X = ((p.X / Program.radius)) * Program.radius;
            

            p.Y += y;

            if (p.Y < 0)
            {
                p.Y = pictureBox1.Height-1;
            }
            else
            {
                if (p.Y >= pictureBox1.Height)
                {
                    p.Y = 0;
                }
            }

            p.Y = ((p.Y / Program.radius)) * Program.radius;

            /*
            if (p.X == this.Hole[0].X && p.Y == this.Hole[0].Y)
            {
                p = this.Hole[1];
            }
            else
            {
                if (p.X == this.Hole[1].X && p.Y == this.Hole[1].Y)
                {
                    p = this.Hole[0];
                }
            }
            */

            if (this.Hole.ContainsKey(p))
            {
                p = this.HoleTras[this.Hole[p]];
            }

            return p;
        }
        
        private Point GetFreePoint()//List<Point> points)
        {
            //for (int i = 0; i < this.snakes.Count; i++)
            {
                int c = 1;
                int x = 0;
                int y = 0;

                List<Point> tmp = new List<Point>();

                tmp.AddRange(this.points);
                tmp.Add(this.Apple);

                /*
                if (this.Hole != null)
                {
                    if (this.Hole[0] != null)
                    {
                        tmp.Add(this.Hole[0]);
                    }
                    if (this.Hole[1] != null)
                    {
                        tmp.Add(this.Hole[1]);
                    }
                }
                */

                if (this.Hole != null)
                {
                    foreach (KeyValuePair<Point, int> k in this.Hole)
                    {
                        tmp.Add(k.Key);
                    }
                }

                for (int i = 0; i < this.snakes.Count; i++)
                {
                    if (snakes[i] != null && snakes[i].FakeApple.X > 0)
                    {
                        tmp.Add(snakes[i].FakeApple);
                    }
                }
                                
                while (c > 0)
                {
                    x = ((this.rnd.Next(pictureBox1.Width) / Program.radius) + 0) * Program.radius;
                    y = ((this.rnd.Next(pictureBox1.Height) / Program.radius) + 0) * Program.radius;

                    //c = this.points.Where(f => x == f.X && y == f.Y).Count();
                    c = tmp.Where(f => x == f.X && y == f.Y).Count();
                }

                System.Diagnostics.Debug.WriteLine("GetFreePoint " + string.Join(",", tmp));
                System.Diagnostics.Debug.WriteLine("GetFreePoint x " + x + " y " + y);

                return new Point(x, y);
            }
        }

        private bool IsFreePoint(Point p)
        {
            int tmp = this.points.Where(f => f.X == p.X && f.Y == p.Y).Count();
            //System.Diagnostics.Debug.WriteLine("IsFreePoint " + this.points.Count + " " + +p.X + " " + p.Y);
            //System.Diagnostics.Debug.WriteLine("points " + string.Join(";", this.points));

            return (tmp == 0 ? true : false);
        }
        //private bool IsFreePoint(Point p)
        //{
        //    Point tmp = this.points.Where(f => f.X == p.X && f.Y == p.Y).FirstOrDefault();

        //    return (tmp == Point.Empty ? true : false);
        //}
        private bool IsFreePointAI(Point p, int i)
        {
            for (int t = 0; t < this.pointsAI.Length; t++)
            {
                if (t != i)
                {
                    if (this.pointsAI[t] == p)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int IsInSquare(Point p)
        {
            //var tmp1 = this.points.Where(f => p.X != f.X && p.Y != f.Y).Select(f => f);
            //var tmp3 = this.points.Where(f => p.X != f.X && p.Y != f.Y).Select(f => (Math.Max(-1, Math.Min(1, p.X - f.X)) + 2) + ((Math.Max(-1, Math.Min(1, p.Y - f.Y)) + 2) * 10));
            int c = this.points.Where(f => p.X != f.X && p.Y != f.Y).Select(f => (Math.Max(-1, Math.Min(1, p.X - f.X)) + 2) + ((Math.Max(-1, Math.Min(1, p.Y - f.Y)) + 2) * 10)).GroupBy(f => f).Count();

            //if (c == 4)
            //{ }
            //return (c >= 4 ? true : false);
            return c;
        }

        //se qlcno è vicino alla mela cambio direzione
        private bool NearApple(int s)
        {

            List<Point> heads = this.snakes.Where(f => f != null && f.i != s).Select(f => f.positions[1].Item1).ToList();

            int[] c1 = heads.Select(f => new int[] { Math.Abs(this.Apple.X - f.X), Math.Abs(this.Apple.Y - f.Y) }).Where(f => f[0] <= Program.radius * 2 && f[1] <= Program.radius * 2).FirstOrDefault();
            int[] c2 = new int[] { Math.Abs(this.Apple.X - snakes[s].positions[1].Item1.X), Math.Abs(this.Apple.Y - snakes[s].positions[1].Item1.Y) };

            return (c1 == null || c1[0] + c1[1] + Program.radius > c2[0] + c2[1] ? false : true);
        }

        //è un tunnel se
        // - alla fine della sua direzione trovo un ostacolo
        // - l ingresso permette un solo passaggio
        // - una volta entrati orgonalmente sono presenti posizioni occupate senza interruzione
        private int IsTunnel(Point p, int f_X, int f_Y)
        {
            HashSet<Point> tmp_points = new HashSet<Point>(this.points.Distinct().ToList());

            int[] res = new int[3] { 1, 1, 0 };

            // sommatoria = 2 -> tunnel
            // sommatoria = 3 -> tunnel senza uscita

            for (int i = 0; res[0] + res[1] + res[2] == 2 && i < 10; i++)
            {
                res = new int[3] { 0, 0, 0 };

                int x_offset;
                int y_offset;

                x_offset = + Math.Abs(f_X) * (p.Y + (Program.radius * i * f_X)) + Math.Abs(f_Y) * (p.X + (Program.radius * f_Y));
                y_offset = + Math.Abs(f_Y) * (p.Y + (Program.radius * i * f_Y)) + Math.Abs(f_X) * (p.X + (Program.radius * f_X));

                Point tmp_p;

                tmp_p = new Point(x_offset, y_offset);

                //
                if (tmp_points.Contains(tmp_p))
                {
                    res[0] = 1;
                    //
                    x_offset = +Math.Abs(f_X) * (p.Y + (Program.radius * i)) + Math.Abs(f_Y) * (p.X - (Program.radius * f_Y));
                    y_offset = +Math.Abs(f_Y) * (p.Y + (Program.radius * i)) + Math.Abs(f_X) * (p.X - (Program.radius * f_X));

                    tmp_p = new Point(x_offset, y_offset);

                    if (tmp_points.Contains(tmp_p))
                    {
                        res[1] = 1;
                        //
                        x_offset = +Math.Abs(f_X) * (p.Y + (Program.radius * (i + 1))) + Math.Abs(f_Y) * p.X;
                        y_offset = +Math.Abs(f_Y) * (p.Y + (Program.radius * (i + 1))) + Math.Abs(f_X) * p.X;

                        tmp_p = new Point(x_offset, y_offset);

                        if (tmp_points.Contains(tmp_p))
                        {
                            res[2] = 1;
                        }
                        else
                        {
                            res[2] = 0;
                        }
                    }
                    else
                    {
                        res[1] = 0;
                    }
                }
                else
                {
                    res[0] = 0;
                }
            }


            return res[0] + res[1] + res[2];
        }

        private int IsTunnel_(Point p, int f_X, int f_Y)
        {
            int f_In = 0;

            HashSet<int> f_1 = new HashSet<int>();
            HashSet<int> f_2 = new HashSet<int>();

            int tmp_p1a = Math.Abs((f_X * p.X) + (f_Y * p.Y));
            int tmp_p1b = Math.Abs((f_X * p.Y) + (f_Y * p.X));

            int f_End = tmp_p1a;

            for (int i = 0; i < this.points.Count; i++)
            {
                int tmp_p2a = Math.Abs((f_X * this.points[i].X) + (f_Y * this.points[i].Y));
                int tmp_p2b = Math.Abs((f_X * this.points[i].Y) + (f_Y * this.points[i].X));

                if (tmp_p1a * (f_X + f_Y) <= tmp_p2a * (f_X + f_Y))
                {
                    int tmp_Diff = tmp_p2b - tmp_p1b;

                    if (Math.Abs(tmp_Diff) == Program.radius)
                    {
                        if (tmp_p1a == tmp_p2a)
                        {
                            f_In++;
                        }
                        else
                        {
                            if (f_End * (f_X + f_Y) < tmp_p2a * (f_X + f_Y))
                            {
                                f_End = tmp_p2a;
                            }
                        }

                        if (tmp_Diff > 0)
                        {
                            if (!f_1.Contains(tmp_p2a))
                            {
                                f_1.Add(tmp_p2a);
                            }
                        }
                        else
                        {
                            if (!f_2.Contains(tmp_p2a))
                            {
                                f_2.Add(tmp_p2a);
                            }
                        }
                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine("IsTunnel f_X " + f_X + " f_Y " + f_Y + " f_In " + f_In + " f_End " + f_End);
            //System.Diagnostics.Debug.WriteLine("f_1 " + string.Join(";", f_1));
            //System.Diagnostics.Debug.WriteLine("f_2 " + string.Join(";", f_2));

            int r = 0;

            if (f_In >= 2 && f_1.Count == f_2.Count && f_1.Contains(f_End))
            {
                r++;

                if (f_1.Count == f_2.Count && f_1.Contains(f_End))
                {
                    r++;
                }
            }

            return r;
        }
        private bool IsTunnel__(Point p, int f_X, int f_Y)
        {            
            int f_In = 0;

            HashSet<int> f_1 = new HashSet<int>();
            HashSet<int> f_2 = new HashSet<int>();

            int tmp_p1a = Math.Abs((f_X * p.X) + (f_Y * p.Y));
            int tmp_p1b = Math.Abs((f_X * p.Y) + (f_Y * p.X));

            int f_End = tmp_p1a;

            for (int i = 0; i < this.points.Count; i++)
            {
                int tmp_p2a = Math.Abs((f_X * this.points[i].X) + (f_Y * this.points[i].Y));
                int tmp_p2b = Math.Abs((f_X * this.points[i].Y) + (f_Y * this.points[i].X));
                
                if (tmp_p1a * (f_X + f_Y) <= tmp_p2a * (f_X + f_Y))
                {
                    int tmp_Diff = tmp_p2b - tmp_p1b;

                    if (Math.Abs(tmp_Diff) == Program.radius)
                    {
                        if (tmp_p1a == tmp_p2a)
                        {
                            f_In++;
                        }
                        else
                        {
                            if (f_End * (f_X + f_Y) < tmp_p2a * (f_X + f_Y))
                            {
                                f_End = tmp_p2a;
                            }
                        }

                        if (tmp_Diff > 0)
                        {
                            if (!f_1.Contains(tmp_p2a))
                            {
                                f_1.Add(tmp_p2a);
                            }
                        }
                        else
                        {
                            if (!f_2.Contains(tmp_p2a))
                            {
                                f_2.Add(tmp_p2a);
                            }
                        }
                    }                    
                }                
            }

            //System.Diagnostics.Debug.WriteLine("IsTunnel f_X " + f_X + " f_Y" + f_Y + " f_In " + f_In + " f_End " + f_End);
            //System.Diagnostics.Debug.WriteLine("f_1 " + string.Join(";", f_1));
            //System.Diagnostics.Debug.WriteLine("f_2 " + string.Join(";", f_2));

            if (f_In >= 2 && f_1.Count == f_2.Count && f_1.Contains(f_End))
            {
                return true;
            }

            return false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.TimerSnake.Tick -= KillSnake;
            this.TimerSnake.Tick -= DrawSnake;
            this.TimerSnake.Tick += DrawSnake;
            this.TimerSnake.Interval = this.speed[0].I1;
            Snake();
            changeColorSnakes();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {                
                DialogResult result = fd.ShowDialog();

                if (result == DialogResult.OK && !fd.FileName.Equals(""))
                {
                    try
                    {
                        Bitmap bg = new Bitmap(fd.FileName);

                        int min = Math.Min(pictureBox1.Width, pictureBox1.Height);
                        double x = min / Math.Max(bg.PhysicalDimension.Width, bg.PhysicalDimension.Height);

                        int w = (int)Math.Round(bg.PhysicalDimension.Width * x);
                        int h = (int)Math.Round(bg.PhysicalDimension.Height * x);

                        pictureBox1.BackgroundImage = new Bitmap(new Bitmap(fd.FileName), new Size(w, h));
                    }
                    catch (Exception ex) { }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool[] tmp = new bool[] { this.TimerSnake.Enabled, this.TimerJoystick.Enabled, this.TimerAI.Enabled };

            //int speed = this.Timer.Interval;
            //this.Timer.Interval = 0;
            this.TimerSnake.Enabled = false;
            this.TimerJoystick.Enabled = false;
            this.TimerAI.Enabled = false;

            new Keys().ShowDialog();

            this.TimerSnake.Enabled = tmp[0];
            this.TimerJoystick.Enabled = tmp[1];
            this.TimerAI.Enabled = tmp[2];
        }
        
        //x evitare che vengano modificate le selezioni usando le frecce durante il gioco
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            bool[] tmp = new bool[] { this.TimerSnake.Enabled, this.TimerJoystick.Enabled, this.TimerAI.Enabled };

            this.TimerSnake.Enabled = false;
            this.TimerJoystick.Enabled = false;
            this.TimerAI.Enabled = false;

            new Network().ShowDialog();

            this.TimerSnake.Enabled = tmp[0];
            this.TimerJoystick.Enabled = tmp[1];
            this.TimerAI.Enabled = tmp[2];

            //this.TimerNetwork.Interval = 50;

            if (classes.NetServer.f_Connect == true || classes.NetClient.f_Connect == true)
            {
                if (classes.NetServer.f_Connect == true)
                {
                    //this.TimerNetwork.Tick += SendToClient;
                    this.TimerNetwork.Tick += ReceiveFromClient;
                    this.TimerNetwork.Interval = 10;
                    this.TimerNetwork.Start();
                    //this.TimerNetwork.Start();

                    SendToClientSettings();
                }
                if (classes.NetClient.f_Connect == true)
                {
                    ////new Thread(new ThreadStart(ReceiveFromServer)) { IsBackground = true }.Start();
                    //this.TimerNetwork.Tick += SendToServer;
                    this.TimerNetwork.Tick += ReceiveFromServer;
                    this.TimerNetwork.Interval = 10;
                    this.TimerNetwork.Start();
                    this.TimerSnake.Enabled = false;
                }
            }
            else
            {
                this.TimerNetwork.Stop();
                this.TimerSnake.Enabled = true;
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chb = (CheckBox)sender;

            if (chb.Enabled)
            {
                //se è selezionabile, allora è disponibile 
                if (chb.Checked)
                {
                    chb.Tag = Program.id;
                }
                else
                {
                    chb.Tag = null;
                }

                if (classes.NetServer.f_Connect == true)
                {
                    SendToClientSettings();
                }

                if (classes.NetClient.f_Connect == true)
                {
                    SendToServerSettings(chb);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Al cargar el formulario, actualizar los colores de las serpientes.
            changeColorSnakes();
        }

        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            difficultLevelSelected = difficultLevels[cmbDifficulty.SelectedIndex];
        }

        //Función que permite modificar los colores
        public void changeColorSnakes()
        {
            //Recorrer todas las serpientes del activas.
            for (int i = 0; i < this.snakes.Count; i++)
            {
                //Verificar que no sean nulas
                if (this.snakes[i] != null)
                {
                    //Establecer el código RGB que fue establecido en los picturebox.
                    this.snakes[i].color_RGB = pbSnakesColors[i].BackColor;
                    //LLamar al método de la clase Snake que modifica el color de la serpiente.
                    this.snakes[i].refreshSpritesSnake();
                }

            }
        }
        //Funcion para activar los checkbox de la IA xq se perdieron xd 
        public void ActivarIA(int num, bool accion) {
            CheckBox chb = (CheckBox)pnlSnakeOptions.Controls["checkBox" + num];

            if (accion)
            chb.Checked = true;
            else 
            chb.Checked = false;
        }
        //Cambiar Nombres 
        public void NombresJugadores() { 
        
        }

        //Lista que contiene todos los PictureBox, se asignan sus valores en el inicializador del formulario no en el load.
        public List<PictureBox> pbSnakesColors = new List<PictureBox>();

        
        //Eventos para abrir el picturebox de los colores de las serpientes (sirve para todos).
        private void pbColorSnake_Click(object sender, EventArgs e)
        {
            //frmColorPicker colorPicker = new frmColorPicker(this, int.Parse(((PictureBox)(sender)).AccessibleName) - 1);
            //colorPicker.ShowDialog();
        }

        private void pbColorSnakeTwo_Click(object sender, EventArgs e)
        {
            frmColorPicker colorPicker = new frmColorPicker(this, int.Parse(((PictureBox)(sender)).AccessibleName), 2, textBox2.Text);
            colorPicker.ShowDialog();
        }

        private void pbColorSnakeOne_Click(object sender, EventArgs e)
        {
            frmColorPicker colorPicker = new frmColorPicker(this, int.Parse(((PictureBox)(sender)).AccessibleName) - 1, 1, textBox1.Text);
            colorPicker.ShowDialog();
        }
        private void pbColorSnakeThree_Click(object sender, EventArgs e)
        {
            frmColorPicker colorPicker = new frmColorPicker(this, int.Parse(((PictureBox)(sender)).AccessibleName)+1, 3, textBox3.Text);
            colorPicker.ShowDialog();
        }
        private void pbColorSnakeFour_Click(object sender, EventArgs e)
        {
            frmColorPicker colorPicker = new frmColorPicker(this, int.Parse(((PictureBox)(sender)).AccessibleName)+2, 4, textBox4.Text);
            colorPicker.ShowDialog();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            { checkBox6.Checked = true; posicion2.Visible = true; puntos1.Visible = true; }
            else
            { checkBox6.Checked = false; posicion2.Visible = false; puntos1.Visible = false; }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox11.Checked == true)
            { checkBox7.Checked = true; posicion3.Visible = true; puntos2.Visible = true; }
            else
            { checkBox7.Checked = false; posicion3.Visible = false; puntos2.Visible = false ; }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox12.Checked == true)
            { checkBox8.Checked = true; posicion4.Visible = true; puntos3.Visible = true; }
            else
            { checkBox8.Checked = false; posicion4.Visible = false ; puntos3.Visible = false; }
        }
        public void Colores_nombres(string nombre, int id, int A, int B, int C, bool ac)
        {
            if (id == 1)
            {
                textBox1.Text = nombre;
                posicion1.Text = nombre;
                if (ac)
                {
                    textBox1.Text = nombre;
                    textBox1.BackColor = Color.FromArgb(A, B, C);
                    panel1.BaseColor = Color.FromArgb(A, B, C);
                    config_1.BackColor = Color.FromArgb(A, B, C);
                }
            }
            else if (id == 2)
            {
                textBox2.Text = nombre;
                posicion2.Text = nombre;
                if (ac)
                {
                    textBox2.BackColor = Color.FromArgb(A, B, C);
                    checkBox10.BackColor = Color.FromArgb(A, B, C);
                    panel2.BaseColor = Color.FromArgb(A, B, C);
                    pictureBox2.BackColor = Color.FromArgb(A, B, C);
                }
            }
            else if (id == 3)
            {
                textBox3.Text = nombre;
                posicion3.Text = nombre;
                if (ac)
                {
                    textBox3.BackColor = Color.FromArgb(A, B, C);
                    checkBox11.BackColor = Color.FromArgb(A, B, C);
                    panel3.BaseColor = Color.FromArgb(A, B, C);
                    pictureBox3.BackColor = Color.FromArgb(A, B, C);
                }
               
            }
            else
            {
                textBox4.Text = nombre;
                posicion4.Text = nombre;
                if(ac)
                {
                    textBox4.BackColor = Color.FromArgb(A, B, C);
                    checkBox12.BackColor = Color.FromArgb(A, B, C);
                    panel4.BaseColor = Color.FromArgb(A, B, C);
                    pictureBox4.BackColor = Color.FromArgb(A, B, C);
                }       
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            xd xd1 = new xd();
            xd1.ShowDialog();
        }
    }

}
