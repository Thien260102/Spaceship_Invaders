using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public Camera mainCamera;
    public Rigidbody2D bullet;
    // Update is called once per frame
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        bullet.AddForce(new Vector2(0.0f, 0.2f), ForceMode2D.Impulse);
        //bullet.AddForce(new Vector2(0.0f, 0.1f));

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = mainCamera.aspect * halfHeight;

        Vector2 BulletPosition = this.transform.position;
        //if (BulletPosition.x < -halfWidth || BulletPosition.x > halfWidth
        //    || BulletPosition.y < -halfHeight || BulletPosition.y > halfHeight)
        //    this.enabled = false;
        Debug.Log("update");
        
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collision");
    //}
}
