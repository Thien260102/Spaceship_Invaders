using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BulletCollision : MonoBehaviour
{
    public Camera mainCamera;
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
        Bullet_Rigidbody.AddForce(new Vector2(0.0f, Variables.BulletSpeed), ForceMode2D.Impulse);

        float halfHeight = mainCamera.orthographicSize;

        Vector2 BulletPosition = this.transform.position;
        
        //bullet out of screen, so delete it.
        if (BulletPosition.y > halfHeight)
        {
            Debug.Log("Bullet out of screen");
            Destroy(Bullet);
        }

        Debug.Log("update");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Enemy")
        {
            Instantiate(Explosion, collision.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

            Destroy(collision.gameObject);
            Destroy(Bullet);
            Debug.Log("Collision");
        }
    }

    private void ExplosionStart()
    {
        Invoke("DestroyObjects", Variables.ExplosionTime);
    }

    private void DestroyObjects()
    {
        Destroy(Explosion);
    }
}
