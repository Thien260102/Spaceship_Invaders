using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Laser_Tentacle : MonoBehaviour
{
    LineRenderer laserLine;
    LayerMask ignoreLayer;

    float length = 0f;

    public float maxLength;

    public int Type { get; set; }

    public int Damage { get; set; }

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        Type = Variables.ByEnemy;
        Damage = 9999;
        ignoreLayer = ((1 << 6) | (1 << 7) | (1 << 9));

        laserLine = GetComponent<LineRenderer>();
    }

    public void GetHitInfo(Vector3 direction)
    {
        //Debug.Log("Getting hit info");
        //float halfHeight = Variables.ScreenHeight / 2;
        //float maxDis = halfHeight - laserSpawnPoint.transform.position.y;
        RaycastHit2D laserhit = Physics2D.Raycast(this.transform.position, direction, 20f, ~ignoreLayer);

        if (laserhit.collider != null)
        {
            length = Mathf.Clamp(Vector2.Distance(this.transform.position, laserhit.point), 0f, 20f);
            Render(direction);

            Entity entity = laserhit.collider.GetComponent<Entity>();
            if (entity == null && laserhit.collider.GetComponentInParent<Entity>() != null)
            {
                entity = laserhit.collider.GetComponentInParent<Entity>();
            }

            if (entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            Instantiate(Explosion, laserhit.collider.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(9999);
                            Debug.Log("GameOver");
                        }
                        break;
                }
            }
        }
        else
        {
            length = maxLength;

            Render(direction);
        }
    }

    public void Render(Vector2 direction)
    {
        laserLine.enabled = true;
        //Debug.Log("Shooting");
        Vector2 position = this.transform.position;
        laserLine.positionCount = 2;
        laserLine.SetPosition(0, position);
        position += direction.normalized * length;
        laserLine.SetPosition(1, position);
    }

    public void Reset()
    {
        Debug.Log("Done");
        laserLine.enabled = false;
    }
}
