using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public float moveSpeed;
    public float projectileForce;
    public float visionRange;
    public float fireCooldown;
    public float damage;

    public EnemyStats()
    {
        this.moveSpeed = 100f;
        this.projectileForce = 5f;
        this.visionRange = 10f;
        this.fireCooldown = .45f;
        this.damage = 12f;
    }

    // where factor is a proportion, i.e. 110% = 1.1
    public EnemyStats(float factor) 
    {
        this.moveSpeed = 100f * factor;
        this.projectileForce = 5f * factor;
        this.visionRange = 10f;
        this.fireCooldown = .45f;
        this.damage = 12f * factor;
    }

    public override string ToString()
    {
        return 
        (
            $"moveSpeed: {this.moveSpeed}\t...\n" +
            $"projectileForce: {this.projectileForce}\n" +
            $"visionRange: {this.visionRange}\n" +
            $"fireCooldown: {this.fireCooldown}\n" +
            $"damage: {this.damage}"
        );
    }
}
