using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] List<Wave> waves;

        int waveIndex;// spawnSequenceIndex = 0;

        bool waveDoneSpawning;

        List<Enemy> Enemies = new List<Enemy>();

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Scenario());
        }

        // Update is called once per frame
        void Update()
        {
            PurgeDeletedObjects();
        }

        private void PurgeDeletedObjects()
        {
            foreach (Enemy e in Enemies.ToArray())
            {
                if (e.IsDeleted)
                {
                    Enemies.Remove(e);
                    e.Destructor();
                    Debug.Log("Delete");
                }
            }
        }

        public IEnumerator Scenario()
        {
            while (waveIndex < waves.Count)
            {
                waveDoneSpawning = false;
                yield return new WaitForSeconds(waves[waveIndex].delayBeforeSpawn);
                Debug.Log("Spawning wave: " + waveIndex.ToString());
                StartCoroutine(SpawnWave(waves[waveIndex]));

                //wait until wave down spawning and all enemies are dead, check every 0.2 sec
                while (!waveDoneSpawning || Enemies.Count != 0)
                {
                    yield return new WaitForSeconds(0.2f);
                }

                waveDoneSpawning = false;
                waveIndex++;
            }
            //level is done, do stuff
            Debug.Log("level complete ");
        }

        public IEnumerator SpawnWave(Wave wave)
        {
            if (wave.spawnSequences.Count > 0)
            {
                int index = 0;

                StartCoroutine(SpawnSpawnSequence(wave.spawnSequences[index], index));
                index++;

                while (index < wave.spawnSequences.Count)
                {
                    yield return new WaitForSeconds(wave.spawnSequences[index].delayPostSequence);

                    while (index < wave.spawnSequences.Count &&
                        wave.spawnSequences[index].WaitType == WaitBetweenSequenceType.Parallel)
                    {
                        StartCoroutine(SpawnSpawnSequence(wave.spawnSequences[index], index));
                        index++;
                    }

                    if (Enemies.Count == 0 && index < wave.spawnSequences.Count)
                    {
                        StartCoroutine(SpawnSpawnSequence(wave.spawnSequences[index], index));
                        index++;
                    }

                    
                }
            }
            
            waveDoneSpawning = true;
        }

        public IEnumerator SpawnSpawnSequence(SpawnSequence spawnSequence, int i)
        {
            Debug.Log("Spawning Sequence: " + i.ToString());
            foreach (EnemySpawnInfo enemySpawnInfo in spawnSequence.enemySpawnInfos)
            {
                StartCoroutine(SpawnEnemies(enemySpawnInfo, spawnSequence.path, spawnSequence.OrbitPath));
            }
            yield return new WaitForSeconds(0);
        }

        public IEnumerator SpawnEnemies(EnemySpawnInfo enemySpawnInfo, Path path, Path orbitPath)
        {
            for (int i = 0; i < enemySpawnInfo.quantity; i++)
            {
                Enemy Instantiate_Enemy = Instantiate(enemySpawnInfo.enemy, path.GetNodePosition(0), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                Instantiate_Enemy.path = path;
                Instantiate_Enemy.OrbitPath = orbitPath;
                Enemies.Add(Instantiate_Enemy);
                yield return new WaitForSeconds(enemySpawnInfo.delayBetweenSpawn);
            }
        }
    }
}