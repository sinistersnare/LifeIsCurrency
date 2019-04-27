using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
        Vector3 newPos = player.transform.position;
        newPos.z = -10; // probably dont need to make this a variable....?
        this.transform.position = newPos;
    }
}
