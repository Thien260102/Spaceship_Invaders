using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    //Does this sequence happens parallel with the previous sequence or after?
    public enum WaitBetweenSequenceType
    {
        Continous,
        Parallel
    }

    [System.Serializable]
    public class EnemySpawnInfo
    {
        [SerializeField] public Test_Enemy enemy;
        [SerializeField] public uint quantity;
        [SerializeField] public float delayBetweenSpawn; // delay between spawning each enemy
    }

    [System.Serializable]
    public class SpawnSequence
    {
        [SerializeField] public Path path;
        [SerializeField] public EnemySpawnInfo[] enemySpawnInfos;
        //Does this sequence happens parallel with the previous sequence or after?
        [SerializeField] public WaitBetweenSequenceType WaitType;
        [SerializeField] public float delayPostSequence;
    }

    public class Wave : MonoBehaviour
    {
        [SerializeField]
        public List<SpawnSequence> spawnSequences;
    }
}