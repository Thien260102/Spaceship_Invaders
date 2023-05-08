using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Laser : Weapon
{
    LineRenderer laserLine;
    Animator animator;

    public GameObject laserSpawnPoint;

    float length = 0f;

    public float maxLength;

    public int Type { get; set; }

    public int Damage { get; set; }

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = this.GetComponent<LineRenderer>();
        animator = this.GetComponent<Animator>();
        Init(Variables.ByPlayer);
    }

    public void Init(int type = 10, int damage = Variables.Damage_Laser_Default)
    {
        Type = type;
        Damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D laserhit = Physics2D.Raycast(laserSpawnPoint.transform.position, Vector2.up);
        if (laserhit.collider == null)
            length = maxLength; else 
            length = Mathf.Clamp(Vector2.Distance(laserSpawnPoint.transform.position, laserhit.point), 0f, 10f);

    }

    public override void Shoot()
    {
        laserLine.SetPosition(0, laserSpawnPoint.transform.position);
        Vector2 newposition = laserSpawnPoint.transform.position;
        newposition.y += length;
        laserLine.SetPosition(1, newposition);

        animator.SetTrigger("Shoot");
    }

    public void GetHitInfo()
    {
        Debug.Log("Getting hit info");
        float halfHeight = Variables.ScreenHeight / 2;
        float maxDis = halfHeight - laserSpawnPoint.transform.position.y;
        RaycastHit2D laserhit = Physics2D.Raycast(laserSpawnPoint.transform.position, Vector2.up, 10f);
        if (laserhit.collider != null)
        {
            Entity entity = laserhit.collider.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                switch (entity.ID)
                {
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            Instantiate(Explosion, laserhit.collider.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(Damage);
                            Debug.Log("LaserHit");
                        }
                        break;

                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            Instantiate(Explosion, laserhit.collider.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.IsDeleted = true;
                            Debug.Log("GameOver");
                        }
                        break;
                }
            }
        }
    }
}
