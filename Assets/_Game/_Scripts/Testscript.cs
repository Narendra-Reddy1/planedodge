using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testscript : MonoBehaviour
{
    public float speed = 5.0f;
    public float maxSpeed = 10.0f;
    public float rotationSpeed = 2.0f;
    public float turningRadius = 1.0f;
    public Transform joystick;
    public Transform planeModel;

    private float rollInput;
    private float pitchInput;
    private float yawInput;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the joystick or keyboard
        rollInput = Input.GetAxis("Horizontal");
        pitchInput = Input.GetAxis("Vertical");

        // Calculate and apply airplane rotation
        CalculateRotation();
    }

    void FixedUpdate()
    {
        // Limit airplane speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Move the airplane forward
        rb.AddForce(transform.right * speed);

        // Handle turning using torque

        //Vector3 rotationTorque = new Vector3(-pitchInput, yawInput, -rollInput) * rotationSpeed;
        //rb.AddTorque(rotationTorque);

        rb.AddTorque(-rollInput * rotationSpeed);

    }

    void CalculateRotation()
    {
        // Calculate and apply roll and pitch based on input
        float roll = rollInput * turningRadius;
        // float pitch = pitchInput * turningRadius;

        // Calculate airplane's rotation angles
        float tiltAroundZ = roll * -rotationSpeed;
        //float tiltAroundX = pitch * -rotationSpeed;

        // Apply rotation to the airplane model
        planeModel.localRotation = Quaternion.Euler(0, 0, tiltAroundZ);
    }
}
