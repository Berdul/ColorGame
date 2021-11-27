using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    public GameObject player;
    private float offset;
    private bool closeCamera = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            closeCamera = !closeCamera;
        }
        if (closeCamera) {
            offset = 12f;
            transform.position = new Vector3(player.transform.position.x - offset, 50, player.transform.position.z - offset);
        } else {
            offset = 3f;
            transform.position = new Vector3(player.transform.position.x - offset, 15, player.transform.position.z - offset);
        }
    }
}
