using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyWave: MonoBehaviour
    {
        public int[] CorrespondingEnemyQuantity = new int[6];

        public Vector2 StartPoint; // Initialize point, and start
        public Vector2 EndPoint;   // End point from start
        private Vector2 Direction;

        public float ReciprocastingRange;

        private bool IsTouchTheEndPoint = false;
        private Vector2 OldPos;

        private List<GameObject> CurrentEnemyWave = null;


        private void Awake()
        {
            // determine direction of EnemyWave to use.
            if (StartPoint.x < EndPoint.x)
                Direction.x = 1;
            else if (StartPoint.x > EndPoint.x)
                Direction.x = -1;
            else
                Direction.x = 0;

            if (StartPoint.y < EndPoint.y)
                Direction.y = 1;
            else if (StartPoint.y > EndPoint.y)
                Direction.y = -1;
            else
                Direction.y = 0;

        }

        public void HandleUpdate()
        {
            //Enemies are moving
            Movement();

            //Deleting death enemies
            PurgeDeletedObjects();

        }

        public bool HasEnemies() { return CurrentEnemyWave != null && CurrentEnemyWave.Count > 0; }

        private void PurgeDeletedObjects()
        {
            foreach (GameObject o in CurrentEnemyWave.ToList())
            {
                if (o.activeSelf == false)
                {
                    CurrentEnemyWave.Remove(o);
                    Destroy(o);
                    Debug.Log("Delete");
                }
            }
        }

        private void Movement()
        {
            if(IsTouchTheEndPoint == false)
            {
                FlyIntoTheEndPoint();
            }
            else
            {
                ReciprocastingMovement();
            }
        }

        public void InitEnemyWave(GameObject[] enemyReference)
        {
            CurrentEnemyWave = new List<GameObject>();

            Vector2 position = StartPoint;
            for (int i = 0; i < CorrespondingEnemyQuantity.Length; i++)
            {
                position.y += 1.5f;
                for (int j = 0; j < CorrespondingEnemyQuantity[i]; j++)
                {
                    position.x -= 1;
                    GameObject Instantiate_Enemy = Instantiate(enemyReference[i], position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
                    Instantiate_Enemy.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);

                    CurrentEnemyWave.Add(Instantiate_Enemy);
                }

            }
        }

        
        private void FlyIntoTheEndPoint() // EnemyWave start from outside and fly into the Screen.
        {
            OldPos = CurrentEnemyWave[0].transform.position;

            if (Direction.x == 0 && Direction.y == 0)
            {
                Direction.x = 1;
                IsTouchTheEndPoint = true;
                return;
            }

            Vector2 position;

            for(int i = 0; i < CurrentEnemyWave.Count; i++)
            {
                position = CurrentEnemyWave[i].transform.position;
                position += Direction * Variables.EnemyFlySpeed;

                if((Direction.x > 0 && position.x > EndPoint.x)
                    || (Direction.x < 0 && position.x < EndPoint.x))
                {
                    Direction.x = 0;
                }

                if ((Direction.y > 0 && position.y > EndPoint.y)
                    || (Direction.y < 0 && position.y < EndPoint.y))
                {
                    Direction.y = 0;
                }

                CurrentEnemyWave[i].transform.position = position;
            }
            
        }

        private void ReciprocastingMovement() // EnemyWave is reciprocasting
        {
            Vector2 position;

            for (int i = 0; i < CurrentEnemyWave.Count; i++)
            {
                position = CurrentEnemyWave[i].transform.position;
                position += Direction * Variables.EnemyFlySpeed;

                if (position.x > Variables.ScreenWidth / 2.0f)
                    Direction.x = -1;
                else if (position.x < -Variables.ScreenWidth / 2.0f)
                    Direction.x = 1;

                Debug.Log(position);
                CurrentEnemyWave[i].transform.position = position;
            }

            Debug.Log("Reciprocasting");
        }    

        public void Destructor()
        {
            CurrentEnemyWave.Clear();
            Destroy(this);
        }
    }
}
