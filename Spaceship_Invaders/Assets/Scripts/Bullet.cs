using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : Entity
    {
        public int Type { get; set; }

        public int Damage { get; set; }

        public Vector2 Direction { get; set; } = new Vector2(0, -1);

        public GameObject Explosion;

        public void Init(int type = 10, int damage = Variables.Damage_Bullet_Default)
        {
            ID = Variables.BULLET;
            Type = type;
            Damage = damage;

            Body = GetComponent<Rigidbody2D>();
            Body.gravityScale = 0;
        }

        private void Update()
        {
            Vector2 Position = Body.position;

            
            switch (Type)
            {
                case Variables.ByPlayer:
                    Position.y += Variables.PlayerBulletSpeed * Time.deltaTime;
                    //Debug.Log("Player shooting");
                    break;

                default:
                    Position += Variables.EnemyBulletSpeed * Direction * Time.deltaTime;
                    break;
            }

            Body.position = Position;

            float halfHeight = Variables.ScreenHeight / 2;

            Vector2 BulletPosition = this.transform.position;

            //bullet out of screen, so delete it.
            if (BulletPosition.y > halfHeight || BulletPosition.y < -halfHeight)
            {
                //Debug.Log("Bullet out of screen");
                Destroy(gameObject);
            }

            //Debug.Log("update");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Entity entity = collision.GetComponent<Entity>();
            
            if(entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.ASTEROID:
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            Instantiate(Explosion, collision.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(Damage);

                            Destroy(gameObject);
                            //Debug.Log("Collision");
                        }
                        break;

                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            Instantiate(Explosion, collision.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.IsDeleted = true;
                            Destroy(gameObject);
                        }
                        break;
                }
            }    
            
        }
    }

}
