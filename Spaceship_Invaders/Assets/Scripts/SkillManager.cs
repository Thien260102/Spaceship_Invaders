using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SkillManager : MonoBehaviour
    {
        static SkillManager instance;

        public static SkillManager Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [SerializeField]
        List<Skill> Skills;

        private void Start()
        {
            if (instance == null)
                instance = this;
        }

        public void CircleShoot(int quantity, Vector2 position)
        {
            if(Skills.Count > 0)
            {
                Skill Instantiate_Skill = Instantiate(Skills[0] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;

                ((BeautifulShapeSkill)Instantiate_Skill).Circle(quantity, position);

            }
        }


    }
}
