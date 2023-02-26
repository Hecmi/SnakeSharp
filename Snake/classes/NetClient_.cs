using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Snake.classes
{
    class NetClient_
    {
        public static bool f_Connect = false;

        private static string Server;
        private static int Port;

        public NetClient_(string server, int port)
        {
            Server = server;
            Port = port;
            f_Connect = false;
        }

        public static void StartClient()
        {
            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry(Server);
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

                // Create a TCP/IP  socket.
                Program.Server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                while (true)
                {
                    try
                    {
                        // Connect to Remote EndPoint
                        Program.Server.Connect(remoteEP);
                        f_Connect = true;

                        break;
                    }
                    catch (Exception e)
                    {
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

        public static void SendToServer(string s)
        {
            byte[] msg = Encoding.ASCII.GetBytes(s);
            int bytesSent = Program.Server.Send(msg);
        }
        public static string ReceiveFromServer()
        {
            while (true)
            {
                if (Program.Server.Poll(10000, SelectMode.SelectRead))
                //while(!Program.Server.Poll(10000, SelectMode.SelectRead))
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = Program.Server.Receive(bytes);
                    string s = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    return s;
                }

                Thread.Sleep(10);
            }

            return "";
        }

        //poi gestisco la risposta dall host
        public static void GetAnswers()
        {
            while (true)
            {
                string s = Console.ReadLine();

                byte[] msg = Encoding.ASCII.GetBytes(s);

                int bytesSent = Program.Server.Send(msg);

                //if (sender.Poll(10000, SelectMode.SelectRead))
                //while(!sender.Poll(10000, SelectMode.SelectRead))
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = Program.Server.Receive(bytes);

                    Console.WriteLine("return {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    //break;
                }
                Thread.Sleep(100);
            }
        }

        //static bool f_Running = false;
        /*
        public static void StartClient()
        {
            if (f_Running == false)
            {
                f_Running = true;

                try
                {
                    // Connect to a Remote server
                    // Get Host IP Address that is used to establish a connection
                    // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                    // If a host has multiple addresses, you will get a list of addresses
                    IPHostEntry host = Dns.GetHostEntry("localhost");
                    IPAddress ipAddress = host.AddressList[0];
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    // Create a TCP/IP  socket.
                    Program.Server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    while (true)
                    {
                        try
                        {
                            // Connect to Remote EndPoint
                            Program.Server.Connect(remoteEP);
                            break;
                        }
                        catch (Exception e)
                        {
                            Thread.Sleep(1000);
                        }
                    }

                    System.Diagnostics.Debug.WriteLine("Socket connected to " + Program.Server.RemoteEndPoint.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                f_Running = false;
            }
        }
        */

        public void StartClient_()
        {
            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                while (true)
                {
                    try
                    {
                        // Connect to Remote EndPoint
                        sender.Connect(remoteEP);
                        break;
                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(1000);
                    }
                }

                System.Diagnostics.Debug.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                while (true)
                {
                    string s = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");// Console.ReadLine();

                    byte[] msg = Encoding.ASCII.GetBytes(s);

                    int bytesSent = sender.Send(msg);

                    if (sender.Poll(10000, SelectMode.SelectRead))
                    {
                        byte[] bytes = new byte[1024];
                        int bytesRec = sender.Receive(bytes);
                        string h = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        System.Diagnostics.Debug.WriteLine("return " + h);
                    }
                    Thread.Sleep(100);
                }
                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}