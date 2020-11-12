using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;  

    void Update()
    {
        if (player == null) { return; }

        Vector3 newPos = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            -10f
        );

        transform.position = newPos;
    }
}
