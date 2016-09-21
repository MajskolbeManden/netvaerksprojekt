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
        
        
     //   public static IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, PortNr);
        private static UdpClient listener = new UdpClient();
        private static UdpClient sender = new UdpClient();
        public static string theIP = "192.168.87.102";
        public static Vector2 msg;
        public static float Xcor;
        public static float Ycor;
        //private static IPAddress ip = IPAddress.Parse(theIP);
       // private IPEndPoint ep = new IPEndPoint(ip, PortNr);
        public static Thread t;
        public UDP()
        {
            t = new Thread(StartClient);
            t.Start();
        }
        public void StartClient()
        {
             int PortNr = 12000;
           
            msg = new Vector2(0,0);
            //foreach (GameObject go in GameWorld.Instance.GameObjects)
            //{
            //    if(go.GetComponent<Player>() != null)
            //    {
            //        msg = go.Transform.Position;
            //        Debug.WriteLine(msg);
            //    }
            //}

            var time = DateTime.UtcNow;

            Socket socket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram,
                            ProtocolType.Udp);

            //string theIP = "127.0.0.1";
            //string ipNew;
            //bool newIP = false;
            IPAddress idb = IPAddress.Broadcast;
            //Socket socket = new Socket(AddressFamily.InterNetwork,
            //                SocketType.Dgram,
            //                ProtocolType.Udp);
            IPAddress ip = IPAddress.Parse(theIP);
            IPEndPoint ep = new IPEndPoint(ip, PortNr);
            while (true)
            {
                foreach (GameObject go in GameWorld.Instance.GameObjects)
                {
                    if (go.GetComponent<Player>() != null)
                    {
                        Xcor = go.Transform.Position.X;
                        Ycor = go.Transform.Position.Y;
                        Debug.WriteLine(Ycor);
                        Debug.WriteLine(Xcor);
                    }
                }
                byte[] packetData = Encoding.ASCII.GetBytes(theIP + " : " + PortNr + "\nPacket: " + "\n" + Xcor + Ycor);
                socket.SendTo(packetData, ep);
            }

        }
      
        public void StartServer()
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();
            listener = new UdpClient();
            listener.Client.Bind(new IPEndPoint(IPAddress.Any, 12000));
            
            try
            {
                while(true)
                {

                    for (int i = 0; i < 1000; i++) 
                    {
                        //byte[] bytes = listener.Receive(ref groupEP);
                        //Console.WriteLine("Broadcast fra: {0}:{1}\n",
                        //                   groupEP.ToString(),
                        //                   Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                        
                    } 
                     StartClient();          
                }    
            }
                
               catch (Exception e)
               {
                
                Console.WriteLine(e.ToString());
               }
       }      
   }
}
