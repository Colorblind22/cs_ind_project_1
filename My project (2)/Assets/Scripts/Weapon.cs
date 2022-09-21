using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        this.RPMToInterval();
    }

    public Transform pivot;
    public Transform barrel;
    public GameObject projectilePrefab;
    public Camera cam;
    public float projectileForce = 25f;
    public float fireRate = 100f;
    private float fireInterval;
    private float fireCooldown = 0f;
    public float damage = 25f;
    private static int projectiles = 1;


    public void SetFireRate(float input)
    {
        this.fireRate = input;
        this.RPMToInterval();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(pivot.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if(!PauseMenu.GamePaused) pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetButton("Fire1") && !PauseMenu.GamePaused && this.fireCooldown <= 0)
        {
            Fire();
            this.fireCooldown = this.fireInterval;
        }
        else /*if(Input.GetButton("Fire1"))*/ this.fireCooldown -= Time.deltaTime;
        //else this.fireCooldown = 0;

        
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        projectile.GetComponent<ProjectileHandler>().damage = this.damage;
        projectile.name = $"Projectile {projectiles++}";
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(barrel.right * projectileForce, ForceMode2D.Impulse);
    }

    // fire interval in seconds = 1/(rpm/60)
    void RPMToInterval() {this.fireInterval = 1/(this.fireRate/60);}
}