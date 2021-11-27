using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    public float movementSpeed;
    public GameObject firePoint;
    private Vector3 movement = new Vector3(0, 1f, 0);
    public CharacterController characterController;

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Get player direction, rotate it 45deg to match camera view, normalize it to move same speed in all directions
        Vector3 direction = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxisRaw("Horizontal"), 1f, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(direction * movementSpeed * Time.fixedDeltaTime, Space.World);

        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            Vector3 lookAtPoint = ray.GetPoint(distance);
            lookAtPoint.y = transform.position.y;
            transform.LookAt(lookAtPoint);
        }
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
}
