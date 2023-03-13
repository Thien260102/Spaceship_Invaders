using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;
    public Camera mainCamera;
    public GameObject bullet;

    public GameObject pauseMenu; 
    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        KeyboardController();
        if (!paused)
        {
            MouseController();
        }
    }

    void MouseController()
    {
        //get mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = mainCamera.aspect * halfHeight;
        Debug.Log(string.Format("Height: {0}, width: {1}", halfHeight * 2, halfWidth * 2));

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

        // pressed mouse left // Spaceship shooting
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = new Vector2(player.position.x - Variables.Adjust / 3, player.position.y + Variables.Adjust);
            GameObject Instantiate_Bullet = Instantiate(bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
            Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);

            position = new Vector2(player.position.x + Variables.Adjust / 3, player.position.y + Variables.Adjust); ;
            Instantiate_Bullet = Instantiate(bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
            Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        }


        Cursor.visible = false; // invisible cursor
        Cursor.lockState = CursorLockMode.Confined;// block cursor into Game screen
    }

    void KeyboardController()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMenuScript pauseMenuScript = pauseMenu.GetComponent<PauseMenuScript>();
            pauseMenuScript.Show();

            paused = true;
            Cursor.visible = true; // invisible cursor
            Cursor.lockState = CursorLockMode.None;// block cursor into Game screen
        }
    }
}
