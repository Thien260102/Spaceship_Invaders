using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Tentacle : Entity
{
    [SerializeField]
    protected float MaxTimeRandom;

    [SerializeField]
    float DeltaTime;
    [SerializeField]
    float maxTime;

    public float followSpeed;
    public bool isFollowingPlayer;
    public bool isShooting = false;

    Animator animator;

    [SerializeField] GameObject target;
    [SerializeField] GameObject rootBone;
    [SerializeField] GameObject lastBone;
    [SerializeField] Laser_Tentacle laser;

    Player player;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        ID = Variables.ENEMY;
        isFollowingPlayer = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        DeltaTime = 0;
        maxTime = Random.Range(5, MaxTimeRandom);
        target.transform.position = this.transform.position;

        HP = 1000;

        //rootBone.transform.position = this.transform.position;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction = target.transform.position - this.transform.position;
        direction = lastBone.transform.right;

        if (isFollowingPlayer && player)
        {
            target.transform.position = Vector2.Lerp(target.transform.position, player.Body.position, followSpeed * Time.deltaTime);
        }

        if (HP <= 0)
            IsDeleted = true;

        if (DeltaTime < maxTime)
            DeltaTime += Time.deltaTime;
        else
        {
            DeltaTime = 0;
            Shooting();
        }

        if (isShooting)
        {
            laser.GetHitInfo(direction);
        }
        UpdateStatusEffect();
    }

    void Shooting()
    {
        isShooting = true;
        animator.SetTrigger("Shoot");
    }

    void DoneShooting()
    {
        animator.ResetTrigger("Shoot");
        isShooting = false;
        maxTime = Random.Range(5, MaxTimeRandom);
        laser.Reset();
    }
}
