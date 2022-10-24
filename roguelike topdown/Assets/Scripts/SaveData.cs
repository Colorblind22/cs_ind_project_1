using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int
    currency,
    wave;
    public float[] playerPosition;

    public float
    health,
    maxHealth,
    moveSpeed,
    damage,
    fireRate;
    
    public int 
    damageUpgradeCost,
    moveSpeedUpgradeCost,
    fireRateUpgradeCost,
    healthUpgradeCost,
    damageUpgradeCount,
    moveSpeedUpgradeCount,
    fireRateUpgradeCount,
    healthUpgradeCount;

    public int
    projectiles,
    enemiesDefeated,
    totalCurrency;

    public SaveData(GameObject player, UpgradeMenu upgrades, Director director, GameOverMenu gameOver)
    {
        this.currency = upgrades.currency;

        this.wave = director.GetWave();

        this.playerPosition = new float[2];
        this.playerPosition[0] = player.transform.position.x;
        this.playerPosition[1] = player.transform.position.y;

        this.health = upgrades.hp.GetHealth();
        this.maxHealth = upgrades.hp.maxHealth;
        this.moveSpeed = upgrades.movement.moveSpeed;
        this.damage = upgrades.wep.damage;
        this.fireRate = upgrades.wep.fireRate;

        this.damageUpgradeCost = upgrades.damageUpgradeCost;
        this.moveSpeedUpgradeCost = upgrades.moveSpeedUpgradeCost;
        this.fireRateUpgradeCost = upgrades.fireRateUpgradeCost;
        this.healthUpgradeCost = upgrades.healthUpgradeCost;
        this.damageUpgradeCount = upgrades.damageUpgradeCount;
        this.moveSpeedUpgradeCount = upgrades.moveSpeedUpgradeCount;
        this.fireRateUpgradeCount = upgrades.fireRateUpgradeCount;
        this.healthUpgradeCount = upgrades.healthUpgradeCount;

        this.projectiles = gameOver.projectiles;
        this.enemiesDefeated = gameOver.enemiesDefeated;
        this.totalCurrency = gameOver.totalCurrency;
    }

    public override string ToString()
    {
        return
        (
            $"Data\n" +
            $"wave: {this.wave}\n" +
            $"currency: {this.currency}\n" +
            $"position: ({this.playerPosition[0]}, {this.playerPosition[1]})\n" +
            $"health: {this.health}\n" +
            $"maxHealth: {this.maxHealth}\n" +
            $"moveSpeed: {this.moveSpeed}\n" +
            $"damage: {this.damage}\n" +
            $"fireRate: {this.fireRate}\n" +
            $"healthUpgradeCost: {this.healthUpgradeCost}\n" +
            $"moveSpeedUpgradeCost: {this.moveSpeedUpgradeCost}\n" +
            $"damageUpgradeCost: {this.damageUpgradeCost}\n" +
            $"fireRateUpgradeCost: {this.fireRateUpgradeCost}\n" +
            $"healthUpgradeCount: {this.healthUpgradeCount}\n" +
            $"moveSpeedUpgradeCount: {this.moveSpeedUpgradeCount}\n" +
            $"damageUpgradeCount: {this.damageUpgradeCount}\n" +
            $"fireRateUpgradeCount: {this.fireRateUpgradeCount}\n"
        );
    }
}
