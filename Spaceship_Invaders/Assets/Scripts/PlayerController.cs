using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;
    public Camera mainCamera;
    // Update is called once per frame
    void Update()
    {
        //get mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = mainCamera.aspect * halfHeight;

        // limit moving area of player
        if (mousePosition.x < -halfWidth)
            mousePosition.x = -halfWidth;
        else if (mousePosition.x > halfWidth)
            mousePosition.x = halfWidth;
        if (mousePosition.y < -halfHeight)
            mousePosition.y = -halfHeight;
        else if (mousePosition.y > halfHeight)
            mousePosition.y = halfHeight;
        player.position = mousePosition;

        Cursor.visible = false; // invisible cursor
        Cursor.lockState = CursorLockMode.Confined;// block cursor into Game screen
    }
}
