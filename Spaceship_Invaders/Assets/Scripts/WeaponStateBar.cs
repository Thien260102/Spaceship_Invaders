using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class WeaponStateBar : MonoBehaviour
    {
        static WeaponStateBar instance = null;
        public static WeaponStateBar Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        public List<GameObject> Levels = null;
        public List<GameObject> Types = null;

        int level = 0;
        public int Level
        {
            get { return level; }
            set 
            {
                if(level != value)
                {
                    level = value;
                    RenderNewState();
                }
            }
        }

        int type = 0;
        public int Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    RenderNewState();
                }
            }
        }

        private void Start()
        {
            if (instance == null) 
                instance = this;

            level = 1;
            type = 0;
            
            RenderNewState();
        }




        public void RenderNewState()
        {
            
            for (int i = 0; i < (level * 3) && i < Levels.Count; i++)
                Levels[i].SetActive(true);

            for (int i = level * 3; i < Levels.Count; i++)
                Levels[i].SetActive(false);


            foreach (var e in Types)
                e.SetActive(false);
            Types[type].SetActive(true);
        }
    }
}
