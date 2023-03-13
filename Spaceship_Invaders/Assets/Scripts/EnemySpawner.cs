using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] enemyReference;

        private GameObject spawnedEnemy;

        [SerializeField]
        private Transform leftSide, rightSide;

        private int randomIndex;
        private int randomSide;
    }
}
