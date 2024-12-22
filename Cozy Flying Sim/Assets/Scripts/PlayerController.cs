using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveForce;
    [SerializeField] private float lift;
    private Quaternion rotation;
    private Vector3 targetVelocity;
    private Vector3 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        targetVelocity = new Vector3(rb.rotation.z, lift, moveForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Foward");
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, 1f);
        }

           
    }
}
