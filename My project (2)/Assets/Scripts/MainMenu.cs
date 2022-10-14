using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public TMPro.TMP_Text highScoreDisplay;
    
    void Start()
    {
        this.highScoreDisplay.text = 
        $"Highest Wave: Wave {PlayerPrefs.GetInt("HighScore")}";
    }
    

    public void Resume()
    {
        Flags.LoadOnEnter = true;
        this.NewGame();
    }

    public void Quit()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }

    public void NewGame()
    {
        StartCoroutine(this.Play());
    }
    IEnumerator Play()
    {
        Debug.Log("Playing game");
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game");
    }

}
