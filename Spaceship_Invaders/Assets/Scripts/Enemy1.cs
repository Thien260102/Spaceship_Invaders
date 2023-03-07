using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy1 : MonoBehaviour
{
    float DeltaTime;
    public GameObject bullet;
    int HP;
    public Rigidbody2D Body;
    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        HP = Variables.HP_Enemy1;
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
