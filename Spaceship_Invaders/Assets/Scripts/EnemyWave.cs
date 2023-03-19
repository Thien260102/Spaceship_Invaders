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

        public Vector2 Range; // two direction: X-axis and Y-axis

        private List<GameObject> CurrentEnemyWave = null;


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
            float direction = 1.0f;
            if (CurrentEnemyWave[CurrentEnemyWave.Count - 1].transform.position.x > Variables.ScreenWidth / 2)
                direction = -1.0f;

            Vector2 position;
            foreach (GameObject e in CurrentEnemyWave)
            {
                position = e.transform.position;
                position.x += direction * 0.1f;

                e.transform.position = position;
            }
        }

        public void InitEnemyWave(GameObject[] enemyReference)
        {
            CurrentEnemyWave = new List<GameObject>();

            for (int i = 0; i < CorrespondingEnemyQuantity.Length; i++)
            {
                StartPoint.y += 2;
                for (int j = 0; j < CorrespondingEnemyQuantity[i]; j++)
                {
                    StartPoint.x -= 1;
                    GameObject Instantiate_Enemy = Instantiate(enemyReference[i], StartPoint, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
                    Instantiate_Enemy.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);

                    CurrentEnemyWave.Add(Instantiate_Enemy);
                }

            }
        }

        public void Destructor()
        {
            CurrentEnemyWave.Clear();
            Destroy(this);
        }
    }
}
