using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        private float spawnCountdown;
        private float spawnCooldown = 0.5f;
        public bool stopSpawn;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameWorld();

                return instance;
            }
        }

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public float DeltaTime
        {
            get { return deltaTime; }
        }

        public static List<GameObject> GameObjects
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

        

<<<<<<< HEAD
       
            
                
               
           
        
=======
        private void SpawnEnemy(KeyboardState keyState)
        {
            if (stopSpawn == false)
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
>>>>>>> 104857691ffa4f4c2d138204d1623f9bf50d5b2f

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

            KeyboardState keyState = Keyboard.GetState();

            CreateEnemy();


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
