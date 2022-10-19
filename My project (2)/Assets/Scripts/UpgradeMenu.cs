using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    #region vars
    public GameObject menu;
    public GameObject player;
    public Weapon wep;
    public PlayerHealth hp;
    public Move movement;
    public TMPro.TMP_Text 
    currencyDisplay,
    healthLabel,
    moveSpeedLabel,
    damageLabel,
    fireRateLabel,
    healLabel,
    healthCostLabel,
    moveSpeedCostLabel,
    damageCostLabel,
    fireRateCostLabel,
    healCostLabel;

    public float 
    damageUpgradeAmount = 5f,
    damageCap = 100f,
    moveSpeedUpgradeAmount = 1f,
    moveSpeedCap = 15f,
    fireRateUpgradeAmount = 50f,
    fireRateCap = 1000f,
    healthUpgradeAmount = 10f,
    healthCap = 200f,
    healAmount = 20f;
    
    public int 
    damageUpgradeCost = 2,
    moveSpeedUpgradeCost = 2,
    fireRateUpgradeCost = 2,
    healthUpgradeCost = 2,
    healCost = 10,
    damageUpgradeCount = 0,
    moveSpeedUpgradeCount = 0,
    fireRateUpgradeCount = 0,
    healthUpgradeCount = 0;

    bool
    damageMaxed = false,
    moveSpeedMaxed = false,
    healthMaxed = false,
    fireRateMaxed = false;

    public int currency = 0;
    #endregion

    public void Start()
    {
        Debug.Log("UpgradeMenu.Start() called");
        wep = player.GetComponentInChildren<Weapon>();
        hp = player.GetComponent<PlayerHealth>();
        movement = player.GetComponent<Move>();
        AddCurrency(0);
        UpdateText();
    }
    public void AddCurrency(int add)
    {
        currencyDisplay.text = $"{this.currency+=add}";
    }
    public void UpgradeDamage()
    {
        if(!damageMaxed && currency >= damageUpgradeCost)
        {
            Debug.Log("damage upgraded");
            wep.damage += this.damageUpgradeAmount;
            currency -= damageUpgradeCost;
            damageUpgradeCost += 1 * ++damageUpgradeCount;
            UpdateText();
        }
        if(!(wep.damage < this.damageCap)) this.damageMaxed=true;
    }
    public void UpgradeMoveSpeed()
    {
        if(!moveSpeedMaxed && currency >= moveSpeedUpgradeCost)
        {
            Debug.Log("movespeed upgraded");
            movement.moveSpeed += this.moveSpeedUpgradeAmount;
            currency -= moveSpeedUpgradeCost;
            moveSpeedUpgradeCost += 1 * ++moveSpeedUpgradeCount;
            UpdateText();
        }
        if(!(movement.moveSpeed < this.moveSpeedCap)) this.moveSpeedMaxed=true;
    }
    public void UpgradeFireRate()
    {
        if (!fireRateMaxed && currency >= fireRateUpgradeCost)
        {
            Debug.Log("fire rate upgraded");
            wep.SetFireRate(wep.fireRate + this.fireRateUpgradeAmount);
            currency -= fireRateUpgradeCost;
            fireRateUpgradeCost += 1 * ++fireRateUpgradeCount;
            UpdateText();
        }
        if(!(wep.fireRate < this.fireRateCap)) this.fireRateMaxed=true;
    }
    public void UpgradeHealth()
    {
        if (!healthMaxed && currency >= healthUpgradeCost)
        {
            Debug.Log("health upgraded");
            hp.maxHealth += this.healthUpgradeAmount;
            hp.Heal(this.healthUpgradeAmount);
            currency -= healthUpgradeCost;
            healthUpgradeCost += 1*++healthUpgradeCount;
            UpdateText();
        }
        if(!(hp.maxHealth < this.healthCap)) this.healthMaxed=true;
    }
    public void Heal()
    {
        if(hp.maxHealth > hp.GetHealth() && currency >= healCost)
        {
            Debug.Log($"Healing {this.healAmount} health");
            hp.Heal(this.healAmount);
            currency -= healCost;
            UpdateText();
        }
    }
    public void UpdateText()
    {
        try {
            damageLabel.text = !damageMaxed ? $"{wep.damage}" : $"{wep.damage} (MAX)"; 
            moveSpeedLabel.text = !moveSpeedMaxed ? $"{movement.moveSpeed}" : $"{movement.moveSpeed} (MAX)";
            fireRateLabel.text = !fireRateMaxed ? $"{wep.fireRate}" : $"{wep.fireRate} (MAX)";
            healthLabel.text = !healthMaxed ? $"{hp.maxHealth}" : $"{hp.maxHealth} (MAX)";
            healLabel.text = $"{hp.GetHealth()}/{hp.maxHealth}";
            currencyDisplay.text = $"{this.currency}";
            healthCostLabel.text = $"{this.healthUpgradeCost}";
            moveSpeedCostLabel.text = $"{this.moveSpeedUpgradeCost}";
            damageCostLabel.text = $"{this.damageUpgradeCost}";
            fireRateCostLabel.text = $"{this.fireRateUpgradeCost}";
            healCostLabel.text = $"{this.healCost}";
            Debug.Log("Text for UpgradeMenu updated");
        }
        catch
        {
            Debug.Log("error updating text");
        }
    }
}
