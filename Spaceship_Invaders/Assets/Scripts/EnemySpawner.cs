using System;
using System.Collections;
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

        private IEnumerator SpawnEnemies(int Type = 0, int second = 2)
        {

            for(int i = 0; i < enemyWave.CorrespondingEnemyQuantity.Length; i++)
            {
                yield return new WaitForSeconds(second);

                for(int j = 0; j < enemyWave.CorrespondingEnemyQuantity[i]; j++)
                {
                    Vector2 position = new Vector2(-j, i);
                    GameObject Instantiate_Enemy = Instantiate(enemyReference[i], position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
                    Instantiate_Enemy.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
                
            }
            
            Debug.Log("Spawned enemy");
        }
    }
}
