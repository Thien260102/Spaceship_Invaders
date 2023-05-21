using UnityEngine;

namespace Assets.Scripts
{
    public class RageSkill : Skill
    {

        public void DivineDeparture(Vector2 direction)
        {
            this.Init(Type, Variables.Damage_Bullet_Default, direction);
            this.isMovable = false;
        }

        public override void HandleDestroy()
        {
            SkillManager.Instance.EnergyWave(Type, gameObject.transform.position, Direction);

            Destroy(gameObject);
        }
        
    }
}
    

