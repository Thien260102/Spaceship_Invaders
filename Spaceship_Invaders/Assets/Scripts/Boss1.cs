using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
namespace Assets.Scripts
{
    public class Boss1 : Boss
    {
        // Update is called once per frame
        void Update()
        {
            if (HP <= 0)
                IsDeleted = true;

            if (DeltaTime < maxTime)
                DeltaTime += Time.deltaTime;
            else
            {
                DeltaTime = 0;
                Action();
                maxTime = Random.Range(1, 5);
            }

            if (nextDestinationNode < path.NodeCount())
            {
                Movement();
            }
            else if (OrbitPath != null)
            {
                if (nextNode < OrbitPath.NodeCount())
                    OrbitMovement();
                else
                    nextNode = 0;
            }
        }



        protected override void Action()
        {
            Vector2 position = Body.position;
            StartCoroutine(CircleShooting(0.0f, position, 10));
            StartCoroutine(CircleShooting(0.2f, position, 10));
            StartCoroutine(CircleShooting(0.4f, position, 10));
        }

    }
}
