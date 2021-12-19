using System.Collections;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
    public GameObject firePoint;
    public Rigidbody rb;
    public Vector3 moveDirection;
    public float positionY;

    private float baseMoveSpeed;
    private bool dashing;

    void Start()
    {
        baseMoveSpeed = moveSpeed;
        transform.position = new Vector3(0, positionY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing) {
            moveDirection = Quaternion.Euler(0, 45, 0) * new Vector3(
                                Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(dash((Vector3) moveDirection, dashSpeed));
        }

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

    IEnumerator dash(Vector3 dashDirection, float dashSpeed) {
        Debug.Log("DASHING");
        dashing = true;
        moveDirection = dashDirection;
        moveSpeed = dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        moveSpeed = baseMoveSpeed;
        dashing = false;
    }
}
