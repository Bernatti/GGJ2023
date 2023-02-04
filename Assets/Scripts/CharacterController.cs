using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public GameObject currentPlanet;
    public float rotationMultiplier = 1.0f;
    public Rigidbody rb;

    public float jumpForce = 10.0f;
    public float maxJumpForce = 20.0f;
    public float jumpPressDuration = 0.5f;

    private float jumpPressTime = 0.0f;
    private bool isJumping = false;

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        var newUp = (transform.position - currentPlanet.transform.position).normalized;
        if (horizontalInput > 0)
            transform.Rotate(newUp, 45.0f * Time.deltaTime * rotationMultiplier, Space.World);
        else if (horizontalInput < 0)
            transform.Rotate(newUp, -45.0f * Time.deltaTime * rotationMultiplier, Space.World);


        //transform.Rotate(newUp,  45.0f * Time.deltaTime, Space.World);
        transform.Rotate(transform.right, Mathf.Deg2Rad * Vector3.SignedAngle(transform.up, newUp, transform.right), Space.World);

        if (verticalInput > 0)
        {
            Vector3 v3Force = movementSpeed * transform.forward;
            rb.AddForce(v3Force);
        }
        else if (verticalInput < 0)
        {
            Vector3 v3Force = movementSpeed * -transform.forward;
            rb.AddForce(v3Force);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressTime = Time.time;
            isJumping = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }


    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            float timePressed = Time.time - jumpPressTime;
            float force = Mathf.Clamp(jumpForce * timePressed / jumpPressDuration, jumpForce, maxJumpForce);
            rb.AddRelativeForce(Vector3.up * force, ForceMode.Impulse);
            isJumping = false;
        }
    }


}
