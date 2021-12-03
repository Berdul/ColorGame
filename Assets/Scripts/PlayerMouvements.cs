using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    public float movementSpeed;
    public GameObject firePoint;
    public Rigidbody rb;
    public Vector3 moveDirection;
    public float positionY;

    void Start()
    {
        transform.position = new Vector3(0, positionY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * movementSpeed;

        // Get Player look in the direction of the mouse
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            Vector3 lookAtPoint = ray.GetPoint(distance);
            lookAtPoint.y = transform.position.y;
            transform.LookAt(lookAtPoint);
        }

        // Do a roll
    }
}
