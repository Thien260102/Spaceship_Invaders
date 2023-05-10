using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Missle : Entity
    {
        public int Type { get; set; }

        public int Damage { get; set; }

        public float RotationSpeed { get; set; }

        public GameObject Explosion;

        GameObject target;

        void Start()
        {
            Init(Variables.ByPlayer);
        }

        public void Init(int type = 10, int damage = Variables.Damage_Missle_Default)
        {
            ID = Variables.MISSLE;
            Type = type;
            Damage = damage;

            Body = GetComponent<Rigidbody2D>();
            Body.gravityScale = 0;

            RotationSpeed = Variables.PlayerMissleRotatingSpeed;
        }

        private void Update()
        {
            target = null;
            //find target to chase
            float minDistance = 100f;
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            List<Collider2D> results = new List<Collider2D>();
            Vector2 center = this.transform.position + this.transform.up.normalized * (Variables.PlayerMissleCircleCastRadius / 2);
            for (int i = 0; i < Physics2D.OverlapCircle(center, Variables.PlayerMissleCircleCastRadius, filter, results); i++)
            {
                GameObject o = results[i].gameObject;
                Entity e = results[i].gameObject.GetComponent<Entity>();
                float distance = Vector2.Distance(this.transform.position, o.transform.position);
                if (e.ID == Variables.ENEMY && distance < minDistance)
                {
                    minDistance = distance;
                    target = o;
                }
            }

            if (target != null) //follow target
            {
                Vector2 direction = (Vector2)target.transform.position - Body.position;

                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.up).z;

                Body.angularVelocity = -rotateAmount * RotationSpeed;
            } else
            {
                Body.angularVelocity = 0;
            }

            switch (Type)
            {
                case Variables.ByPlayer:
                    Body.velocity = Variables.PlayerMissleSpeed * transform.up;
                    break;

                default:
                    Body.velocity = Variables.EnemyMissleSpeed * transform.up;
                    break;
            }

            float halfHeight = Variables.ScreenHeight / 2;

            Vector2 Position = this.transform.position;

            //bullet out of screen, so delete it.
            if (Position.y > halfHeight || Position.y < -halfHeight)
            {
                //Debug.Log("Bullet out of screen");
                Destroy(this.gameObject);
            }

            //Debug.Log("update");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Entity entity = collision.GetComponent<Entity>();

            if (entity != null)
            {
                switch (entity.ID)
                {
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            Instantiate(Explosion, collision.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(Damage);

                            Destroy(this.gameObject);
                            //Debug.Log("Collision");
                        }
                        break;

                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            Instantiate(Explosion, collision.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.IsDeleted = true;
                            Destroy(this.gameObject);
                            Debug.Log("GameOver");
                        }
                        break;
                }
            }

        }
    }

}