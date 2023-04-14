using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Test_Enemy : Enemy
{
    float DeltaTime;
    float maxTime;
    public Path path;
    int nextDestinationNode;

    void Awake()
    {
        this.Init();
        HP = Variables.HP_Enemy1;

        DeltaTime = 0;
        maxTime = Random.Range(2, 10);
        Body = GetComponent<Rigidbody2D>();
        nextDestinationNode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
            IsDeleted = true;

        if (DeltaTime < maxTime)
            DeltaTime += Time.deltaTime;
        else
        {
            DeltaTime = 0;
            Shooting();
            maxTime = Random.Range(5, 10);
        }

        if (nextDestinationNode < path.NodeCount())
        {
            Movement();
        }
    }

    private void Shooting()
    {
        Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust);
        Bullet Instantiate_Bullet = Instantiate(bullet as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;

        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
        Instantiate_Bullet.Init(Variables.ByEnemy);
    }

    private void Movement()
    {
        Vector2 destination = new Vector2(path.GetNodePosition(nextDestinationNode).x, path.GetNodePosition(nextDestinationNode).y);
        Body.position = Vector2.Lerp(Body.position, destination, Variables.EnemyFlySpeed * Time.deltaTime);

        if (Vector2.Distance(Body.position, destination) < 3f)
        {
            nextDestinationNode++;
        }
    }
}
