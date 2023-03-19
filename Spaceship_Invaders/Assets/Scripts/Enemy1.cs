using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy1 : MonoBehaviour, IEnemy
{
    float DeltaTime;
    float maxTime;
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
        maxTime = Random.Range(5, 10);
    }
    // Update is called once per frame
    void Update()
    {
        if (DeltaTime < maxTime)
            DeltaTime += Time.deltaTime;
        else
        {
            DeltaTime = 0;
            Shooting();
            maxTime = Random.Range(5, 10);
        }
    }

    private void Shooting()
    {
        Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust);
        GameObject Instantiate_Bullet = Instantiate(bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
    }

}
