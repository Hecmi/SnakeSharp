using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class Keys_ : Form
    {
        const int JoyOffset= 10000000;
        const string file = "PlayersKeys.txt";

        private classes.ComboBoxItem<int>[] KeysList;

        System.Windows.Forms.Timer TimerJoystick = new System.Windows.Forms.Timer();
        /*
        public static Dictionary<int, classes.ComboBoxItem<int>> Joystick = new Dictionary<int, classes.ComboBoxItem<int>>()
        {

        };
        */

        public static List<classes.ComboBoxItem<int>> Joystick = new List<classes.ComboBoxItem<int>>()
        {
            new classes.ComboBoxItem<int>("J1 U", (1 * JoyOffset) + 203),
            new classes.ComboBoxItem<int>("J1 L", (1 * JoyOffset) + 200),
            new classes.ComboBoxItem<int>("J1 R", (1 * JoyOffset) + 201),
            new classes.ComboBoxItem<int>("J1 D", (1 * JoyOffset) + 202),
            new classes.ComboBoxItem<int>("J1 1", (1 * JoyOffset) + 000),
            new classes.ComboBoxItem<int>("J1 2", (1 * JoyOffset) + 001),
            new classes.ComboBoxItem<int>("J1 3", (1 * JoyOffset) + 002),
            new classes.ComboBoxItem<int>("J1 4", (1 * JoyOffset) + 003),
            new classes.ComboBoxItem<int>("J1 5", (1 * JoyOffset) + 004),
            new classes.ComboBoxItem<int>("J1 6", (1 * JoyOffset) + 005),
            new classes.ComboBoxItem<int>("J1 7", (1 * JoyOffset) + 006),
            new classes.ComboBoxItem<int>("J1 8", (1 * JoyOffset) + 007),

            new classes.ComboBoxItem<int>("J2 U", (2 * JoyOffset) + 203),
            new classes.ComboBoxItem<int>("J2 L", (2 * JoyOffset) + 200),
            new classes.ComboBoxItem<int>("J2 R", (2 * JoyOffset) + 201),
            new classes.ComboBoxItem<int>("J2 D", (2 * JoyOffset) + 202),
            new classes.ComboBoxItem<int>("J2 1", (2 * JoyOffset) + 000),
            new classes.ComboBoxItem<int>("J2 2", (2 * JoyOffset) + 001),
            new classes.ComboBoxItem<int>("J2 3", (2 * JoyOffset) + 002),
            new classes.ComboBoxItem<int>("J2 4", (2 * JoyOffset) + 003),
            new classes.ComboBoxItem<int>("J2 5", (2 * JoyOffset) + 004),
            new classes.ComboBoxItem<int>("J2 6", (2 * JoyOffset) + 005),
            new classes.ComboBoxItem<int>("J2 7", (2 * JoyOffset) + 006),
            new classes.ComboBoxItem<int>("J2 8", (2 * JoyOffset) + 007),

            new classes.ComboBoxItem<int>("J3 U", (3 * JoyOffset) + 203),
            new classes.ComboBoxItem<int>("J3 L", (3 * JoyOffset) + 200),
            new classes.ComboBoxItem<int>("J3 R", (3 * JoyOffset) + 201),
            new classes.ComboBoxItem<int>("J3 D", (3 * JoyOffset) + 202),
            new classes.ComboBoxItem<int>("J3 1", (3 * JoyOffset) + 000),
            new classes.ComboBoxItem<int>("J3 2", (3 * JoyOffset) + 001),
            new classes.ComboBoxItem<int>("J3 3", (3 * JoyOffset) + 002),
            new classes.ComboBoxItem<int>("J3 4", (3 * JoyOffset) + 003),
            new classes.ComboBoxItem<int>("J3 5", (3 * JoyOffset) + 004),
            new classes.ComboBoxItem<int>("J3 6", (3 * JoyOffset) + 005),
            new classes.ComboBoxItem<int>("J3 7", (3 * JoyOffset) + 006),
            new classes.ComboBoxItem<int>("J3 8", (3 * JoyOffset) + 007),

            new classes.ComboBoxItem<int>("J4 U", (4 * JoyOffset) + 203),
            new classes.ComboBoxItem<int>("J4 L", (4 * JoyOffset) + 200),
            new classes.ComboBoxItem<int>("J4 R", (4 * JoyOffset) + 201),
            new classes.ComboBoxItem<int>("J4 D", (4 * JoyOffset) + 202),
            new classes.ComboBoxItem<int>("J4 1", (4 * JoyOffset) + 000),
            new classes.ComboBoxItem<int>("J4 2", (4 * JoyOffset) + 001),
            new classes.ComboBoxItem<int>("J4 3", (4 * JoyOffset) + 002),
            new classes.ComboBoxItem<int>("J4 4", (4 * JoyOffset) + 003),
            new classes.ComboBoxItem<int>("J4 5", (4 * JoyOffset) + 004),
            new classes.ComboBoxItem<int>("J4 6", (4 * JoyOffset) + 005),
            new classes.ComboBoxItem<int>("J4 7", (4 * JoyOffset) + 006),
            new classes.ComboBoxItem<int>("J4 8", (4 * JoyOffset) + 007),
        };
        

        public Keys_()
        {
            InitializeComponent();

            label9.Select();

            label1.Text = "Up";
            label2.Text = "Left";
            label3.Text = "Right";
            label4.Text = "Down";

            label5.Text = "Player 1";
            label6.Text = "Player 2";
            label7.Text = "Player 3";
            label8.Text = "Player 4";
            
            button17.Text = "Save";
            
            var keys = Enum.GetValues(typeof(System.Windows.Forms.Keys));

            //int[] keysascii = new int[] { 37, 38, 39, 40, 48, 49, 50, 51, 52, 53, 54, 56, 57, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105 };

            this.KeysList = new classes.ComboBoxItem<int>[keys.Length + 1 + Joystick.Count];
            //this.KeysList = new classes.ComboBoxItem<int>[keysascii.Length + 1 + Joystick.Count];
            this.KeysList[0] = new classes.ComboBoxItem<int>("", 0);

            int i = 1;
            
            foreach (System.Windows.Forms.Keys k in keys)
            {
                //Console.WriteLine((int)k + " - " + k.ToString());

                //if (keysascii.Contains((int)k))
                {
                    //Console.WriteLine((int)k);
                    //int n = (int)k;
                    //Console.WriteLine(n);
                    this.KeysList[i] = new classes.ComboBoxItem<int>(k.ToString(), (int)k);
                    i++;
                }
            }

            foreach (classes.ComboBoxItem<int> c in Joystick)
            {
                this.KeysList[i] = c;
                i++;
            }
            //this.KeysList

            //comboBox1.Items.AddRange(this.KeysList);
            //comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //return;

            for (i = 1; i <= 16; i++)
            {
                ComboBox cmb = (ComboBox)this.Controls["comboBox" + i];
                cmb.Items.AddRange(this.KeysList);
                //foreach (classes.ComboBoxItem<int> c in this.KeysList)
                //{
                //    cmb.Items.Add(c);
                //}
                //cmb.DataSource = this.KeysList;
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            //return;

            Read();

            this.TimerJoystick.Interval = 50;
            this.TimerJoystick.Tick += JoystickTrigger;
            //TimerJoystick.Start();
        }

        private void SelectKey(ComboBox cmb, int key)
        {            
            //classes.ComboBoxItem<int> v = cmb.Items.Cast<classes.ComboBoxItem<int>>().Where(f => f.Value == key).FirstOrDefault();
            classes.ComboBoxItem<int> v = this.KeysList.Where(f => f.Value == key).FirstOrDefault();

            if (v != null && v.Value != 0)
            {
                this.Tag = null;
                this.TimerJoystick.Stop();

                for (int i = 1; i <= 16; i++)
                {
                    ComboBox tmp = (ComboBox)this.Controls["comboBox" + i];

                    if (cmb.Name != tmp.Name)
                    {
                        if (tmp.SelectedItem == v)
                        {
                            MessageBox.Show("Key '" + v + "' is already used");
                            v = (classes.ComboBoxItem<int>)cmb.Tag;
                            //break;
                        }
                    }
                }

                cmb.SelectedItem = v;// comboBox1.Items.Cast<classes.ComboBoxItem<int>>().Where(f => f.Value == (int)keyData);
                cmb.Tag = v;

                label9.Text = "";
                //sposto il focus su un elemento non selezionabile x impedire un nuovo evento legato al bottone
                label9.Select();
            }
        }

        public static int GetJoykey(int j)
        {
            int offset = (j + 1) * JoyOffset;

            JoystickState state = Program.Joystick[j].GetCurrentState();

            int button;
            //for (button = 0; button < state.Buttons.Length && state.Buttons[button] == false; button++)
            for (button = 0; button < state.Buttons.Length; button++)
            {
                if (state.Buttons[button] == true)
                {
                    //MoveSnake(button + offset);
                    return (button + offset);
                }
            }

            button = -1;
            //Console.WriteLine(state.X + (state.Y * 10));
            //if (state.X != 32767)
            //{ }

            switch (state.X + (state.Y * 10))
            {
                //-X
                case 327670:
                    {
                        button = 200 + offset;
                        break;
                    }
                //+X
                case 393205:
                    {
                        button = 201 + offset;
                        break;
                    }
                //-Y
                case 32767:
                    {
                        button = 202 + offset;
                        break;
                    }
                //+Y
                case 688117:
                    {
                        button = 203 + offset;
                        break;
                    }
            }
            //button += offset;
            //MoveSnake(button);
            return button;
        }    

        public static void ReadKeys()//out Dictionary<int, tuple.t2<int, int>> keys)
        {
            using (StreamReader readtext = new StreamReader(file))
            {
                Program.keys = new Dictionary<int, tuple.t2<int, int>>();

                for (int i = 0; i < 16 && readtext.EndOfStream == false; i++)
                {
                    int val = int.Parse(readtext.ReadLine());

                    int s = i / 4;
                    int p = i % 4;

                    if (val != 0)
                    {
                        Program.keys.Add(val, new tuple.t2<int, int>(s, p));
                    }
                    
                }
            }
        }

        private void Read()
        {
            using (StreamReader readtext = new StreamReader(file))
            {
                for (int i = 1; i <= 16 && readtext.EndOfStream == false; i++)
                {
                    ComboBox cmb = (ComboBox)this.Controls["comboBox" + i];
                    string val = readtext.ReadLine();
                    SelectKey(cmb, (val == "" ? 0 : int.Parse(val)));
                }
            }
        }
        
        private void Save()
        {
            using (StreamWriter writetext = new StreamWriter(file))
            {
                for (int i = 1; i <= 16; i++)
                {
                    ComboBox cmb = (ComboBox)this.Controls["comboBox" + i];
                    classes.ComboBoxItem<int> values = (classes.ComboBoxItem<int>)cmb.SelectedItem;
                    writetext.WriteLine((values == null ? 0 : values.Value));
                }

                writetext.Close();
            }
        }
        
        public void JoystickTrigger(object sender, EventArgs e)
        {
            //return;
            for (int i = 0; i < Program.Joystick.Count; i++)
            {
                int key = GetJoykey(i);

                SetKey(this.Tag, key);

                //this.Tag = null;
                //this.TimerJoystick.Stop();
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            label9.Text = "Press the button!";

            this.Tag = ((Control)sender);
            this.TimerJoystick.Start();
        }

        private void button_KeyDown(object sender, KeyEventArgs e)
        {
            //ComboBox cmb = (ComboBox)this.Controls[((Button)sender).Tag.ToString()];
            SetKey(sender, (int)e.KeyCode);
        }

        private void SetKey(object sender, int key)
        {
            ComboBox cmb = (ComboBox)this.Controls[((Button)sender).Tag.ToString()];
            SelectKey(cmb, key);

            //textBox1.ReadOnly = true;
            //textBox1.Text = ((char)key).ToString();
            //textBox1.Tag = key;
            //label1.Select();
        }

        //https://stackoverflow.com/a/36305348/3061212
        //x fare in modo che anche le frecce vengano intercettate dall evento button_KeyDown
        protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == System.Windows.Forms.Keys.Up || keyData == System.Windows.Forms.Keys.Left || keyData == System.Windows.Forms.Keys.Right || keyData == System.Windows.Forms.Keys.Down)
            {
                return false;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Save();
            ReadKeys();
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ChangeKey((ComboBox)sender);
        }

        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            ChangeKey((ComboBox)sender);
        }

        private void ChangeKey(ComboBox cmb)
        {
            ComboBox tmp = cmb;

            if (tmp.SelectedItem != null)
            {
                SelectKey(tmp, ((classes.ComboBoxItem<int>)(tmp.SelectedItem)).Value);
            }
        }
    }
}
