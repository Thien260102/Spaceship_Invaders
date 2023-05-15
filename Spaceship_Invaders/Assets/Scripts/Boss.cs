using System.Collections;
using UnityEngine;
using Assets.Scripts;


namespace Assets.Scripts
{
    public class Boss : Enemy
    {
        protected float DeltaTime;
        protected float maxTime;

        void Awake()
        {
            Init();
            HP = Variables.HP_Enemy5;

            DeltaTime = 0;
            maxTime = Random.Range(1, 5);
            Body = GetComponent<Rigidbody2D>();
            nextDestinationNode = 1;

            
        }

        protected virtual void Action()
        {

        }

        protected IEnumerator CircleShooting(float delayTime, Vector2 position, int quantity)
        {
            yield return new WaitForSeconds(delayTime);
            SkillManager.Instance.CircleShoot(quantity, position);

        }

        protected void DivineDeparture(int index = 1)
        {
            

        }

    }
}
