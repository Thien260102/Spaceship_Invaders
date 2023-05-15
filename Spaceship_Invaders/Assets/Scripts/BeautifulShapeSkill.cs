using UnityEngine;

namespace Assets.Scripts
{
    public class BeautifulShapeSkill : Skill
    {

        public void Circle(int quantity, Vector2 position)
        {
            float angle = 360.0f / quantity;
            Vector2 xAxis = new Vector2(1, 0);
            Vector2 direction = Quaternion.Euler(0, 0, angle - 90) * xAxis;
            this.Init(Variables.ByEnemy, Variables.Damage_Bullet_Default, direction);

            BeautifulShapeSkill skill = gameObject.GetComponent<BeautifulShapeSkill>();
            
            for (int i = 2; i <= quantity; i++)
            {
                Skill Instantiate_Skill = Instantiate(skill as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;
                
                direction = Quaternion.Euler(0, 0, angle * i - 90) * xAxis;

                if(Instantiate_Skill != null)
                    ((Skill)Instantiate_Skill).Init(Variables.ByEnemy, Variables.Damage_Bullet_Default, direction);
            }
        }

    }
}
