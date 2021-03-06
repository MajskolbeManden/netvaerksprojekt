﻿using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace netværksprojekt
{
    class Enemy : Component, ILoadable, IUpdateable, ICollisionEnter, ICollisionExit
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
            if (this.GameObject.Transform.Position.X < 5)
            {
                GameWorld.Instance.ObjectsToRemove.Add(this.GameObject);
                
            }
                
        }


        public void LoadContent(ContentManager content) { }

        public void OnCollisionEnter(Collider other)
        {

        }

        public void OnCollisionExit(Collider other)
        {
        }
    }
}
