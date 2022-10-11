using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public int
    wave,
    projectiles,
    enemiesDefeated,
    totalCurrency;
    public bool newHighScore = false;
    public GameObject newHighScoreLabel;
    public TMPro.TMP_Text
    waveLabel,
    statsTextBox;
    public Animator animator;
    public Director director;

    public void Activate()
    {
        this.GetVariables();
        gameObject.SetActive(true);
        animator.SetTrigger("GameOver");
        waveLabel.text = $"Reached Wave {wave}";
        statsTextBox.text =
        $"Total projectiles fired: {projectiles}\n" +
        $"Total enemies defeated: {enemiesDefeated}\n" +
        $"Total currency gained: {totalCurrency}\n";
        newHighScoreLabel.SetActive(newHighScore);
    }

    void GetVariables()
    {
        this.wave = director.GetWave();
        this.projectiles = director.upgrades.wep.projectiles;
    }

    public void Exit()
    {
        SaveSystem.Wipe();
        Application.Quit();
    }
}
