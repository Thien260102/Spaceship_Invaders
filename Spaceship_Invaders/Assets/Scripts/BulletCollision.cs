using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BulletCollision : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D Bullet_Rigidbody;
    public GameObject Bullet;
    public GameObject Explosion;

    void Start()
    {
        mainCamera = Camera.main;
        Bullet_Rigidbody = Bullet.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(Bullet.tag)
        {
            case "PlayerBullet":
                Bullet_Rigidbody.AddForce(new Vector2(0.0f, Variables.PlayerBulletSpeed), ForceMode2D.Impulse);
                break;

            default:
                Bullet_Rigidbody.AddForce(new Vector2(0.0f, -Variables.EnemyBulletSpeed), ForceMode2D.Impulse);
                break;
        }
        

        float halfHeight = Variables.ScreenHeight / 2;

        Vector2 BulletPosition = this.transform.position;
        
        //bullet out of screen, so delete it.
        if (BulletPosition.y > halfHeight || BulletPosition.y < -halfHeight)
        {
            Debug.Log("Bullet out of screen");
            Destroy(Bullet);
        }

        Debug.Log("update");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        switch(obj.tag)
        {
            case "Enemy":
                if(Bullet.tag == "PlayerBullet")
                {
                    Instantiate(Explosion, collision.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    collision.gameObject.SetActive(false);
                    Destroy(Bullet);
                    Debug.Log("Collision");
                }
                break;

            case "Player":
                if(Bullet.tag != "PlayerBullet")
                {
                    Instantiate(Explosion, collision.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                    Destroy(collision.gameObject);
                    Destroy(Bullet);
                    Debug.Log("GameOver");
                }
                break;
        }

    }
}
