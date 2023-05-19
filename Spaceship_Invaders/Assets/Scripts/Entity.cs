using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
