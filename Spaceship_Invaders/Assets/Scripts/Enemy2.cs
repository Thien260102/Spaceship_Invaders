using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{
    public class Enemy2 : Enemy
    {
        float DeltaTime;
        float maxTime;

        void Awake()
        {
            this.Init();
            HP = Variables.HP_Enemy2;

            DeltaTime = 0;
            maxTime = Random.Range(2, 4);
            Body = GetComponent<Rigidbody2D>();
            nextDestinationNode = 1;
        }

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
                Shooting();
                maxTime = Random.Range(2, 4);
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

        private void Shooting()
        {
            Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust * 3);
            Vector2 direction = new Vector2(position.x + 1, position.y - 1) - position;

            Shoot(position, direction);

            direction = new Vector2(position.x - 1, position.y - 1) - position;

            Shoot(position, direction);
        }
    }
}
