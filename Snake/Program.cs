using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    static class Program
    {
        public static readonly string id = Guid.NewGuid().ToString();

        public const int radius= 20;

        public static Dictionary<int, tuple.t2<int, int>> keys;
        public static List<Joystick> Joystick;
        public static List<Socket> NetPlayers;
        public static Socket Server;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Keys.ReadKeys();
            Application.Run(new Form1());
        }
    }
}
