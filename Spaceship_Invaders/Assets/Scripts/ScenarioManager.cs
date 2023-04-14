using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] List<Wave> waves;

        int waveIndex, spawnSequenceIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnWave(waves[0]));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator SpawnWave(Wave wave)
        {
            int index = 0;
            while (index < wave.spawnSequences.Count)
            {
                Debug.Log("Spawning Wave");
                StartCoroutine(SpawnSpawnSequence(wave.spawnSequences[index], index));
                index++;
                if (index >= wave.spawnSequences.Count)
                {
                    yield return new WaitForSeconds(0);
                }
                else while (wave.spawnSequences[index].WaitType == WaitBetweenSequenceType.Parallel)
                    {
                        StartCoroutine(SpawnSpawnSequence(wave.spawnSequences[index], index));
                        index++;
                        if (index >= wave.spawnSequences.Count)
                        {
                            
                        }
                    }
                yield return new WaitForSeconds(wave.spawnSequences[index].delayPostSequence);
            }
        }

        public IEnumerator SpawnSpawnSequence(SpawnSequence spawnSequence, int i)
        {
            Debug.Log("Spawning Sequence: " + i.ToString());
            foreach (EnemySpawnInfo enemySpawnInfo in spawnSequence.enemySpawnInfos)
            {
                StartCoroutine(SpawnEnemies(enemySpawnInfo, spawnSequence.path));
            }
            yield return new WaitForSeconds(0);
        }

        public IEnumerator SpawnEnemies(EnemySpawnInfo enemySpawnInfo, Path path)
        {
            Debug.Log("CCCC");
            for (int i = 0; i < enemySpawnInfo.quantity; i++)
            {
                Test_Enemy Instantiate_Enemy = Instantiate(enemySpawnInfo.enemy, path.GetNodePosition(0), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                Instantiate_Enemy.path = path;
                yield return new WaitForSeconds(enemySpawnInfo.delayBetweenSpawn);
            }
        }
    }
}
