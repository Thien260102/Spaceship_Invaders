using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.DataPersistence;

public class MainMenuScript : MonoBehaviour
{
    //private string[] scenePaths = {"Assets/Scenes/Level1.unity"};
    public List<GameObject> MainMenuCanvas = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.PlayMenuAudioBackGround();
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
