using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    //private string[] scenePaths = {"Assets/Scenes/Level1.unity"};
    public List<GameObject> MainMenuCanvas = new List<GameObject>();

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
        foreach (GameObject canvas in MainMenuCanvas)
        {
            canvas.SetActive(false);
        }
        MainMenuCanvas[targetCanvas].SetActive(true);
        Debug.Log(targetCanvas);
    }

    public void LoadLevel(int i)
    {
        //Debug.Log("Scene loading: " + scenePaths[i]);
        //if (scenePaths[i] != null)
        //{
        //    SceneManager.LoadScene(scenePaths[i], LoadSceneMode.Single);
        //} else{
        //    Debug.Log("scene not found!");
        //}
        GameManager.Instance.LoadLevel(i);
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
