using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool jumpInput, isGrounded;

    [HideInInspector]
    public float horizontalInput, verticalInput;

    [SerializeField]
    private float moveSpeed, jumpForce, rayLength, smoothTurn;
    private float smoothVelocity;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
        }

        Vector3 direction = new Vector3(-horizontalInput, 0, -verticalInput).normalized;

        // If moving
        if (horizontalInput + verticalInput != 0)
        {
            // Calculate angle we are facing
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            // Damp that angle from current angle to target angle using smoothTurn
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref smoothVelocity, smoothTurn);

            // Rotate to that angle
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void FixedUpdate()
    {
        // Check if we are touching the ground using a ray
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            if (hit.collider.gameObject.CompareTag("Death"))
            {
                SceneManager.LoadScene("Game");
            }
        }
        else
        {
            isGrounded = false;
        }

        // If we press jump, and are on the ground, add a jump force
        if (jumpInput && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpInput = false;
        }

        // Move normally on x and z axis
        float x = horizontalInput * moveSpeed;
        float z = verticalInput * moveSpeed;
        rb.velocity = new Vector3(x, rb.velocity.y, z);
    }
        
}

