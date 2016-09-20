﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private static List<GameObject> objectsToAdd = new List<GameObject>();
        private static List<GameObject> objectsToRemove = new List<GameObject>();
        private static List<GameObject> gameObjects = new List<GameObject>();
        public List<Collider> colliders = new List<Collider>();
        private bool spawnEnemy;

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
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new Enemy(gameObject));
            gameObject.AddComponent(new SpriteRenderer(gameObject, "enemy", 1f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = new Vector2(700, 10);

            objectsToAdd.Add(gameObject);
        }

        private void SpawnEnemy(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && spawnEnemy == true)
            {
                CreateEnemy();
                spawnEnemy = false;
            }
            if (keyState.IsKeyUp(Keys.Space))
            {
                spawnEnemy = true;
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
            GameObject gameObject = new GameObject();
            gameObject.AddComponent(new Player(gameObject));
            gameObject.AddComponent(new SpriteRenderer(gameObject, "testSprite.png", 1f));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.Transform.Position = new Vector2(10, 10);

            gameObjects.Add(gameObject);
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

            SpawnEnemy(keyState);

            
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
