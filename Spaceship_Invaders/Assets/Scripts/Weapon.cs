using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ID { get; set; } //player, enemies, bullet

    public float RateOfFire; //How fast weapon shoot, shots per minute

    protected float DelayBetweenShots; //Reload between each shot, defined by RateOfFire

    protected float DelayTimer; //timer

    void Start()
    {
        if (RateOfFire != 0)
        {
            DelayBetweenShots = 1.0f / (RateOfFire / 60.0f);
        } else
        {
            DelayBetweenShots = 0.0f;
        }
        DelayTimer = 0.0f;
        Debug.Log(DelayBetweenShots.ToString());
    }

    void Update()
    {
        if (DelayTimer >= 0f)
        {
            DelayTimer -= Time.deltaTime;
        }
    }

    public void Trigger() //Attempt to shoot the weapon
    {
        if (DelayTimer <= 0f)
        {
            Shoot();
            DelayTimer = DelayBetweenShots;
        }
    }

    public virtual void Shoot() //Weapon shooting, spawn bullet and stuff, defined by individual weapon
    {

    }
}
