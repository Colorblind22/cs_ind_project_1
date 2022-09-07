using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed = 5.0f;
    float vertical;
    float horizontal;
    Vector2 move;
    // Update is called once per frame
    void Update()
    {
        move.y = Input.GetAxis("Vertical");
        move.x = Input.GetAxis("Horizontal");

        //player.Translate(horizontal*moveSpeed*dt, vertical*moveSpeed*dt, 0);
    }

    void FixedUpdate() 
    {
        player.MovePosition(player.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
