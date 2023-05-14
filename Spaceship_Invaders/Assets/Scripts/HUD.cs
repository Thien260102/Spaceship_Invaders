using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    public class HUD: MonoBehaviour
    {
        static HUD instance = null;

        public static HUD Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        public TMP_Text Scores;
        public TMP_Text Lifes;

        int score = 0;
        public int Score
        {
            get { return score; }
            set
            {
                if (score != value)
                {
                    score = value;
                    Scores.text = string.Format("{0: 000,000}", score);
                }
            }
        }

        int life = 3;
        public int Life
        {
            get { return life; }
            set
            {
                if (life != value)
                {
                    life = value;
                    Lifes.text = life.ToString();
                }
            }
        }

        int boom = 0;
        public int Boom
        {
            get { return boom; }
            set
            {
                if (boom != value)
                {
                    boom = value;
                    
                }
            }
        }

        // Get score and Life, ...
        private void Start()
        {
            if (instance == null) 
                instance = this;

        }

    }
}
