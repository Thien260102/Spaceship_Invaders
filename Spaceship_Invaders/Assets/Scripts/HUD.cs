﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    public class HUD: MonoBehaviour
    {
        static HUD instance = null;

        public GameObject floatingText;
        public GameObject Canvas;

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

        public void DisplayFloatingText(string displayText, Vector2 position)
        {
            int randomCoorX = Random.Range(1, 9);
            int randomCoorY = Random.Range(1, 9);
            Vector2 location = Camera.main.WorldToScreenPoint(position);
            location.x += -0.5f + randomCoorX * 0.1f;
            location.y += -0.5f + randomCoorY * 0.1f;
            GameObject text = Instantiate(floatingText, location, new Quaternion(0,0,0,0));
            text.transform.SetParent(Canvas.transform);
            text.GetComponentInChildren<Text>().text = displayText;
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
