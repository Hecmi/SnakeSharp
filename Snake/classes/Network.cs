using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Snake.classes
{
    public enum NetWorkType
    {
        Settings = 1,
        Draw = 2,
        Kill = 3
    }

    class NetWorkLite
    {
        //https://makolyte.com/csharp-deserialize-json-with-a-specific-constructor/
        //definisco a json ql è il costruttore da usare al momento della deserializzazione
        [JsonConstructor]
        protected NetWorkLite(NetWorkType NetWorkType)
        {
            this.NetWorkType = NetWorkType;
        }

        public NetWorkType NetWorkType;
    }

    class NetWorkSettings : NetWorkLite
    {
        public NetWorkSettings(NetWorkType SendType) : base(SendType)
        {

        }

        //public bool[] checkboxes;
        public Tuple<string, bool, string>[] checkboxes;
    }

    class NetWorkDraw : NetWorkLite
    {
        public NetWorkDraw(NetWorkType SendType) : base(SendType)
        {

        }

        public int pictureBox1Height;
        public int pictureBox1Width;

        public List<SnakeLite> Snakes;
        public Point Apple;
        public Dictionary<Point, int> Hole;
        public Dictionary<int, Point> HoleTras;
    }

    class NetWorkKill : NetWorkLite
    {
        public NetWorkKill(NetWorkType SendType) : base(SendType)
        {

        }
        
        public Point p;
    }

    class NetWork
    {
        //MISTERO.!!!!.
        //non sò xkè, ma se la stringa inviata è inferiore a 1500 byte, la lettura lato client è + lenta e causa un ritardo.
        //x il momento, aggiunto tanti caratteri x superare la dimensione e evitare il problema
        //private static string End = "#!#";
        private static string TrashString = "*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+";
        private static string TrashStringEnd = "^!!^";

        public static void Send(Socket socket, string s)
        {
            //aggiungo un carattere x identificare la fine del messaggio x evitare che in caso di accodamento vengano mischiati
            s = s + TrashString + TrashStringEnd;

            //System.Diagnostics.Debug.WriteLine("Send:" + s);
            
            byte[] msg = Encoding.UTF8.GetBytes(s);
            //socket.SendBufferSize = msg.Length;
            int bytesSent = socket.Send(msg);

            //Console.WriteLine("bytesSent: " + bytesSent + " socket: " + socket.SendBufferSize);
        }

        public static string[] Receive_(Socket socket)
        {
            StringBuilder s = new StringBuilder();
            byte[] bytes = new byte[socket.ReceiveBufferSize];

            //potrebbe essere che si accumulino messaggi, x cui continuo finchè non ne ho +
            while (true)
            {
                if (socket.Poll(1000, SelectMode.SelectRead))
                //while(!Program.Server.Poll(10000, SelectMode.SelectRead))
                {
                    //byte[] bytes = new byte[1024 * 2];
                    //socket.SendBufferSize = bytes.Length;
                    int bytesRec = socket.Receive(bytes);
                    s.Append(Encoding.UTF8.GetString(bytes, 0, bytesRec));
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("bytesRec: " + bytes.Length + " c: " + (s.ToString().Split(new string[] { "#!#" }, StringSplitOptions.None).Length - 1));

            //string h = s.ToString().Split(new string[] { "#!#" }, StringSplitOptions.None)[0];
            return s.ToString().Split(new string[] { TrashString }, StringSplitOptions.None);
        }

        public static string[] Receive(Socket socket)
        {
            StringBuilder s = new StringBuilder();
            byte[] bytes = new byte[socket.ReceiveBufferSize];
            
            //potrebbe essere che si accumulino messaggi, x cui continuo finchè non ne ho +
            do
            {
                if (socket.Poll(10, SelectMode.SelectRead))
                //while(!Program.Server.Poll(10000, SelectMode.SelectRead))
                {
                    int bytesRec = socket.Receive(bytes);
                    s.Append(Encoding.UTF8.GetString(bytes, 0, bytesRec));
                    
                    /*
                    try
                    {
                        //byte[] bytes = new byte[1024 * 2];
                        //socket.SendBufferSize = bytes.Length;
                        int bytesRec = socket.Receive(bytes);
                        s.Append(Encoding.UTF8.GetString(bytes, 0, bytesRec));
                        //Console.WriteLine("tmp: '" + tmp + "'");
                    }
                    catch (Exception e)
                    {
                        DialogResult r = MessageBox.Show("Network problems....", "Error.!!.", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        if (r.Equals(DialogResult.Retry))
                        {
                            continue;
                        }

                        NetClient.f_Connect = false;
                        return null;
                    }
                    */
                }
                else
                {
                    //Console.WriteLine("break");
                    //break;
                }
            }
            while (s.ToString() != "" && s.ToString().Contains(TrashStringEnd) == false);

            //Console.WriteLine("bytesRec: " + bytes.Length + " c: " + (s.ToString().Split(new string[] { "#!#" }, StringSplitOptions.None).Length - 1));

            //string h = s.ToString().Split(new string[] { "#!#" }, StringSplitOptions.None)[0];
            return s.ToString().Split(new string[] { TrashString + TrashStringEnd }, StringSplitOptions.None);
        }

    }

    class NetClient
    {
        public static bool f_Connect;// = false;

        private static string Server;
        private static int Port;

        public NetClient(string server, int port)
        {
            Server = server;
            Port = port;
            //f_Connect = false;
        }

        public static void StartClient()
        {
            try
            {
                
                //Console.WriteLine("Client: Connecting to server");
                //TcpClient client = new TcpClient();
                //client.Connect("7.197.147.38", 12345);
                //Console.WriteLine("Client: Connected to server");
                //Console.Read();

                //TcpClient client = new TcpClient(Server, Port);

                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                //IPHostEntry host = Dns.GetHostEntry(Server);
                //IPAddress ipAddress = host.AddressList[0];
                IPAddress ipAddress = System.Net.IPAddress.Parse(Server);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

                f_Connect = false;
                // Create a TCP/IP  socket.
                Program.Server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                while (true)
                {
                    try
                    {
                        // Connect to Remote EndPoint
                        Program.Server.Connect(Server, Port);//remoteEP);
                        f_Connect = true;

                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        Thread.Sleep(1000);
                    }
                }

                System.Diagnostics.Debug.WriteLine("Socket connected to " + Program.Server.RemoteEndPoint.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    class NetServer
    {
        public static bool f_Connect { get { return Program.NetPlayers != null && Program.NetPlayers.Count > 0; } }

        public static IPAddress ipAddress;
        //private static bool f_Running = false;
        private static Socket listener;// = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //public static IPHostEntry Host;
        private static int Port;
        //public static IPEndPoint LocalEndPoint;

        public NetServer(int port)
        {
            Port = port;

            //Program.NetPlayers = null;
        }

        //prima resto in attesa di partecipanti
        public static void StartServer()
        {
            //IPHostEntry host = Dns.GetHostEntry("localhost");
            //Host = Dns.GetHostEntry("127.0.0.1");
            //ipAddress = Host.AddressList.Where(f => f.AddressFamily.Equals(AddressFamily.InterNetwork) && f.ToString().Equals("7.197.147.38")).FirstOrDefault();
            //IPAddress ipAddress = System.Net.IPAddress.Parse("7.197.147.38");
            //string ip = Host.AddressList[5].ToString();
            IPEndPoint LocalEndPoint = new IPEndPoint(ipAddress, Port);

            Program.NetPlayers = new List<Socket>();

            try
            {
                if (listener != null)
                {
                    listener.Dispose();
                }
                // Create a Socket that will use Tcp protocol
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(LocalEndPoint);

                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);

                //var t1 = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
                //var t2 = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();

                //controllo e aggiungo le connessioni
                while (true)//Program.NetRunning)
                {
                    System.Diagnostics.Debug.WriteLine("Waiting for a connection..." + DateTime.Now.ToString("yyyyMMhh mm:ss:"));
                    Program.NetPlayers.Add(listener.Accept());

                    Thread.Sleep(1000);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
