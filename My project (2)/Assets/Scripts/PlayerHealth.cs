using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject parent;
    public float maxHealth = 100f;
    private float health;
    public Director director;
    public HealthBar healthBar;

    private void Start() {
        this.health = this.maxHealth;
        if(healthBar is not null) healthBar.SetHealth(this.health/this.maxHealth);
    }

    public float GetHealth()
    {
        return this.health;
    }

    public void SetHealth(float arg)
    {
        this.health = arg;
    }

    public void Damage(float inflict)
    {
        this.health -= inflict;
        Debug.Log($"{gameObject.name} takes {inflict} damage ({this.health}/{this.maxHealth})");

        if(this.health <= 0)
        {
            Die();
        }

        if(this.healthBar is not null)
        {
            this.healthBar.SetHealth(this.health/this.maxHealth);
        }
    }

    public void Heal()
    {
        this.health = this.maxHealth;
    }
    
    public void Heal(float healAmount)
    {
        if(this.health + healAmount > this.maxHealth) this.health = this.maxHealth;
        else this.health += healAmount;
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died...");
        gameObject.SetActive(false);
        Time.timeScale = 0f;
        director.GameOver();
    }
}
