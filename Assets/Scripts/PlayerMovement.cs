using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask floor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        rb.transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
        
        float rotationInRad = rb.rotation.eulerAngles.y / 180 * Mathf.PI;

        rb.velocity = new Vector3(verticalInput * movementSpeed * Mathf.Sin(rotationInRad), rb.velocity.y,
                                    verticalInput * movementSpeed * Mathf.Cos(rotationInRad));

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(rb.position, 0.5f, floor);
    }
}
