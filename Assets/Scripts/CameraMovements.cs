using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    public GameObject player;
    public float offset;
    private bool closeCamera = true;

    public float rotationSpeed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(45, 45, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            closeCamera = !closeCamera;
        }
        if (closeCamera) {
            offset = 5;
            transform.position = new Vector3(player.transform.position.x - offset, 20, player.transform.position.z - offset);
        } else {
            offset = 12;
            transform.position = new Vector3(player.transform.position.x - offset, 40, player.transform.position.z - offset);
        }
        transform.LookAt(player.transform);
    }
}
