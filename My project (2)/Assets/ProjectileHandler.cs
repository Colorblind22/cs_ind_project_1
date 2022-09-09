using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public float damage = 25f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Health enemy = other.GetComponent<Health>();

        if(enemy != null)
        {
            enemy.Damage(this.damage);
            Destroy(gameObject);
        }
        else if(other.GetComponent<ProjectileHandler>() != null) return;
        else Destroy(gameObject);
    }
}
