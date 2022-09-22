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
    public TMPro.TMP_Text currencyDisplay;
    public TMPro.TMP_Text healthLabel;
    public TMPro.TMP_Text moveSpeedLabel;
    public TMPro.TMP_Text damageLabel;
    public TMPro.TMP_Text fireRateLabel;
    public TMPro.TMP_Text healLabel;
    public TMPro.TMP_Text healthCostLabel;
    public TMPro.TMP_Text moveSpeedCostLabel;
    public TMPro.TMP_Text damageCostLabel;
    public TMPro.TMP_Text fireRateCostLabel;
    public TMPro.TMP_Text healCostLabel;
    public float damageUpgradeAmount = 5f;
    public float damageCap = 100f;
    public float moveSpeedUpgradeAmount = 1f;
    public float moveSpeedCap = 15f;
    public float fireRateUpgradeAmount = 50f;
    public float fireRateCap = 1000f;
    public float healthUpgradeAmount = 10f;
    public float healthCap = 200f;
    public float healAmount = 20f;
    public int damageUpgradeCost = 2,
    moveSpeedUpgradeCost = 2,
    fireRateUpgradeCost = 2,
    healthUpgradeCost = 2;
    public int healCost = 10;

    int damageUpgradeCount = 0,
    moveSpeedUpgradeCount = 0,
    fireRateUpgradeCount = 0,
    healthUpgradeCount = 0;

    public int currency = 0;
    
    //use these eventually to limit upgrades. for now work on display & function

    void Start()
    {
        wep = player.GetComponentInChildren<Weapon>();
        hp = player.GetComponent<Health>();
        movement = player.GetComponent<Move>();
        AddCurrency(0);
        UpdateText();
    }
    
    public void AddCurrency(int add)
    {
        this.currency += add;
        currencyDisplay.text = $"{this.currency}";
    }

    public void UpgradeDamage()
    {
        if(wep.damage < this.damageCap && currency >= damageUpgradeCost)
        {
            Debug.Log("damage upgraded");
            wep.damage += this.damageUpgradeAmount;
            currency -= damageUpgradeCost;
            damageUpgradeCost += 1 * ++damageUpgradeCount;
            UpdateText();
        }
    }
    public void UpgradeMoveSpeed()
    {
        if (movement.moveSpeed < this.moveSpeedCap && currency >= moveSpeedUpgradeCost)
        {
            Debug.Log("movespeed upgraded");
            movement.moveSpeed += this.moveSpeedUpgradeAmount;
            currency -= moveSpeedUpgradeCost;
            moveSpeedUpgradeCost += 1 * ++moveSpeedUpgradeCount;
            UpdateText();
        }
    }
    public void UpgradeFireRate()
    {
        if (wep.fireRate < this.fireRateCap && currency >= fireRateUpgradeCost)
        {
            Debug.Log("fire rate upgraded");
            wep.SetFireRate(wep.fireRate + this.fireRateUpgradeAmount);
            currency -= fireRateUpgradeCost;
            fireRateUpgradeCost += 1 * ++fireRateUpgradeCount;
            UpdateText();
        }
    }
    public void UpgradeHealth()
    {
        if (hp.maxHealth < this.healthCap && currency >= healthUpgradeCost)
        {
            Debug.Log("health upgraded");
            hp.maxHealth += this.healthUpgradeAmount;
            hp.Heal(this.healthUpgradeAmount);
            currency -= healthUpgradeCost;
            healthUpgradeCost += 1*++healthUpgradeCount;
            UpdateText();
        }
    }
    public void Heal()
    {
        if(hp.maxHealth < hp.GetHealth() && currency >= healCost)
        {
            Debug.Log("Healing");
            hp.Heal(this.healAmount);
            currency -= healCost;
            UpdateText();
        }
    }

    void UpdateText()
    {
        damageLabel.text = $"{wep.damage}";
        moveSpeedLabel.text= $"{movement.moveSpeed}";
        fireRateLabel.text = $"{wep.fireRate}";
        healthLabel.text = $"{hp.maxHealth}";
        healLabel.text = $"{hp.GetHealth()}/{hp.maxHealth}";
        currencyDisplay.text = $"{this.currency}";
        healthCostLabel.text = $"{this.healthUpgradeCost}";
        moveSpeedCostLabel.text = $"{this.moveSpeedUpgradeCost}";
        damageCostLabel.text = $"{this.damageUpgradeCost}";
        fireRateCostLabel.text = $"{this.fireRateUpgradeCost}";
        healCostLabel.text = $"{this.healCost}";
    }
}
