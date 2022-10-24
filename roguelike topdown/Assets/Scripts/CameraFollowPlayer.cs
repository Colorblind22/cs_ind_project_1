using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        Transform cam = gameObject.transform;
        Vector2 playerPos = this.player.position;
        Vector3 cameraPos = new Vector3();
        cameraPos.x = playerPos.x;
        cameraPos.y = playerPos.y;
        cameraPos.z = -10f;
        cam.position = cameraPos;
    }
}
