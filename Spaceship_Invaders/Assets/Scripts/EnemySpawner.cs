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

        private void Start()
        {
            StartCoroutine(SpawnEnemies(Variables.Enemy1, 1));
        }

        private IEnumerator SpawnEnemies(int Type = 0, int second = 2)
        {
            yield return new WaitForSeconds(second);

            Vector2 position = new Vector2(0, 0);
            GameObject Instantiate_Bullet = Instantiate(enemyReference[Type], position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
            Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            Debug.Log("Spawned enemy");
        }
    }
}
