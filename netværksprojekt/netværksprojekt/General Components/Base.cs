using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netværksprojekt
{
    class Base : Component, ICollisionEnter, ICollisionExit
    {
        Player player;
        public Base(GameObject gameobject, Player player) : base(gameobject)
        {
            this.player = player;
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Enemy") is Enemy)
            {
                GameWorld.Instance.ObjectsToRemove.Add(other.GameObject);
                player.Health -= 10;
            }
                
        }

        public void OnCollisionExit(Collider other)
        {
        }
    }
}
