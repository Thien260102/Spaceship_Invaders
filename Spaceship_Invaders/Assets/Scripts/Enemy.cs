using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : Entity
    {
        //maybe will add some methods or properties of Enemy in the future 

        public Bullet bullet;
        
        public void Init()
        {
            ID = Variables.ENEMY;
        }

    }
}
