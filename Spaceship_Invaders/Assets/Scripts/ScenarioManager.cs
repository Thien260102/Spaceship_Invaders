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

        List<Entity> EnemiesOrAsteroid = new List<Entity>();

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
            foreach (Entity e in EnemiesOrAsteroid.ToArray())
            {
                if (e.IsDeleted)
                {
                    EnemiesOrAsteroid.Remove(e);

                    if (e.HP <= 0)
                    {

                        if (e is Asteroid)
                        {
                            Asteroid asteroid = ((Asteroid)e);

                            asteroid.MyDestroy(EnemiesOrAsteroid);

                            switch (asteroid.level)
                            {
                                case Asteroid.Level.Large:
                                    HUD.Instance.Score += 3000;
                                    HUD.Instance.DisplayFloatingText("3000", e.transform.position);
                                    break;

                                case Asteroid.Level.Medium:
                                    HUD.Instance.Score += 2000;
                                    HUD.Instance.DisplayFloatingText("2000", e.transform.position);
                                    break;

                                default:
                                    HUD.Instance.Score += 1000;
                                    HUD.Instance.DisplayFloatingText("1000", e.transform.position);
                                    break;
                            }
                        }
                        else if (e is Enemy1)
                        {
                            HUD.Instance.Score += 1000;
                            HUD.Instance.DisplayFloatingText("1000", e.transform.position);
                        }
                        else if (e is Enemy2)
                        {
                            HUD.Instance.Score += 2000;
                            HUD.Instance.DisplayFloatingText("2000", e.transform.position);
                        }
                        else if (e is Enemy3)
                        {
                            HUD.Instance.Score += 5000;
                            HUD.Instance.DisplayFloatingText("5000", e.transform.position);
                        }
                        else if (e is Boss)
                        {
                            HUD.Instance.Score += 50000;
                            HUD.Instance.DisplayFloatingText("50000", e.transform.position);
                        }

                        ItemManager.Instance.RandomItem(e.transform.position);
                    }

                    e.Destructor();
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
                while (!waveDoneSpawning || EnemiesOrAsteroid.Count != 0)
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
                    //yield return new WaitForSeconds(wave.spawnSequences[index].delayPostSequence);

                    while (index < wave.spawnSequences.Count &&
                        wave.spawnSequences[index].WaitType == WaitBetweenSequenceType.Parallel)
                    {
                        yield return new WaitForSeconds(wave.spawnSequences[index].delayPostSequence);
                        StartCoroutine(SpawnSpawnSequence(wave.spawnSequences[index], index));
                        index++;
                    }

                    if (EnemiesOrAsteroid.Count == 0 && index < wave.spawnSequences.Count)
                    {
                        yield return new WaitForSeconds(wave.spawnSequences[index].delayPostSequence);
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
            if ((enemySpawnInfo.entity as Enemy) != null)
            {
                for (int i = 0; i < enemySpawnInfo.quantity; i++)
                {
                    Enemy Instantiate_Enemy = Instantiate(enemySpawnInfo.entity, path.GetNodePosition(0), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Enemy;
                    Instantiate_Enemy.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);

                    Instantiate_Enemy.path = path;
                    Instantiate_Enemy.OrbitPath = orbitPath;
                    EnemiesOrAsteroid.Add(Instantiate_Enemy);
                    yield return new WaitForSeconds(enemySpawnInfo.delayBetweenSpawn);
                }
            }
            else
            {
                for (int i = 0; i < enemySpawnInfo.quantity; i++)
                {
                    Vector3 position = path.GetNodePosition(0);
                    position.x += (i * Random.Range(1f, 5f));
                    position.y += (i * Random.Range(1f, 5f));

                    Asteroid Instantiate_Asteroid = Instantiate(enemySpawnInfo.entity, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Asteroid;
                    Instantiate_Asteroid.path = path;
                    EnemiesOrAsteroid.Add(Instantiate_Asteroid);
                    yield return new WaitForSeconds(enemySpawnInfo.delayBetweenSpawn);
                }
            }


        }
    }
}