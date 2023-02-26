using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

// Socket Listener acts as a server and listens to the incoming
// messages on the specified port and protocol.
public class SocketListener
{
    public static int Main(String[] args)
    {
        StartServer();
        return 0;
    }

    public static void StartServer()
    {
        /*
        TcpListener TcpListener = new TcpListener(IPAddress.Any, 12345);
        TcpListener.Start();
        Console.WriteLine("Server: Listener started");

        Console.WriteLine("Server: Waiting for client to connect");
        List<TcpClient> client = new List<TcpClient>();
        //TcpListener.

        string r = "";
        while (r == "")
        {
            client.Add(TcpListener.AcceptTcpClient());
            Console.WriteLine("Server: Client connected");
            r = Console.ReadLine();
        }

        TcpListener.Stop();
        Console.WriteLine("Server: Listener stopped");

        Console.WriteLine("Server: Sending data");
        //string TrashString = " _-|!'0123456789abcdefghilmopqrstuvz123456789abcdefghilmopqrstuvz123456789abcdefghilmopqrstuvz123456789abcdefghilmopqrstuvz";

        List<NetworkStream> clientStream = new List<NetworkStream>();

        foreach (TcpClient c in client)
        {
            clientStream.Add(c.GetStream());
        }

        while (true)
        {
            for (int i = 0; i < client.Count; i++)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(i + " - " + DateTime.Now.ToString("yyyyMMdd HHmmss"));//new byte[] { 1, 2, 3, 4, 5 };
                //using (NetworkStream clientStream = client[i].GetStream())
                {
                    clientStream[i].Write(buffer, 0, buffer.Length);
                }
                Console.WriteLine("Server: Sent data");
            }
            Thread.Sleep(10000);
        }

        Console.Read();
        */
        // Get Host IP Address that is used to establish a connection
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1
        // If a host has multiple addresses, you will get a list of addresses
        IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
        IPAddress ipAddress = host.AddressList[5];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 12345);

        try
        {

            // Create a Socket that will use Tcp protocol
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // A Socket must be associated with an endpoint using the Bind method
            listener.Bind(localEndPoint);
            // Specify how many requests a Socket can listen before it gives Server busy response.
            // We will listen 10 requests at a time
            listener.Listen(1000000);

            Console.WriteLine("Waiting for a connection...");
            Socket handler = listener.Accept();

            Console.WriteLine("connected...");
            Console.Read();

            while (true)
            {

                // Incoming data from the client.
                string data = null;
                byte[] bytes = null;

                //while (true)
                {
                    bytes = new byte[1024];
                    //https://stackoverflow.com/a/32412875/3061212
                    data = Encoding.ASCII.GetString(bytes, 0, handler.Receive(bytes));
                    //bytes = new byte[1024];
                    //int bytesRec = handler.Receive(bytes);
                    //data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //if (data.IndexOf("<EOF>") > -1)
                    //{
                    //    break;
                    //}
                }

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);

                Thread.Sleep(100);
                //continue;

            }

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\n Press any key to continue...");
        Console.ReadKey();
    }
}
