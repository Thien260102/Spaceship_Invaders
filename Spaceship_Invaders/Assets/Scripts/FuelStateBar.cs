using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class FuelStateBar : MonoBehaviour
    {
        static FuelStateBar instance = null;
        public static FuelStateBar Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [SerializeField]
        Player player;

        [SerializeField]
        List<GameObject> Levels;

        int level = 0;
        public int Level
        {
            get { return level; }
            set
            {
                if (level != value)
                {
                    level = value > Levels.Count ? Levels.Count: value;
                    RenderNewState();

                    if (level <= 0)
                    {
                        player.IsDeleted = true;
                        StartCoroutine(player.Destroyed());

                        level = Levels.Count;
                    }
                }
            }
        }

        float distance = 0;
        public float Distance 
        {
            get { return distance; }
            set 
            {
                distance = value;

                if (distance >= 100)
                {
                    Level = Level <= 0 ? 0 : Level - 1;
                    distance = 0;
                }
            }
        }

        float DeltaTime = 0;

        private void Start()
        {
            if (instance == null)
                instance = this;

            level = 9;
            RenderNewState();
        }

        private void Update()
        {
            if (level <= 2)
                RenderWarning();
        }

        public void RenderNewState()
        {

            for (int i = 0; i < level; i++)
                Levels[i].SetActive(true);

            for (int i = level; i < Levels.Count; i++)
                Levels[i].SetActive(false);

        }

        public void RenderWarning()
        {
            DeltaTime += Time.deltaTime;

            if(DeltaTime >= 0.2f)
            {

                if (Random.Range(0, 7) % 2 == 1)
                {
                    for (int i = 0; i < level; i++)
                        Levels[i].SetActive(true);
                }
                else
                {
                    for (int i = 0; i < level; i++)
                        Levels[i].SetActive(false);
                }

                DeltaTime = 0;
            }
        }

    }
}
