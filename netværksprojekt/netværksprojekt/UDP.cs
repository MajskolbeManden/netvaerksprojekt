using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Diagnostics;

namespace netværksprojekt
{
    class UDP
    {
        private static UdpClient listener = new UdpClient();
        private static UdpClient sender = new UdpClient();
        public static string theIP = "127.0.0.1";
        public static Vector2 msg;
        public static float Xcor;
        public static float Ycor;
        public static Thread l;
        public static Thread t;
        public UDP()
        {
            
        }

        public void StartServer()
        {
            t = new Thread(Server);
            t.Start();
            l = new Thread(ServerListener);
            l.Start();
        }

        public void StartClient()
        {
            t = new Thread(Client);
            t.Start();
            l = new Thread(ClientListener);
            l.Start();
        }

        public static void Client()
        {
            int PortNr = 12000;
            var time = DateTime.UtcNow;
            
            Socket socket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram,
                            ProtocolType.Udp);
            IPAddress idb = IPAddress.Broadcast;
            IPAddress ip = IPAddress.Parse(theIP);
            IPEndPoint ep = new IPEndPoint(ip, PortNr);
       
            while (ip==ip)
            {
                foreach (GameObject go in GameWorld.Instance.GameObjects)
                {
                    if (go.GetComponent<Player>() != null)
                    {
                        Xcor = go.Transform.Position.X;
                        Ycor = go.Transform.Position.Y;
                    }
                }
                byte[] packetData = Encoding.ASCII.GetBytes(theIP + " : " + PortNr + "\nPacket: " + "\n" + Xcor + "," + Ycor);
                socket.SendTo(packetData, ep);
            }
            
        }
        
        public void ClientListener()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork,
                          SocketType.Dgram,
                          ProtocolType.Udp);
            UdpClient listener = new UdpClient();
            listener.Client.Bind(new IPEndPoint(IPAddress.Any, 13000));
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 13000);
            while (true)
            {
              byte[] bytes = listener.Receive(ref groupEP);

            Console.WriteLine("Received Packets from {0} :\n {1}\n",
                        groupEP.ToString(),
                        Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            }
            
        }

        public void Server()
        {
            int PortNr = 13000;
            var time = DateTime.UtcNow;

            Socket socket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram,
                            ProtocolType.Udp);
            IPAddress idb = IPAddress.Broadcast;
            IPAddress ip = IPAddress.Parse(theIP);
            IPEndPoint ep = new IPEndPoint(ip, PortNr);

            while (ip == ip)
            {
                foreach (GameObject go in GameWorld.Instance.GameObjects)
                {
                    if (go.GetComponent<Player>() != null)
                    {
                        Xcor = go.Transform.Position.X;
                        Ycor = go.Transform.Position.Y;
                    }
                }
                byte[] packetData = Encoding.ASCII.GetBytes(theIP + " : " + PortNr + "\nPacket: " + "\n" + Xcor + "," + Ycor);
                socket.SendTo(packetData, ep);
            }


        }
        public void ServerListener()
        {

            UdpClient listener = new UdpClient();
            listener.Client.Bind(new IPEndPoint(IPAddress.Any, 12000));
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 12000);
            while (true)
            {
                byte[] bytes = listener.Receive(ref groupEP);

                Console.WriteLine("Received Packets from {0} :\n {1}\n",
                            groupEP.ToString(),
                            Encoding.ASCII.GetString(bytes, 0, bytes.Length));
            }

        }
    }
}
