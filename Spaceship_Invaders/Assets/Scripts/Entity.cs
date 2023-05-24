using System.Collections;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{
    public class Entity : MonoBehaviour
    {
        public int ID { get; set; } //player, enemies, bullet

        public int HP { get; set; }

        public Rigidbody2D Body;

        public GameObject Explosion;

        public Variables.Skill_Effect isEffecting { get; set; }

        bool isDeleted = false;
        public bool IsDeleted 
        { 
            get { return isDeleted; }
            set { if (isDeleted != value) isDeleted = value; }
        }

        public int State { get; set; }

        public Entity()
        {
            IsDeleted = false;
            //Debug.Log("Constructor");
        }

        public virtual void DamageTaken(int dame)
        {
            HP -= dame;
            if (HP <= 0)
                IsDeleted = true;
        }

        public virtual void EffectTaken(float time, Variables.Skill_Effect effect)
        {
            isEffecting = effect;
            Debug.Log("Effect: " + effect);

            switch(effect)
            {
                case Variables.Skill_Effect.None:

                    break;

                case Variables.Skill_Effect.OppositeDirection:

                    break;
            }

            Invoke("EndEffect", time);
        }

        protected void EndEffect()
        {
            isEffecting = Variables.Skill_Effect.None;

            Debug.Log("End effect");
        }

        public void Destructor()
        {
            if (Explosion != null)
            {
                Instantiate(Explosion, transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            }
            Destroy(gameObject);
        }

    }
}
