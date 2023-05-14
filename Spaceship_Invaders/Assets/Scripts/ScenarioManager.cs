using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Awards
    {
        [SerializeField] public Item item;
        [SerializeField] public uint ratio;
    }

    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] List<Wave> waves;
        [SerializeField] List<Awards> awards;

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

                            switch(asteroid.level)
                            {
                                case Asteroid.Level.Large:
                                    HUD.Instance.Score += 3000;
                                    break;

                                case Asteroid.Level.Medium:
                                    HUD.Instance.Score += 2000;
                                    break;

                                default:
                                    HUD.Instance.Score += 1000;
                                    break;
                            }
                        }
                        else if (e is Enemy1)
                            HUD.Instance.Score += 1000;
                        else if (e is Enemy2)
                            HUD.Instance.Score += 2000;
                        else if (e is Enemy3)
                            HUD.Instance.Score += 5000;


                        foreach(var award in awards)
                        {

                            float ratio = Random.Range(0.0f, 1.0f);

                            if (ratio <= award.ratio / 100.0f)
                            {
                                Instantiate(award.item, e.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                            }
                        }
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
                    yield return new WaitForSeconds(wave.spawnSequences[index].delayPostSequence);

                    while (index < wave.spawnSequences.Count &&
                        wave.spawnSequences[index].WaitType == WaitBetweenSequenceType.Parallel)
                    {
                        StartCoroutine(SpawnSpawnSequence(wave.spawnSequences[index], index));
                        index++;
                    }

                    if (EnemiesOrAsteroid.Count == 0 && index < wave.spawnSequences.Count)
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
            if ((enemySpawnInfo.entity as Enemy) != null)
            {
                for (int i = 0; i < enemySpawnInfo.quantity; i++)
                {
                    Enemy Instantiate_Enemy = Instantiate(enemySpawnInfo.entity, path.GetNodePosition(0), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Enemy;
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