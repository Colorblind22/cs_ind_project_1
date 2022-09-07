using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Transform pivot;
    public Transform barrel;
    public GameObject projectilePrefab;
    public Camera cam;
    public Transform player;
    public float moveSpeed = 5f;
    public float projectileForce = 5f;
    public float visionRange = 5f;


    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.position - pivot.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //cam.ScreenToWorldPoint(player.position);

        //pivot.LookAt(player);

        if(Input.GetButtonDown("Jump") && !PauseMenu.GamePaused)
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        Vector2 dir = player.position - pivot.position;
        if(DistanceFromPlayer() > visionRange)
        {
            rigidBody.MovePosition(rigidBody.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
               
    }

    float DistanceFromPlayer()
    {
        // a^2 + b^2 = c^2
        // c = sqrt(a^2 + b^2)
        // Mathf.Sqrt();

        float a = player.position.x - pivot.position.x;
        float b = player.position.y - pivot.position.y;
        return Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(barrel.right * projectileForce, ForceMode2D.Impulse);
    }
}
