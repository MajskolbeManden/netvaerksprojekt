using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace netværksprojekt
{
    class UDP
    {
        
        internal const int PortNr = 13000;
        public static IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, PortNr);
        private static UdpClient listener = new UdpClient();
        private static UdpClient sender = new UdpClient();


        private static void StartClient()
        {
            var time = DateTime.UtcNow;
            string theIP = "127.0.0.1";
            string msg;
            string navn;
            string ipNew;
            bool newIP = false;
            IPAddress idb = IPAddress.Broadcast;
            Socket socket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram,
                            ProtocolType.Udp);
            IPAddress ip = IPAddress.Parse(theIP);
            IPEndPoint ep = new IPEndPoint(ip, PortNr);
            Console.Clear();
            while (ip == ip)
            {
                Console.WriteLine("vil du skrive til en ny IP?");
                ipNew = Console.ReadLine();
                if (ipNew == "yes")

                    Console.Write("indtast din bedsked: ");
                msg = Console.ReadLine();

                Console.WriteLine("Nu har jeg sent det");
                byte[] sendBuf = Encoding.ASCII.GetBytes("\nName: " + navn /*+ "\n" + theIP + ":" +thePort */+ "\nMessage:" + "\n" + msg);
                socket.SendTo(sendBuf, ep);
            }
            if(time.AddSeconds(30) > DateTime.UtcNow)
            {
                StartServer();
            }
        }

        private static void StartServer()
        {
            var time = DateTime.UtcNow;
            listener = new UdpClient();
            listener.Client.Bind(new IPEndPoint(IPAddress.Any, PortNr));
            try
            {
                byte[] bytes = listener.Receive(ref groupEP);
                Console.WriteLine("Opkald fra {0}:\n {1}\n",
                                  groupEP.ToString(),
                                  Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
