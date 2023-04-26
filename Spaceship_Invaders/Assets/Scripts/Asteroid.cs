using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroid: Entity
    {
        public Asteroid Medium;
        public Asteroid Small;

        public Level level;

        public Path path;
        int nextDestinationNode;

        public enum Level{
            Large,
            Medium,
            Small
        }

        private void Start()
        {
            Body = GetComponent<Rigidbody2D>();
            
            ID = Variables.ENEMY;
            nextDestinationNode = 1;
            
            //init HP
            switch (level)
            {
                case Level.Large:
                    HP = 200;
                    break;

                case Level.Medium:
                    HP = 100;
                    break;

                default:
                    HP = 50;
                    break;
            }
        }

        private void Update()
        {
            if(HP <= 0)
            {
                MyDestroy();
            }
            
            if (path != null && nextDestinationNode < path.NodeCount())
            {
                Movement();
            }
        }

        void Movement()
        {
            Vector2 destination = new Vector2(path.GetNodePosition(nextDestinationNode).x, path.GetNodePosition(nextDestinationNode).y);
            Body.position = Vector2.Lerp(Body.position, destination, 0.1f * Time.deltaTime);

            if (Vector2.Distance(Body.position, destination) < 3f)
            {
                nextDestinationNode++;
            }
        }

        private void MyDestroy()
        {
            Vector3 pos = Body.position;

            Destroy(gameObject);
            
            switch (level)
            {
                case Level.Large:

                    float radius = Vector3.Distance(pos, this.path.GetNodePosition(1));
                    Vector2 position = new Vector2(pos.x + 1, pos.y);
                    Asteroid asteroid = Instantiate(Medium, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    Path newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    var vector2 = Random.insideUnitCircle.normalized * radius;
                    newPath.AddNode(new Vector3(vector2.x, vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Medium;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;

                    

                    asteroid = Instantiate(Medium, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    newPath.AddNode(new Vector3(vector2.x, -vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Medium;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;

                    break;

                case Level.Medium:
                    radius = Vector3.Distance(pos, this.path.GetNodePosition(1));
                    position = new Vector2(pos.x + 1, pos.y);
                    asteroid = Instantiate(Small, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    vector2 = Random.insideUnitCircle.normalized * radius;
                    newPath.AddNode(new Vector3(vector2.x, vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Small;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;



                    asteroid = Instantiate(Small, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    newPath = Instantiate(path, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newPath.Clear();

                    newPath.AddNode(pos);

                    newPath.AddNode(new Vector3(vector2.x, -vector2.y, 0));

                    asteroid.path = newPath;
                    asteroid.level = Level.Small;
                    asteroid.Medium = Medium;
                    asteroid.Small = Small;


                    break;
            }

        }

    }
}
