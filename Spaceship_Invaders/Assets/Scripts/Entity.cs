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

        public bool IsDeleted { get; set; }

        public int State { get; set; }

        public Entity()
        {
            IsDeleted = false;
            //Debug.Log("Constructor");
        }

        public void DamageTaken(int dame)
        {
            HP -= dame;
            if (HP <= 0)
                IsDeleted = true;
        }

        public void Destructor()
        {
            Destroy(gameObject);
        }

    }
}
