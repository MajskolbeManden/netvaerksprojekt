using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace netværksprojekt
{
    class Player : Component, ILoadable, IUpdateable
    {
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

        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;
            PlayerController(keyState, translation);

        }

        private void PlayerController(KeyboardState keyState, Vector2 translation)
        {

            if (keyState.IsKeyDown(Keys.D))
                translation += new Vector2(1, 0);

            if (keyState.IsKeyDown(Keys.A))
                translation += new Vector2(-1, 0);

            if (keyState.IsKeyDown(Keys.W))
                translation += new Vector2(0, -1);

            if (keyState.IsKeyDown(Keys.S))
                translation += new Vector2(0, 1);

        }
    }
}