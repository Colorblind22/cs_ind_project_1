using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileHandler : MonoBehaviour
{
    #region vars
	public float damage = 25f;
	
	public float timeLimit = 10f;
    #endregion
    #region methods
    void LateUpdate()
    {
        if (timeLimit > 0) timeLimit -= Time.deltaTime; else Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if(other.GetComponentInChildren<Enemy>()) return;

        if(player != null)
        {
            player.Damage(this.damage);
            Destroy(gameObject);
        }
        else if(other.GetComponent<ProjectileHandler>() != null || other.GetComponent<EnemyProjectileHandler>() != null) return;
        else Destroy(gameObject);
    }
    #endregion
}
