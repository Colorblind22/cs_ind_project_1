using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Transform pivot;
    public Transform barrel;
    public GameObject projectilePrefab;
    //public Camera cam;
    public Transform player;
    public EnemyStats stats;
    //public float moveSpeed = 100f;
    private float fireTimer;
    static int enemyProjectiles = 1;

    void Start()
    {
        this.fireTimer = this.stats.fireCooldown;        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.position - pivot.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if(this.fireTimer <= 0 && !PauseMenu.GamePaused) 
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        //Vector2 dir = player.position - pivot.position;
        if(DistanceFromPlayer() > stats.visionRange)
        {
            Vector2 dir = new Vector2(player.position.x-pivot.position.x, player.position.y-pivot.position.y);
            rigidBody.AddForce(dir * this.stats.moveSpeed * Time.fixedDeltaTime);
            this.fireTimer = this.stats.fireCooldown;
        }
        else if(!PauseMenu.GamePaused && fireTimer > 0) fireTimer -= Time.deltaTime;
        //else this.stats.fireCooldown = 0; // i dont know why this line was here and it was causing the rapidfire bug
    }

    float DistanceFromPlayer() // pythagorean theorem to find exact distance from player
    {
        float a = player.position.x - pivot.position.x;
        float b = player.position.y - pivot.position.y;
        return Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        EnemyProjectileHandler projectileHandler = projectile.GetComponent<EnemyProjectileHandler>();
        projectileHandler.damage = stats.damage;
        projectile.name = $"Enemy projectile {enemyProjectiles++}";
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(barrel.right * stats.projectileForce, ForceMode2D.Impulse);
        this.fireTimer = this.stats.fireCooldown;
    }
}
