using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Player : Entity
{
    public Camera mainCamera;
    public Bullet bullet;

    public Animator animator;
    private Vector2 Velocity;

    public GameObject pauseMenu; 
    private bool paused = false;

    private Vector3 lastFrameMousePosition;
    public float sensitivity = 1.0f;

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        lastFrameMousePosition = Body.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeleted)
        {
            State = Variables.Player_DESTROYED;
            Invoke("Destroyed", 1.0f);
        }
        else 
        {
            KeyboardController();
            if (!paused)
            {
                MouseController2();
            }

            SetState();
        } 

        animator.SetInteger("State", State);
    }

    void SetState()
    {
        if (Velocity.x == 0 && Velocity.y == 0)
            State = Variables.Player_IDLE;
        else if (Velocity.y > 0)
            State = Variables.Player_BOOST;
        else
            State = Variables.Player_MOVE;
    }

    void MouseController2()
    {
        //get mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseMovement = mousePosition - lastFrameMousePosition;
        Vector3 newbodyPosition = new Vector3(Body.position.x, Body.position.y, mousePosition.z);
        newbodyPosition += mouseMovement * sensitivity;

        Velocity = mouseMovement / Time.deltaTime;

        float halfHeight = Variables.ScreenHeight / 2;
        float halfWidth = Variables.ScreenWidth / 2;

        // limit moving area of player
        if (newbodyPosition.x < -halfWidth + Variables.Adjust)
            newbodyPosition.x = -halfWidth + Variables.Adjust;
        else if (newbodyPosition.x > halfWidth - Variables.Adjust)
            newbodyPosition.x = halfWidth - Variables.Adjust;
        if (newbodyPosition.y < -halfHeight + Variables.Adjust)
            newbodyPosition.y = -halfHeight + Variables.Adjust;
        else if (newbodyPosition.y > halfHeight - Variables.Adjust)
            newbodyPosition.y = halfHeight - Variables.Adjust;
        Body.position = newbodyPosition;

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

        lastFrameMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void KeyboardController()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GamePaused();
        }
    }

    void GamePaused()
    {
        PauseMenuScript pauseMenuScript = pauseMenu.GetComponent<PauseMenuScript>();
        pauseMenuScript.Show();

        paused = true;
        Cursor.visible = true; // show cursor

        Time.timeScale = 0.0f;
    }

    public void GameResume()
    {
        //Since movement is detected by mouse position difference, reset last mouse position here so
        //there wont be a difference, then the player position wont jump.
        lastFrameMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Time.timeScale = 1.0f;

        paused = false;
        Cursor.visible = false; // invisible cursor
    }


    void Destroyed()
    {
        Destroy(gameObject);
    }
}
