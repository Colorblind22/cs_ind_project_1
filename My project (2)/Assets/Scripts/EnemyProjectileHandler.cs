using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHandler : MonoBehaviour
{
    public float damage = 25f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Health enemy = other.GetComponent<Health>();

        if(other.GetComponentInChildren<Enemy>()) return;

        if(enemy != null)
        {
            enemy.Damage(this.damage);
            Destroy(gameObject);
        }
        else if(other.GetComponent<ProjectileHandler>() != null || other.GetComponent<EnemyProjectileHandler>() != null) return;
        else Destroy(gameObject);
    }
}
