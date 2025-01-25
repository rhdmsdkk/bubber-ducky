using System.Collections;
using UnityEngine;

public class BubberController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private float speedScale;
    private int slowCount;
    private Vector3 ogVel;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private LayerMask groundLayer;
    private float bubberHeight;
    private bool isGrounded;

    [Header("Mesh")]
    [SerializeField] private Transform meshTransform;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        speedScale = 1f;
        slowCount = 0;
        ogVel = Vector3.zero;

        bubberHeight = GetComponent<CapsuleCollider>().radius;
    }

    private void Update()
    {
        CapSpeed();
        AlignMesh();
    }

    private void FixedUpdate()
    {
        Move();
        CheckSlow();
        CheckJump();
    }

    #region Move
    private void Move()
    {
        // forward movement
        Vector3 forwardDir = 20f * Vector3.forward;

        // sideways movement
        Vector3 sidewaysDir = Vector3.zero;
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
        {
            sidewaysDir = 50f * Input.GetAxisRaw("Horizontal") * transform.right;
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

    #region Jump
    private void CheckJump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, bubberHeight + 0.5f, groundLayer);

        Debug.Log(isGrounded + " " + bubberHeight);

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(20f * jumpForce * transform.up, ForceMode.Impulse);
        }

        if (!isGrounded) {
            rb.velocity += new Vector3(0f, -fallMultiplier, 0f);
        }
    }

    private void AlignMesh()
    {
        if (isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            meshTransform.up = hit.normal;
        }
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
