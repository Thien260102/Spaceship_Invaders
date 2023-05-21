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
        public TMP_Text Coins;

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

        int coin = 0;
        public int Coin
        {
            get { return coin; }
            set
            {
                if (coin != value)
                {
                    coin = value;
                    Coins.text = coin.ToString();
                }
            }
        }

        // Get score and Life, ... from file
        private void Start()
        {
            if (instance == null) 
                instance = this;
            Life = 30;
        }

    }
}
