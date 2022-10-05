using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    
    public void Play()
    {
        Debug.Log("Playing game");
        animator.SetTrigger("Play");
        //WaitForSeconds();
        SceneManager.LoadScene("Game");
    }

    // public void Resume()
    // {
    //     this.Play();
    //     Director d = d;
    // }

    public void Quit()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }
}
