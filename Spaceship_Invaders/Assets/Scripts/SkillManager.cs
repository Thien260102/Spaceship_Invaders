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

        public void CircleShoot(int Type, int quantity, Vector2 position, Vector2 direction)
        {
            if(Skills.Count > 0)
            {
                BeautifulShapeSkill Instantiate_Skill = Instantiate(Skills[0] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as BeautifulShapeSkill;

                Instantiate_Skill.Type = Type;
                Instantiate_Skill.Circle(quantity, position, direction);
            }
        }

        public void DivineDeparture(int Type, Vector2 position, Vector2 direction)
        {
            if (Skills.Count > 1) 
            {
                RageSkill Instantiate_Skill = Instantiate(Skills[1] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as RageSkill;
                Instantiate_Skill.Type = Type;
                Instantiate_Skill.DivineDeparture(direction);
            }
        }

        public void EnergyWave(int Type, Vector2 position, Vector2 direction)
        {
            if (Skills.Count > 2)
            {
                Skill Instantiate_Skill = Instantiate(Skills[2] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;

                Instantiate_Skill.Init(Type, Variables.Damage_Bullet_Default, direction);
            }
        }

        public void Invincible(int Type, Vector2 position, Vector2 direction, GameObject parent)
        {
            if(Skills.Count > 3)
            {
                Skill Instantiate_Skill = Instantiate(Skills[3] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;

                Instantiate_Skill.isMovable = false;
                Instantiate_Skill.SetParent(parent);
                Instantiate_Skill.Init(Type, Variables.Damage_Bullet_Default, direction);

            }
        }

    }
}
