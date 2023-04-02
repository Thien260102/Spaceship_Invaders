using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Player : Entity
{
    public Camera mainCamera;
    public Bullet bullet;

    public GameObject pauseMenu; 
    private bool paused = false;

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardController();
        if (!paused)
        {
            MouseController();
        }

        if (IsDeleted)
            Destroy(gameObject);
    }

    void MouseController()
    {
        //get mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        float halfHeight = Variables.ScreenHeight / 2;
        float halfWidth = Variables.ScreenWidth / 2;

        // limit moving area of player
        if (mousePosition.x < -halfWidth + Variables.Adjust)
            mousePosition.x = -halfWidth + Variables.Adjust;
        else if (mousePosition.x > halfWidth - Variables.Adjust)
            mousePosition.x = halfWidth - Variables.Adjust;
        if (mousePosition.y < -halfHeight + Variables.Adjust)
            mousePosition.y = -halfHeight + Variables.Adjust;
        else if (mousePosition.y > halfHeight - Variables.Adjust)
            mousePosition.y = halfHeight - Variables.Adjust;
        Body.position = mousePosition;

        // pressed mouse left // Spaceship shooting
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = new Vector2(Body.position.x - Variables.Adjust / 3, Body.position.y + Variables.Adjust);
            Bullet Instantiate_Bullet = Instantiate(bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
            Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            Instantiate_Bullet.Init(Variables.ByPlayer);

            position = new Vector2(Body.position.x + Variables.Adjust / 3, Body.position.y + Variables.Adjust); ;
            Instantiate_Bullet = Instantiate(bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
            Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            Instantiate_Bullet.Init(Variables.ByPlayer);
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
