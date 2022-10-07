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
    
    public void Play()
    {
        Debug.Log("Playing game");
        animator.SetTrigger("Play");
        //WaitForSeconds();
        SceneManager.LoadScene("Game");
    }

    public void Resume()
    {
        Flags.LoadOnEnter = true;
        this.Play();
    }

    public void Quit()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }
}
