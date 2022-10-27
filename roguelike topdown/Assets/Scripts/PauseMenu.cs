using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region vars
    public static bool GamePaused = false;
    public GameObject upgradeMenu;
    public Director director;
    public Animator levelTransition;
    public Animator anim;
    #endregion
    #region methods   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!upgradeMenu.activeInHierarchy) if(!GamePaused) Pause(); else Resume();
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
        gameObject.SetActive(true);
        GamePaused = true;
        this.anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(.25f);
        Time.timeScale = 0f;
    }

    IEnumerator FadeOut()
    {
        Time.timeScale = 1f;
        //this.anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(.25f);
        gameObject.SetActive(false);
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
        StartCoroutine(upgradeMenu.GetComponent<UpgradeMenu>().Close());
        director.Save();
        BackToMenu();
    }
    #endregion
}
