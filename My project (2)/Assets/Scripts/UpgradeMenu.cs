using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;

    public GameObject player;

    private Weapon wep;
    private Health hp;
    private Move movement;

    //public int upgradesPerWave = 2; // this system or a currency system?

    public TMPro.TMP_Text healthLabel;
    public TMPro.TMP_Text moveSpeedLabel;
    public TMPro.TMP_Text damageLabel;
    public TMPro.TMP_Text fireRateLabel;
    public TMPro.TMP_Text healLabel;


    public float damageUpgradeAmount = 5f;
    public float damageCap = 100f;
    public float moveSpeedUpgradeAmount = 1f;
    public float moveSpeedCap = 15f;
    public float fireRateUpgradeAmount = 50f;
    public float fireRateCap = 1000f;
    public float healthUpgradeAmount = 10f;
    public float healthCap = 200f;
    public float healAmount = 20f;
    
    //use these eventually to limit upgrades. for now work on display & function

    void Start()
    {
        wep = player.GetComponentInChildren<Weapon>();
        hp = player.GetComponent<Health>();
        movement = player.GetComponent<Move>();
        UpdateText();
    }

    public void UpgradeDamage()
    {
        if(wep.damage < this.damageCap)
        {
            Debug.Log("damage upgraded");
            wep.damage += this.damageUpgradeAmount;
            UpdateText();
        }
        else
        {
            // capped
        }
    }
    public void UpgradeMoveSpeed()
    {
        if (movement.moveSpeed < this.moveSpeedCap)
        {
            Debug.Log("movespeed upgraded");
            movement.moveSpeed += this.moveSpeedUpgradeAmount;
            UpdateText();
        }
        else
        {
            //capped
        }
    }
    public void UpgradeFireRate()
    {
        if (wep.fireRate < this.fireRateCap)
        {
            Debug.Log("fire rate upgraded");
            wep.SetFireRate(wep.fireRate + this.fireRateUpgradeAmount);
            UpdateText();
        }
        else
        {
            //capped
        }
    }
    public void UpgradeHealth()
    {
        if (hp.maxHealth < this.healthCap)
        {
            Debug.Log("health upgraded");
            hp.maxHealth += this.healthUpgradeAmount;
            hp.Heal(10f);
            UpdateText();
        }
        else
        {
            //capped
        }
    }
    public void Heal()
    {
        Debug.Log("Healing");
        hp.Heal(this.healAmount);
        UpdateText();
    }

    void UpdateText()
    {
        damageLabel.text = $"{wep.damage}";
        moveSpeedLabel.text= $"{movement.moveSpeed}";
        fireRateLabel.text = $"{wep.fireRate}";
        healthLabel.text = $"{hp.maxHealth}";
        healLabel.text = $"{hp.GetHealth()}/{hp.maxHealth}";
    }
}
