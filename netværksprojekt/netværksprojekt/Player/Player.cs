using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace netværksprojekt
{
    class Player : Component, ILoadable, IUpdateable, ICollisionEnter, ICollisionExit, IDrawable
    {
        private Transform transform;
        
        private string name;
        private int health;
        private float speed;
        private int highscore;
        private bool takeDamage;
        private SpriteFont font;
        private bool hit;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Highscore
        {
            get { return Highscore; }
            set { Highscore = value; }
        }

        public int Income
        {
            get { return Income; }
            set { Income = value; }
        }

        public Player(GameObject gameobject) : base(gameobject)
        {
            transform = gameobject.Transform;
            health = 100;
            speed = 6;
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("health");
        }

        public void Update()
        {
            if(health <= 0)
            {
                health = 0;
                GameWorld.Instance.ObjectsToRemove.Add(this.GameObject);
                GameWorld.Instance.stopSpawn = true;
            }
            KeyboardState keyState = Keyboard.GetState();

            highscore++;

            Vector2 translation = transform.Position;
            PlayerController(keyState,translation);

            if (keyState.IsKeyDown(Keys.Space) && hit == false)
                hit = true;

            if (keyState.IsKeyUp(Keys.Space))
                hit = false;


        }

        private void PlayerController(KeyboardState keyState, Vector2 translation)
        {
            if (keyState.IsKeyDown(Keys.D))
                transform.Position += new Vector2(1, 0) * speed;

            if (keyState.IsKeyDown(Keys.A))
                transform.Position += new Vector2(-1, 0) * speed;

            if (keyState.IsKeyDown(Keys.W))
                transform.Position += new Vector2(0, -1) * speed;

            if (keyState.IsKeyDown(Keys.S))
                transform.Position += new Vector2(0, 1) * speed;

            

        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Enemy") is Enemy)
            {
                health--;
            }

            if (other.GameObject.GetComponent("Enemy") is Enemy && hit == true)
            {
                GameWorld.Instance.ObjectsToRemove.Add(other.GameObject);
                highscore += 100;
            }
                

        }

        public void OnCollisionExit(Collider other)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Health: " + health, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, "Score: " + highscore, new Vector2(130, 10), Color.Black);
        }
    }
}