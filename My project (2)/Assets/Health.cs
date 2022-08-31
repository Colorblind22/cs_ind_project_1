using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject parent;
    public float maxHealth = 100f;
    private float health;

    public HealthBar healthBar;

    private void Start() {
        this.health = this.maxHealth;
        healthBar.SetHealth(this.health/this.maxHealth);
    }

    public float GetHealth()
    {
        return this.health;
    }

    public void Damage(float inflict)
    {
        this.health -= inflict;

        if(this.health <= 0)
        {
            Destroy(gameObject);
        }

        healthBar.SetHealth(this.health/this.maxHealth);
    }
}
