using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public float damage;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Health enemy = other.GetComponent<Health>();

        if(enemy != null)
        {
            enemy.Damage(this.damage);
            Debug.Log($"{this.damage} damage dealt");
            Destroy(gameObject);
        }
        else if(other.GetComponent<ProjectileHandler>() != null || other.GetComponent<EnemyProjectileHandler>() != null) return;
        else Destroy(gameObject);
    }
}
