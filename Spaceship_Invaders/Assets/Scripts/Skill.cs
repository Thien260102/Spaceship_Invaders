using UnityEngine;

namespace Assets.Scripts
{
    public class Skill : Entity
    {

        public int Type { get; set; }

        public int Damage { get; set; }

        public Vector2 Direction { get; set; } = new Vector2(0, -1);

        public int Effect { get; set; } // Stun, Slow, Ignite, ...

        protected bool isMovable = true;

        [SerializeField]
        bool isUnstoppable = false;

        public float Duration;
        float TotalTime = 0;

        public GameObject Explosion;

        private void Start()
        {
            Body = GetComponent<Rigidbody2D>();
            ID = Variables.SKILL;

        }

        public void Init(int type, int damage, Vector2 direction)
        {
            Effect = 0;
            Type = type;
            Damage = damage;

            Direction = direction;
            float angle = Vector2.Angle(direction, new Vector2(1, 0));
            if (direction.y < 0)
                transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
            else
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //Debug.Log("Angle: " + angle);
        }

        public void Init(int type, int damage, Vector2 direction, int duration)
        {
            Effect = 0;
            Type = type;
            Damage = damage;
            Duration = duration;

            this.Direction = direction;
            float angle = Vector2.Angle(direction, new Vector2(1, 0));
            if (direction.y < 0)
                this.transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
            else
                this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //Debug.Log("Angle: " + angle);
        }

        public void Init(int type, int damage, Vector2 direction, int effect, int duration)
        {
            Type = type;
            Damage = damage;

            this.Direction = direction;
            float angle = Vector2.Angle(direction, new Vector2(1, 0));
            if (direction.y < 0)
                this.transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
            else
                this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Effect = effect;
            Duration = duration;
            //Debug.Log("Angle: " + angle);
        }

        private void Update()
        {
            if(isMovable)
            {
                Vector2 Position = Body.position;
                switch (Type)
                {
                    case Variables.ByPlayer:
                        Position += Variables.PlayerBulletSpeed * Direction * Time.deltaTime;
                        //Debug.Log("Player shooting");
                        break;

                    default:
                        Position += Variables.EnemyBulletSpeed * Direction * Time.deltaTime;
                        break;
                }

                Body.position = Position;
            }

            TotalTime += Time.deltaTime;
            if (TotalTime >= Duration)
                HandleDestroy();
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Entity entity = collision.GetComponent<Entity>();

            if (entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.ASTEROID:
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            entity.DamageTaken(Damage);

                            if (entity is Boss)
                                HUD.Instance.Score += Damage;


                            if (!isUnstoppable)
                                HandleDestroy();
                        }
                        break;

                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            entity.IsDeleted = true;


                            if (!isUnstoppable)
                                HandleDestroy();
                        }
                        break;
                }


            }

        }

        protected virtual void HandleDestroy()
        {
            if(Explosion != null)
                Instantiate(Explosion, transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

            Destroy(gameObject);
        }

    }
}
