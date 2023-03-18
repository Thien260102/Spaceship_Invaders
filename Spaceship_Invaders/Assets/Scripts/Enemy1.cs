using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy1 : MonoBehaviour, IEnemy
{
    float DeltaTime;
    public GameObject bullet;
    public Rigidbody2D Body;

    public int HP
    {
        get { return HP; }
        set { }
    }

    void Awake()
    {
        HP = Variables.HP_Enemy1;
        Body = GetComponent<Rigidbody2D>();
        DeltaTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (DeltaTime < 1)
            DeltaTime += Time.deltaTime;
        else
        {
            DeltaTime = 0;

            Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust);
            GameObject Instantiate_Bullet = Instantiate(bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
            Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
        }
    }

    public void setPosition(Vector2 position)
    {
        Body.position = position;
    }
}
