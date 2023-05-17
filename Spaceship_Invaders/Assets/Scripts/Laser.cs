using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Laser : Weapon
{
    LineRenderer laserLine;
    Animator animator;
    LayerMask ignoreLayer;

    public GameObject laserSpawnPoint;

    float length = 0f;

    public float maxLength;

    public int Type { get; set; }

    public int Damage { get; set; }

    public GameObject Explosion;



    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        laserLine = this.GetComponent<LineRenderer>();
        animator = this.GetComponent<Animator>();
        Init(Variables.ByPlayer);


        Debug.Log("Laser shoot");
    }

    public void Init(int type = 10, int damage = Variables.Damage_Laser_Default)
    {
        Type = type;
        Damage = damage;
        ignoreLayer = ((1 << 6) | (1 << 7) | (1 << 8));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D laserhit = Physics2D.Raycast(laserSpawnPoint.transform.position, Vector2.up, 20f, ~ignoreLayer);
        if (laserhit.collider == null)
            length = maxLength; 
        else 
            length = Mathf.Clamp(Vector2.Distance(laserSpawnPoint.transform.position, laserhit.point), 0f, 20f);

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
        RaycastHit2D laserhit = Physics2D.Raycast(laserSpawnPoint.transform.position, Vector2.up, 20f, ~ignoreLayer);
        if (laserhit.collider != null)
        {
            Entity entity = laserhit.collider.gameObject.GetComponent<Entity>();
            if (entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.ASTEROID:
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
