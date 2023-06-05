using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    private void Awake()
    {
        // this will exist throughout all scenes
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void CompleteLevel()
    {
        Debug.Log("Level Won");
        Invoke("NextLevel", 1f);
    }

    public void NextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int level)
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        if(level < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(level);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}