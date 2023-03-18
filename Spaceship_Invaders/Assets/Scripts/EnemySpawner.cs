using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] enemyReference;

        [SerializeField]
        public GameObject[] ListWave;

        private List<GameObject> CurrentEnemyWave = null;
        EnemyWave enemyWave;

        int currentWave = 0;

        private void Start()
        {
            if(ListWave.Length > currentWave)
            {
                enemyWave = ListWave[currentWave++].GetComponent<EnemyWave>();
            }
            StartCoroutine(SpawnEnemies(Variables.Enemy1, 1));
        }

        private void FixedUpdate()
        {
            if(CurrentEnemyWave != null && CurrentEnemyWave.Count > 0)
            {
                float direction = 1.0f;
                if (CurrentEnemyWave[CurrentEnemyWave.Count - 1].transform.position.x > Variables.ScreenWidth / 2)
                    direction = -1.0f;

                Vector2 position;
                foreach (GameObject e in CurrentEnemyWave)
                {
                    position = e.transform.position;
                    position.x += direction * 0.01f;

                    e.transform.position = position;
                }
                PurgeDeletedObjects();
            }
        }

        private IEnumerator SpawnEnemies(int Type = 0, int second = 2)
        {
            CurrentEnemyWave = new List<GameObject>();

            for(int i = 0; i < enemyWave.CorrespondingEnemyQuantity.Length; i++)
            {
                yield return new WaitForSeconds(second);

                for(int j = 0; j < enemyWave.CorrespondingEnemyQuantity[i]; j++)
                {
                    Vector2 position = new Vector2(-j, i);
                    GameObject Instantiate_Enemy = Instantiate(enemyReference[i], position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
                    Instantiate_Enemy.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);

                    CurrentEnemyWave.Add(Instantiate_Enemy);
                }
                
            }
            
            Debug.Log("Spawned enemy");
        }

        private void PurgeDeletedObjects()
        {
            foreach(GameObject o in CurrentEnemyWave.ToList())
            {
                if(o.activeSelf == false)
                {
                    CurrentEnemyWave.Remove(o);
                    Destroy(o);
                    Debug.Log("Delete");
                }
            }
        }


    }
}
