using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private string[] scenePaths = {"Assets/Scenes/LevelTest.unity"};

    // Start is called before the first frame update
    void Start()
    {

    }

    public void LoadLevel(int i)
    {
        Debug.Log("Scene loading: " + scenePaths[i]);
        if (scenePaths[i] != null)
        {
            SceneManager.LoadScene(scenePaths[i], LoadSceneMode.Single);
        } else{
            Debug.Log("scene not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
