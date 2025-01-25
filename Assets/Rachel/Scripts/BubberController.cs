using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubberController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private float speedScale;
    private int slowCount;
    private Vector3 ogVel;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedScale = 1f;
        slowCount = 0;
        ogVel = Vector3.zero;
    }

    private void Update()
    {
        CapSpeed();
        CheckSlow();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region Movement
    private void Move()
    {
        // forward movement
        Vector3 forwardDir = 20f * Vector3.forward;

        // sideways movement
        Vector3 sidewaysDir = Vector3.zero;
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
        {
            sidewaysDir = 35f * Input.GetAxisRaw("Horizontal") * transform.right;
        }

        rb.AddForce(20f * speed * (forwardDir + sidewaysDir).normalized, ForceMode.Force);
    }

    private void CapSpeed()
    {
        Vector3 vel = new(rb.velocity.x, 0f, rb.velocity.z);

        if (vel.magnitude > maxSpeed)
        {
            Vector3 cappedVel = vel.normalized * maxSpeed;
            rb.velocity = new(cappedVel.x, rb.velocity.y, cappedVel.z);
        }
    }

    private void CheckSlow()
    {
        if (slowCount <= 0)
        {
            speedScale = 1f;
            ogVel = rb.velocity;
            return;
        }

        rb.velocity = new(rb.velocity.x, rb.velocity.y, ogVel.z * speedScale);
    }
    #endregion

    #region Slow
    public void Slow(float slowMultiplier)
    {
        speedScale = 1f - slowMultiplier;
        StartCoroutine(ISlow());
    }

    IEnumerator ISlow()
    {
        slowCount++;

        yield return new WaitForSeconds(1.5f);

        slowCount--;
    }
    #endregion
}
