using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Gun : Weapon
{
    public Bullet Bullet;
    public GameObject MissleSpawnPoint1;
    public GameObject MissleSpawnPoint2;

    public override void Shoot()
    {
        Bullet Instantiate_Bullet = Instantiate(Bullet, MissleSpawnPoint1.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.Init(Variables.ByPlayer);

        Instantiate_Bullet = Instantiate(Bullet, MissleSpawnPoint2.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.Init(Variables.ByPlayer);
    }
}
