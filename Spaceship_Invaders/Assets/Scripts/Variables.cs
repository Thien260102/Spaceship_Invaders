﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Variables
    {
        public const int PLAYER = 0;
        public const int ENEMY = 1;
        public const int BULLET = 2;

        public const float ScreenHeight = 16.0f;
        public const float ScreenWidth = 28.4f;

        public const float Adjust = 0.5f;

        #region Player

        public const float PlayerBulletSpeed = 15.0f;

        // state
        public const int Player_IDLE = 0;
        public const int Player_MOVE = 1;
        public const int Player_BOOST = 2;
        public const int Player_DESTROYED = 4;

        public const float Player_SPEED_BOOST = 1.0f;

        #endregion


        #region Enemy

        public const float EnemyBulletSpeed = 5.0f;

        public const int HP_Enemy1 = 100;
        public const int HP_Enemy2 = 500;
        public const int HP_Enemy3 = 5000;
        public const int HP_Enemy4 = 10000;
        public const int HP_Enemy5 = 20000;
        public const int HP_Enemy6 = 200000;


        public const int Enemy1 = 0;
        public const int Enemy2 = 1;
        public const int Enemy3 = 2;
        public const int Enemy4 = 3;
        public const int Enemy5 = 4;
        public const int Enemy6 = 5;

        #endregion


        #region Bullet
        public const int ByPlayer = 10;
        public const int ByEnemy = 20;

        public const int DAMAGE_Bullet1 = 50;
        public const int Damage_Bullet_Default = 50;

        #endregion



        public const float ExplosionTime = 0.4f;


        public const float EnemyFlySpeed = 1.0f;
    }
}
