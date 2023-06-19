using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.DataPersistence;

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
        DataPersistenceManager.Instance.SaveData();
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        DataPersistenceManager.Instance.NewData();
        DataPersistenceManager.Instance.SaveData();
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void CompleteLevel()
    {
        Debug.Log("Level Won");
        Invoke("NextLevel", 1f);
    }

    public void NextLevel()
    {
        DataPersistenceManager.Instance.SaveData();
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int level)
    {
        DataPersistenceManager.Instance.LoadData();

        if (level < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(level);
        
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        DataPersistenceManager.Instance.SaveData();
        Application.Quit();
    }
}