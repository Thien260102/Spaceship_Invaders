using UnityEngine;

namespace Assets.Scripts
{
    public class BeautifulShapeSkill : Skill
    {

        public void Circle(int quantity, Vector2 position, Vector2 direction)
        {
            float angle = 360.0f / quantity;
            Vector2 xAxis = new Vector2(1, 0);
            this.Init(Type, Damage, direction);

            float originalAngle = Vector3.Angle(direction, xAxis);

            BeautifulShapeSkill skill = gameObject.GetComponent<BeautifulShapeSkill>();
            
            for (int i = 1; i <= quantity; i++)
            {
                Skill Instantiate_Skill = Instantiate(skill as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;
                
                direction = Quaternion.Euler(0, 0, angle * i + originalAngle) * xAxis;

                Instantiate_Skill.Init(Type, Damage, direction);
            }
        }

    }
}
