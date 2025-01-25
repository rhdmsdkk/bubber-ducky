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
    private float speedScale;
    private int slowCount;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedScale = 1f;
        slowCount = 0;
    }

    private void Update()
    {
        // constantly slide down the pipe
        transform.Translate(slideSpeedMultiplier * speed * speedScale * Time.deltaTime * Vector3.forward);

        // sideways movement
        if (Input.GetAxisRaw("Horizontal") < -0.5f || Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            rb.velocity += sidewaysSpeedMultiplier * speed * speedScale * Time.deltaTime * Input.GetAxisRaw("Horizontal") * transform.right;
        }

        // check for slow
        if (slowCount == 0)
        {
            speedScale = 1f;
        }
    }

    private void FixedUpdate()
    {
        // cap speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

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
