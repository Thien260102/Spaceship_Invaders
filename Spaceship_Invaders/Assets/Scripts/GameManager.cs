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
        if (instance == null)
            instance = this;

        // this will exist throughout all scenes
        DontDestroyOnLoad(this);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CompleteLevel()
    {
        Debug.Log("Level Won");
        Invoke("NextLevel", 1f);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}