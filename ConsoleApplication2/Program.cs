using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// Client app is the one sending messages to a Server/listener.
// Both listener and client can send messages back and forth once a
// communication is established.
public class SocketClient
{
    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }

    public static void StartClient()
    {
        /*
        Console.WriteLine("Client: Connecting to server");
        TcpClient client = new TcpClient();
        client.Connect("7.197.147.38", 12345);
        Console.WriteLine("Client: Connected to server");
        //Console.Read();

        Console.WriteLine("Client: Receiving data");

        string r = "";

        NetworkStream clientStream = client.GetStream();
        clientStream.ReadTimeout = 1;

        while (true)
        {
            r = "";
            //using (NetworkStream clientStream = client.GetStream())
            {
                try
                {
                    while (true)
                    {
                        byte[] buffer = new byte[50];
                        clientStream.Read(buffer, 0, buffer.Length);
                        if (buffer[0] == 0)
                        {                            
                            break;
                        }
                        r += Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(r);
                }
                //while (tmp != "");
            }
            //Console.WriteLine("Client: Received data: " + buffer.Aggregate("", (s, b) => s += " " + b.ToString()));
            if (r != "")
            {
                Console.WriteLine(r.Trim());
            }
        }

        client.Close();
        */
        try
        {
            // Connect to a Remote server
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            //IPHostEntry host = Dns.GetHostEntry("7.197.147.38");
            //IPAddress ipAddress = host.AddressList[0];
            //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                while (true)
                {
                    try
                    {
                        // Connect to Remote EndPoint
                        sender.Connect("7.167.103.244", 12345);
                        break;
                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(1000);
                    }
                }

                Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                while (true)
                {
                    string s = Console.ReadLine();

                    byte[] msg = Encoding.ASCII.GetBytes(s);

                    int bytesSent = sender.Send(msg);
                    /*
                    string m = "";

                    while (true)
                    {
                        byte[] bytes = new byte[1024];
                        int bytesRec = sender.Receive(bytes);
                        string tmp = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (tmp == "")
                        {
                            break;
                        }
                    }
                    Console.WriteLine("return " + m);
                    */
                    ///*
                    //if (sender.Poll(10000, SelectMode.SelectRead))
                    //while(!sender.Poll(10000, SelectMode.SelectRead))
                    {
                        byte[] bytes = new byte[1024 * 8];
                        int bytesRec = sender.Receive(bytes);
                        Console.WriteLine("return {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                        //break;
                    }
                    //*/
                    Thread.Sleep(100);
                }
                /*
                Guid g = Guid.NewGuid();
                while (true)
                {
                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("Client " + g + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    Thread.Sleep(3000);
                }
                */
                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}