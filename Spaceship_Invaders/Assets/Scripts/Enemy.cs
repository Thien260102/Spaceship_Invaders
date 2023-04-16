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

        public Path path;
        public int nextDestinationNode;

        public void Init()
        {
            ID = Variables.ENEMY;
        }


        protected void Movement()
        {
            Vector2 destination = new Vector2(path.GetNodePosition(nextDestinationNode).x, path.GetNodePosition(nextDestinationNode).y);
            Body.position = Vector2.Lerp(Body.position, destination, Variables.EnemyFlySpeed * Time.deltaTime);

            if (Vector2.Distance(Body.position, destination) < 3f)
            {
                nextDestinationNode++;
            }
        }

    }
}
