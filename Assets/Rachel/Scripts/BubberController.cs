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
    private float speedScale = 1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedScale = 1f;
    }

    private void Update()
    {
        transform.Translate(slideSpeedMultiplier * speed * speedScale * Time.deltaTime * Vector3.forward);

        if (Input.GetAxisRaw("Horizontal") < -0.5 || Input.GetAxisRaw("Horizontal") > 0.5)
        {
            rb.velocity += sidewaysSpeedMultiplier * speed * speedScale * Time.deltaTime * Input.GetAxisRaw("Horizontal") * transform.right;
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    #region Slow
    public void Slow(float slowMultiplier)
    {
        StartCoroutine(ISlow(slowMultiplier));
    }

    IEnumerator ISlow(float slowMultiplier)
    {
        speedScale -= slowMultiplier;

        yield return new WaitForSeconds(1.5f);

        speedScale = 1f;
    }
    #endregion
}
