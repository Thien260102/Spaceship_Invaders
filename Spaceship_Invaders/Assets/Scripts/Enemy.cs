using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : Entity
    {
        //maybe will add some methods or properties of Enemy in the future 

        public Bullet bullet;

        public Path path;
        public int nextDestinationNode;

        public Path OrbitPath;
        public int nextNode;

        public void Init()
        {
            ID = Variables.ENEMY;
            nextNode = 0;
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

        protected void OrbitMovement()
        {
            Vector2 destination = new Vector2(OrbitPath.GetNodePosition(nextNode).x, OrbitPath.GetNodePosition(nextNode).y);
            Body.position = Vector2.Lerp(Body.position, destination, Variables.EnemyFlySpeed / 2 * Time.deltaTime);

            if (Vector2.Distance(Body.position, destination) < 3f)
            {
                nextNode++;
            }
        }    

    }
}
