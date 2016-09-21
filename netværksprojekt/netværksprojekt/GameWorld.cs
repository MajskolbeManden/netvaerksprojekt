using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace netværksprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static float deltaTime;
        private static GameWorld instance;
        private Random Rnd;
        private static List<GameObject> objectsToAdd = new List<GameObject>();
        private static List<GameObject> objectsToRemove = new List<GameObject>();
        private static List<GameObject> gameObjects = new List<GameObject>();
        public List<Collider> colliders = new List<Collider>();
        public bool spawnEnemy = true;
        private float spawnCountdown;
        private float spawnCooldown = 0.5f;
        UDP udp;
        Thread t;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameWorld();

                return instance;
            }
        }
        #region properties
        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public float DeltaTime
        {
            get { return deltaTime; }
        }

        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        public List<GameObject> ObjectsToRemove
        {
            get { return objectsToRemove; }
            set { objectsToRemove = value; }
        }

        public List<GameObject> ObjectsToAdd
        {
            get { return objectsToAdd; }
            set { objectsToAdd = value; }
        }

        public List<Collider> Colliders
        {
            get
            {
                List<Collider> tmp = new List<Collider>();
                foreach (GameObject go in GameObjects)
                {
                    tmp.Add(go.GetComponent<Collider>());
                }
                return tmp;

            }
        }
        #endregion
        private void CreateEnemy()
        { 
            Rnd = new Random();
            int randomX = Rnd.Next(700,800);
            int randomY = Rnd.Next(0, 450);
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new Enemy(gameObject));
            gameObject.AddComponent(new SpriteRenderer(gameObject, "enemy", 1f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = new Vector2(randomX, randomY);
            
            objectsToAdd.Add(gameObject);
        }

        

        private void SpawnEnemy()
        {
            if (spawnEnemy == true)
            {
                if (spawnCountdown > 0)
                    spawnCountdown -= deltaTime;

                if (spawnCountdown <= 0)
                {
                    spawnCountdown = 0;
                    CreateEnemy();
                    spawnCountdown = spawnCooldown;
                }
            }
            else
            {

            }

        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
           
            
            // TODO: Add your initialization logic here
            GameObject player = new GameObject();
            player.AddComponent(new Player(player));
            player.AddComponent(new SpriteRenderer(player, "testSprite.png", 1f));
            player.AddComponent(new Collider(player));
            player.Transform.Position = new Vector2(10, 10);
            
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "base1", 0f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.AddComponent(new Base(gameObject, player.GetComponent("Player") as Player));
            gameObject.Transform.Position = new Vector2(0, 30);
            gameObjects.Add(gameObject);
            
            gameObject = new GameObject();
            gameObject.AddComponent(new SpriteRenderer(gameObject, "base2", 0f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = new Vector2(0, 230);
            gameObjects.Add(gameObject);

            gameObjects.Add(player);

            udp = new UDP();
            base.Initialize();
            
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }


            IsMouseVisible = true;
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
    
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            foreach (GameObject go in objectsToAdd)
            {
                go.LoadContent(Content);
                gameObjects.Add(go);
            }
            objectsToAdd.Clear();

            foreach (GameObject go in ObjectsToRemove)
            {
                gameObjects.Remove(go);
            }

            objectsToRemove.Clear();

            foreach (GameObject go in gameObjects)
                go.Update();


            SpawnEnemy();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            

            foreach (GameObject go in gameObjects)
                go.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
//class UDP
//{


//    public static IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 12000);
//    private static UdpClient listener = new UdpClient();
//    private static UdpClient sender = new UdpClient();
//    public static string theIP = "192.168.87.102";
//    public static Vector2 msg;
//    public static string stringtest;
//    public int pl2;
//    //private static IPAddress ip = IPAddress.Parse(theIP);
//    // private IPEndPoint ep = new IPEndPoint(ip, PortNr);
//    public static Thread t;
//    public UDP()
//    {
//        t = new Thread(StartServer);
//        t.Start();
//    }
//    public void StartClient()
//    {
//        int PortNr = 13000;

//        msg = new Vector2(0, 0);
//        foreach (GameObject go in GameWorld.Instance.GameObjects)
//        {
//            if (go.GetComponent<Player>() != null)
//            {
//                msg = go.Transform.Position;
//            }
//        }

//        var time = DateTime.UtcNow;

//        Socket socket = new Socket(AddressFamily.InterNetwork,
//                        SocketType.Dgram,
//                        ProtocolType.Udp);

//        //string theIP = "127.0.0.1";
//        //string ipNew;
//        //bool newIP = false;
//        IPAddress idb = IPAddress.Broadcast;
//        //Socket socket = new Socket(AddressFamily.InterNetwork,
//        //                SocketType.Dgram,
//        //                ProtocolType.Udp);
//        IPAddress ip = IPAddress.Parse(theIP);
//        IPEndPoint ep = new IPEndPoint(ip, PortNr);
//        //while (ip == ip)
//        //{
//        //    byte[] packetData = Encoding.ASCII.GetBytes(theIP + ":" + PortNr + "\nPacket: " + "\n" + msg);
//        //    socket.SendTo(packetData, ep);
//        //}

//    }

//    public void StartServer()
//    {

//        Stopwatch watch = new Stopwatch();
//        watch.Start();
//        listener = new UdpClient();
//        listener.Client.Bind(new IPEndPoint(IPAddress.Any, 12000));

//        try
//        {
//            while (true)
//            {




//                byte[] bytes = listener.Receive(ref groupEP);
//                Console.WriteLine("Broadcast fra: {0}:{1}\n{2}:{3}",
//                                   groupEP.ToString(),
//                                  Encoding.ASCII.GetString(bytes, 0, bytes.Length));
//                Debug.WriteLine(bytes);
//                //string stringtest = bytes.ToString();
//                //pl2 = Convert.ToInt32(stringtest);

//                //byte[] packetData = Encoding.ASCII.GetBytes(theIP + ":"  + "\nPacket: " + "\n" + msg);
//                //listener.Client.Send(packetData);

//            }
//        }

//        catch (Exception e)
//        {

//            Console.WriteLine(e.ToString());
//        }
//    }
//}
