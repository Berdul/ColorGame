using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float height;
    // private bool closeCamera = true;

    public float rotationSpeed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(45, 45, 0);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - offset, height, player.transform.position.z - offset);
        transform.LookAt(player.transform);
    }
}
