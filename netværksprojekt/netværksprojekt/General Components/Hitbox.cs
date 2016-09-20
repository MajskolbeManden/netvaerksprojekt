using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace netværksprojekt
{
    class Hitbox : Component, IUpdateable, ICollisionEnter, ICollisionExit
    {
        
        public Hitbox(GameObject gameobject) : base(gameobject)
        {

        }

        public void Update()
        {
            
        }

        public void OnCollisionEnter(Collider other)
        {
            
             
        }

        public void OnCollisionExit(Collider other)
        {

        }

        
    }
}
