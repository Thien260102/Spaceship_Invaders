using UnityEngine;

namespace Assets.Scripts
{
    public class Item : Entity
    {
        [SerializeField]
        public Variables.ItemType Type;

        private void Start()
        {
            ID = Variables.ITEM;
        }

        private void Update()
        {
            Vector2 Position = Body.position;

            Position.y -= Variables.ItemSpeed * Time.deltaTime;
            if(Type == Variables.ItemType.Star)
                Position.y -= Variables.ItemSpeed * Time.deltaTime;

            Body.position = Position;
        }
    }
}
