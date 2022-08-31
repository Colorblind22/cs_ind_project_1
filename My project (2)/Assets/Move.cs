using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    /* Start is called before the first frame update
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;  
    }*/

    public Transform player;

    public float moveSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float dt = Time.deltaTime;

        player.Translate(horizontal*moveSpeed*dt, vertical*moveSpeed*dt, 0);
    }
}
