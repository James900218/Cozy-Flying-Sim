using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveForce;
    [SerializeField] private float lift;
    [SerializeField] private float smoothTime = 1f;
    [SerializeField] private float rollSpeed = 1;
    [SerializeField] private float pitchSpeed = 1f;

    private Quaternion rotation;
    private Vector3 currentVelocity;

    private float accelerationInput;
    private float rollInput;
    private float pitchInput;

    private bool isGrounded = true;
    private LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        GroundCheck();
    }

    private void UpdateMovement()
    {

        Vector3 targetVelocity = new(rb.velocity.x, (accelerationInput * lift), (accelerationInput * moveForce) * 10f);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);

        rb.drag = rb.velocity.z / 2;

        transform.Rotate(new Vector3(pitchInput * pitchSpeed, 0f, rollInput * rollSpeed));
    }

    public void GetAcceleration(float value)
    {
        accelerationInput = value;
    }
    public void GetRoll(float value)
    {
        rollInput = value;
    }
    public void GetPitch(float value)
    {
        pitchInput = value;
    }

    private void GroundCheck()
    {
        Collider[] collision = Physics.OverlapCapsule(transform.transform.position, Vector3.down, 3f, whatIsGround);

        for (int i = 0; i < collision.Length; i++)
        {
            if (collision[i].gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            else isGrounded = false;
        }

        if (isGrounded)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
