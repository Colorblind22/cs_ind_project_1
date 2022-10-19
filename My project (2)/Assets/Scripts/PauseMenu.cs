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
    public Animator levelTransition;
    public Animator anim;
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
        StartCoroutine(this.FadeOut());
    }

    void Pause()
    {
        StartCoroutine(this.FadeIn());
        Debug.Log("Paused game");
    }

    IEnumerator FadeIn()
    {
        pauseUI.SetActive(true);
        GamePaused = true;
        this.anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(.25f);
        Time.timeScale = 0f;
    }

    IEnumerator FadeOut()
    {
        Time.timeScale = 1f;
        this.anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(.25f);
        pauseUI.SetActive(false);
        GamePaused = false;
    }

    IEnumerator Menu()
    {
        Debug.Log("Back to main menu");
        Resume();
        levelTransition.SetTrigger("Play");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenu()
    {
        StartCoroutine(this.Menu());
    }

    public void SaveAndExit()
    {
        Debug.Log("Saving and exiting to menu");
        StartCoroutine(dir.CloseUpgradeMenu());
        dir.Save();
        BackToMenu();
    }
    #endregion
}
