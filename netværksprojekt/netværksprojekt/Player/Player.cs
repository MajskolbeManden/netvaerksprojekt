using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace netværksprojekt
{
    class Player : Component, ILoadable, IUpdateable
    {
        private Transform transform;
        private string name;
        private int health;
        private int highscore;
        private int income;
        private int damage;

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
            health = 10;
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = transform.Position;
            PlayerController(keyState,translation);
            

        }

        private void PlayerController(KeyboardState keyState, Vector2 translation)
        {
            if (keyState.IsKeyDown(Keys.D))
                transform.Position += new Vector2(1, 0);

            if (keyState.IsKeyDown(Keys.A))
                transform.Position += new Vector2(-1, 0);

            if (keyState.IsKeyDown(Keys.W))
                transform.Position += new Vector2(0, -1);

            if (keyState.IsKeyDown(Keys.S))
                transform.Position += new Vector2(0, 1);


        }
    }
}