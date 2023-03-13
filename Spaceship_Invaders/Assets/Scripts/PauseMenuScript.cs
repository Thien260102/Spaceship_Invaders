using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private string mainMenuScenePath = "Assets/Scenes/MainMenu.unity";
    public List<GameObject> PauseMenuCanvas= new List<GameObject>();

    private bool isVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach (GameObject c in GameObject.FindGameObjectsWithTag("MainMenuCanvas"))
        {
            MainMenuCanvas.Add(c);
        }*/
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
