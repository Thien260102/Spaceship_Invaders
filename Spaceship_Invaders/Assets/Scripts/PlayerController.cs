using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;
    public Camera mainCamera;
    public GameObject bullet;
    // Update is called once per frame
    void Update()
    {
        MouseController();
    }

    void MouseController()
    {
        //get mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = mainCamera.aspect * halfHeight;

        // limit moving area of player
        if (mousePosition.x < -halfWidth + Variables.Adjust)
            mousePosition.x = -halfWidth + Variables.Adjust;
        else if (mousePosition.x > halfWidth - Variables.Adjust)
            mousePosition.x = halfWidth - Variables.Adjust;
        if (mousePosition.y < -halfHeight + Variables.Adjust)
            mousePosition.y = -halfHeight + Variables.Adjust;
        else if (mousePosition.y > halfHeight - Variables.Adjust)
            mousePosition.y = halfHeight - Variables.Adjust;
        player.position = mousePosition;

        // pressed mouse left
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = new Vector2(player.position.x, player.position.y + 1);
            GameObject Instantiate_Bullet = Instantiate(bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
            Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        }


        Cursor.visible = false; // invisible cursor
        Cursor.lockState = CursorLockMode.Confined;// block cursor into Game screen
    }
}
