using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubberController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float slideSpeedMultiplier;
    [SerializeField] private float sidewaysSpeedMultiplier;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(slideSpeedMultiplier * speed * Time.deltaTime * Vector3.forward);

        if (Input.GetAxisRaw("Horizontal") < -0.5 || Input.GetAxisRaw("Horizontal") > 0.5)
        {
            rb.velocity += sidewaysSpeedMultiplier * speed * Time.deltaTime * Input.GetAxisRaw("Horizontal") * transform.right;
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
