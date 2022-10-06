using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region vars
    public static bool GamePaused = false;
    public GameObject pauseUI;

    public Director dir;
    #endregion
    #region methods   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused) Resume(); else Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("Resumed game");
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        Debug.Log("Paused game");
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Menu()
    {
        Debug.Log("Back to main menu");
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveAndExit()
    {
        Debug.Log("Saving and exiting to menu");
        dir.Save();
        this.Menu();
    }
    #endregion
}
