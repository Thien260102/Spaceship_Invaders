using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Mathematics
    {
        private static Mathematics instance;

        public static Mathematics Instance
        {
            get { if (instance == null) instance = new Mathematics(); return instance; }
            private set { instance = value; }
        }

        public Vector2 ReciprocatingMovement(Vector2 current_pos, float time)
        {
            return new Vector2();
        }

    }
}
