using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class PauseMenuScript : MonoBehaviour
{
    private string mainMenuScenePath = "Assets/Scenes/MainMenu.unity";
    public List<GameObject> PauseMenuCanvas= new List<GameObject>();

    private bool isVisible = false;
    [SerializeField]
    GameObject playerObject;

    private Player player;

    bool State = false; // false: UnActive, true: Active

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            switch (State)
            {
                case false:
                    Show();
                    State = true;
                    Cursor.visible = true; // show cursor
                    player.GamePaused();
                    break;

                case true:
                    Hide();
                    State = false;
                    Cursor.visible = false;
                    player.GameResume();
                    break;
            }
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            HUD.Instance.Coin++;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            if(HUD.Instance.Life <= 0)
            {
                HUD.Instance.Life++;
                player.gameObject.SetActive(true);
            }
        }
    }

    public void NavigateTo(int targetCanvas)
    {
        foreach (GameObject canvas in PauseMenuCanvas)
        {
            canvas.SetActive(false);
        }
        PauseMenuCanvas[targetCanvas].SetActive(true);
    }

    public void Show()
    {
        PauseMenuCanvas[0].SetActive(true);
        isVisible = true;
    }

    public void Hide()
    {
        foreach (GameObject canvas in PauseMenuCanvas)
        {
            canvas.SetActive(false);
        }
        isVisible = false;
        //ResumeGame
        Cursor.visible = false; // invisible cursor

        player.GameResume();
    }

    public bool isShowing()
    {
        return isVisible;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning...");
        SceneManager.LoadScene(mainMenuScenePath, LoadSceneMode.Single);
    }

}
