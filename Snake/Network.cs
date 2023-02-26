using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public partial class Network : Form
    {
        System.Windows.Forms.Timer TimerClients = new System.Windows.Forms.Timer();
        private Thread Mode;

        public Network()
        {
            InitializeComponent();

            textBox1.Visible = false;
            comboBox2.Visible = false;

            label0.Text = "Be a...";
            label1.Text = "Server Ip";
            label2.Text = "Port";

            textBox1.Text = "127.0.0.1";
            textBox2.Text = "11000";
            //textBox3.ReadOnly = true;

            comboBox1.Items.Add("Server");
            comboBox1.Items.Add("Client");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            button1.Text = "Connect";

            reset();

            TimerClients.Interval = 1000;
            TimerClients.Tick += Clients;
            TimerClients.Start();
        }

        private void reset()
        {
            IPHostEntry Host = Dns.GetHostEntry("127.0.0.1");

            foreach (var ip in Host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    comboBox2.Items.Add(new classes.ComboBoxItem<IPAddress>(ip.ToString(), ip));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                int port;

                if (int.TryParse(textBox2.Text, out port))
                {
                    switch ((comboBox1.Enabled ? 0 : 10) + comboBox1.SelectedIndex)
                    {
                        case 0:

                            if (comboBox2.SelectedIndex > 0)
                            {
                                classes.NetServer.ipAddress = ((classes.ComboBoxItem<IPAddress>)comboBox2.SelectedItem).Value;

                                new classes.NetServer(port);
                                Mode = new Thread(classes.NetServer.StartServer) { IsBackground = true };//.Start();
                                Mode.Start();
                            }
                            break;
                        case 1:

                            new classes.NetClient(textBox1.Text, port);
                            Mode = new Thread(classes.NetClient.StartClient) { IsBackground = true };//.Start();
                            Mode.Start();

                            break;

                        case 10:
                            CloseThread();
                            break;
                        case 11:

                            CloseThread();
                            break;
                    }
                }

                comboBox1.Enabled = !comboBox1.Enabled;
                comboBox2.Enabled = !comboBox2.Enabled;
                //textBox1.Enabled = !textBox1.Enabled;
                //textBox2.Enabled = !textBox2.Enabled;
                textBox1.ReadOnly = !textBox1.ReadOnly;
                textBox2.ReadOnly = !textBox2.ReadOnly;
            }
        }

        private void CloseThread()
        {            
            if (Mode != null)
            {
                Mode.Abort();
            }
        }

        private void Clients(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        
                        listBox1.Items.Clear();

                        if (Program.NetPlayers != null)
                        {
                            foreach (Socket s in Program.NetPlayers)
                            {
                                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                                foreach (var ip in host.AddressList)
                                {
                                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                                    {
                                        //textBox3.Text += ip.ToString() + ";\n";
                                        listBox1.Items.Add(ip.ToString());
                                        break;
                                    }
                                }
                            }
                        }

                        break;

                    case 1:

                        listBox1.Items.Clear();
                        textBox1.BackColor = (classes.NetClient.f_Connect == true ? System.Drawing.Color.LightGreen : System.Drawing.Color.LightSalmon);

                        break;
                }
            }
        }

        private void Network_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseThread();
        }

        private void Network_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    textBox1.Visible = false;
                    comboBox2.Visible = true;
                    break;
                case 1:
                    textBox1.Visible = true;
                    comboBox2.Visible = false;
                    break;
            }
        }
    }
}
