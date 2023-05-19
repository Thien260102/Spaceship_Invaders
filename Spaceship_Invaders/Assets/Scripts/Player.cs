using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class DictionarySkill
    {
        [SerializeField] public KeyCode key;
        [SerializeField] public Variables.Skill_Type skill;
    }

    public class Player : Entity
    {
        private Camera mainCamera;
        public List<Weapon> weapons;
        int currentWeapon;
        public int CurrentWeapon { get { return currentWeapon; } }

        [SerializeField]
        List<DictionarySkill> playerSkill;

        [SerializeField]
        FuelStateBar fuel;

        [SerializeField]
        Exhaust exhaust;

        public Animator animator;
        private Vector2 Velocity;

        public GameObject pauseMenu;
        private bool paused = false;

        private Vector3 lastFrameMousePosition;
        public float sensitivity = 1.0f;

        public bool invincible = false;

        public float MaximumSpeed;
        public float MinimumSpeed;
        private float SpeedCap;

        float halfHeight;
        float halfWidth;

        private void Start()
        {
            Body = GetComponent<Rigidbody2D>();
            lastFrameMousePosition = Body.position;

            Body.isKinematic = false; // turn on OncollisionEnter2d
            Body.gravityScale = 0.0f;

            halfHeight = Variables.ScreenHeight / 2;
            halfWidth = Variables.ScreenWidth / 2;
            currentWeapon = 0;

            mainCamera = Camera.main;
            SpeedCap = MaximumSpeed;

            Cursor.lockState = CursorLockMode.Confined;
            StartCoroutine(SetInvincible(3.0f));
        }

        // Update is called once per frame
        void Update()
        {
            SpeedCap = MaximumSpeed;
            if (fuel.Contain <= 1)
            {
                SpeedCap = MinimumSpeed;
            }
            /*if (fuel.Contain == 0)
            {
                IsDeleted = true;
                fuel.Fill();
            }*/

            if (IsDeleted)
            {
                if (State != Variables.Player_DESTROYED)
                    StartCoroutine(Destroyed());
            }
            else
            {
                KeyboardController();
                if (!paused)
                {
                    MouseController2();
                }

                SetState();
            }
        }

        public IEnumerator SetInvincible(float duration)
        {
            invincible = true;
            yield return new WaitForSeconds(duration);
            invincible = false;
        }

        public override void DamageTaken(int dame)
        {
            if (!invincible)
            {
                HP -= dame;
                if (HP <= 0)
                    IsDeleted = true;
            }
        }

        void SetState()
        {
            if (IsDeleted)
                State = Variables.Player_DESTROYED;
            else
                if (Velocity.x == 0 && Velocity.y == 0)
                State = Variables.Player_IDLE;
            else if (Velocity.y > 0)
                State = Variables.Player_BOOST;
            else
                State = Variables.Player_MOVE;

            animator.SetInteger("State", State);
        }

        void MouseController2()
        {
            //get mouse position
            Vector3 mousePosition = lastFrameMousePosition;
            mousePosition.x += Input.GetAxis("Mouse X") * sensitivity * SpeedCap * Time.deltaTime;
            mousePosition.y += Input.GetAxis("Mouse Y") * sensitivity * SpeedCap * Time.deltaTime;

            // Adding distance to handle Fuel
            fuel.Distance += Vector3.Distance(mousePosition, lastFrameMousePosition);
            if (Vector3.Distance(mousePosition, lastFrameMousePosition) <= 25)
            {
                fuel.Contain += 0.25f * Time.deltaTime;
            }


            Velocity = (mousePosition - lastFrameMousePosition) / Time.deltaTime;

            Body.position = mousePosition;
            Body.position = new Vector3(
                Mathf.Clamp(Body.position.x, -Variables.ScreenWidth / 2 + Variables.Adjust, Variables.ScreenWidth / 2 - Variables.Adjust),
                Mathf.Clamp(Body.position.y, -Variables.ScreenHeight / 2 + Variables.Adjust, Variables.ScreenHeight / 2 - Variables.Adjust),
                0f
            );

            lastFrameMousePosition = Body.position;

            Debug.Log(lastFrameMousePosition);

            // limit moving area of player
            //if (mousePosition.x < -halfWidth + Variables.Adjust)
            //    newbodyPosition.x = -halfWidth + Variables.Adjust;
            //else if (mousePosition.x > halfWidth - Variables.Adjust)
            //    newbodyPosition.x = halfWidth - Variables.Adjust;
            //else
            //    newbodyPosition.x = mousePosition.x;

            //if (mousePosition.y < -halfHeight + Variables.Adjust)
            //    newbodyPosition.y = -halfHeight + Variables.Adjust;
            //else if (mousePosition.y > halfHeight - Variables.Adjust)
            //    newbodyPosition.y = halfHeight - Variables.Adjust;
            //else
            //    newbodyPosition.y = mousePosition.y;
            //Body.position = newbodyPosition;

            
            // pressed mouse left // Spaceship shooting
            if (Input.GetMouseButtonDown(0))
            {
                weapons[currentWeapon].Trigger();
            }
            else if (Input.GetMouseButtonDown(2))
            {
                currentWeapon++;
                if (currentWeapon == weapons.Count)
                    currentWeapon = 0;

                //WeaponStateBar.Instance.Type = currentWeapon;
            }
            else if (Input.GetMouseButtonDown(1)) // hack level weapon
            {
                WeaponLevelUp(1);
                //WeaponStateBar.Instance.Level = Weapon.Level;
            }


            Cursor.visible = false; // invisible cursor
            Cursor.lockState = CursorLockMode.Confined;// block cursor into Game screen

            //lastFrameMousePosition = mainCamera.ScreenToWorldPoint(newbodyPosition); ;
        }

        void KeyboardController()
        {
            DictionarySkill skill = null;
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.Q);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.W);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.E);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                skill = playerSkill.Find(x => x.key == KeyCode.R);
            }

            if (skill != null)
                ActiveSkill(skill.skill);

        }

        void ActiveSkill(Variables.Skill_Type skill)
        {
            Vector2 position = Body.position;
            switch (skill)
            {
                case Variables.Skill_Type.CircleShooting:
                    SkillManager.Instance.CircleShoot(Variables.ByPlayer, 10, position, new Vector2(0, -1));
                    break;

                case Variables.Skill_Type.DivineDeparture:
                    SkillManager.Instance.DivineDeparture(Variables.ByPlayer, position, new Vector2(0, 1));
                    break;

                case Variables.Skill_Type.EnergyWave:
                    SkillManager.Instance.EnergyWave(Variables.ByPlayer, position, new Vector2(0, 1));
                    break;
            }
        }

        public void GamePaused()
        {
            paused = true;
            Time.timeScale = 0.0f;
        }

        public void GameResume()
        {
            //Since movement is detected by mouse position difference, reset last mouse position here so
            //there wont be a difference, then the player position wont jump.
            lastFrameMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Time.timeScale = 1.0f;

            paused = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsDeleted)
                return;

            GameObject Object = collision.gameObject;

            if (Object.tag == "Item")
            {
                Item item = collision.gameObject.GetComponent<Item>();
                CollectItem(item.Type);

                Destroy(Object);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsDeleted)
                return;

            GameObject Object = collision.gameObject;

            if (!invincible)
            {
                if (Object.tag == "Enemy")
                    IsDeleted = true;
            }
        }

        private void CollectItem(Variables.ItemType ItemType)
        {
            switch (ItemType)
            {
                case Variables.ItemType.Star:
                    WeaponLevelUp(1);

                    HUD.Instance.Score += 2000;
                    
                    break;

                case Variables.ItemType.Fuel:
                    fuel.Contain++;
                    break;

                case Variables.ItemType.Coin:
                    HUD.Instance.Coin++;
                    break;
            }

            Debug.Log(ItemType);
        }

        public void SetSmokeState(string state)
        {
            exhaust.SetSmokeState(state);
        }

        private void WeaponLevelUp(int amount)
        {
            Weapon.Level += amount;
            Weapon.Level = Mathf.Clamp(Weapon.Level, 1, 3);
        }

        public IEnumerator Destroyed()
        {
            SetState();
            Body.isKinematic = true; // turn off OncollisionEnter2d
            Debug.Log("Player Destroyed");
            HUD.Instance.Life--;
            Weapon.Level--;
            if (Weapon.Level < 1)
                Weapon.Level = 1;

            //WeaponStateBar.Instance.Level = Weapon.Level;

            yield return new WaitForSeconds(1f);

            if (HUD.Instance.Life <= 0)
                gameObject.SetActive(false);
            else
            {
                Body.isKinematic = false; // turn on OncollisionEnter2d
                IsDeleted = false;
                SetState();

                fuel.RenderNewState();
                StartCoroutine(SetInvincible(3.0f));
            }

        }
    }
}