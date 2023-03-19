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

        EnemyWave enemyWave;

        int currentWave = 0;

        private void StartWave()
        {
            if(ListWave.Length > currentWave)
            {
                enemyWave = ListWave[currentWave++].GetComponent<EnemyWave>();

                SpawnEnemies();
            }
        }

        private void FixedUpdate()
        {
            if(enemyWave != null)
            {
                if (enemyWave.HasEnemies() == false)
                    enemyWave.Destructor();
                else
                    enemyWave.HandleUpdate();

            }
            else
                StartWave();
             
        }

        private void SpawnEnemies(int second = 2)
        {
            enemyWave.InitEnemyWave(enemyReference);
            
            Debug.Log("Spawned enemy");
        }
    
    }
}
