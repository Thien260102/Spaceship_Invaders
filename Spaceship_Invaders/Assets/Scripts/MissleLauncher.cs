using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class MissleLauncher : Weapon
{
    public Missle Missle;
    public GameObject MissleSpawnPoint1;
    public GameObject MissleSpawnPoint2;

    public override void Shoot()
    {
        Missle Instantiate_Missle = Instantiate(Missle, MissleSpawnPoint1.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Missle;

        Instantiate_Missle = Instantiate(Missle, MissleSpawnPoint2.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Missle;
    }
}
