using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace netværksprojekt
{
    class Enemy : Component, ILoadable, IUpdateable
    {
        private int health;
        private Transform transform;
        private float speed = 5;
        private int damage;
        
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Enemy(GameObject gameobject) : base(gameobject)
        {
            health = 5;
            transform = gameobject.Transform;
        }

        public void Update()
        {
            transform.Position += new Vector2(-1, 0) * speed;
        }


        public void LoadContent(ContentManager content) { }
    }
}
