using System.Collections;
using UnityEngine;

public enum BubberState { IDLE, PLAY };

public class BubberController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private float speedScale;
    private int slowCount;
    private Vector3 ogVel;
    private float horizontalInput;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private LayerMask groundLayer;
    private float bubberHeight;
    private bool isGrounded;
    private bool canJump;

    [Header("Mesh")]
    [SerializeField] private Transform meshTransform;

    private BubberState state;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // speed
        speedScale = 1f;
        slowCount = 0;
        ogVel = Vector3.zero;
        horizontalInput = 0f;

        // jump
        bubberHeight = GetComponent<CapsuleCollider>().radius;

        EnterIdleState();

    }

    private void OnDisable()
    {
        EnterIdleState();
        EnableCursor();
    }

    private void Update()
    {
        if (state == BubberState.PLAY)
        {
            CheckInput();
            CapSpeed();
        }

        AlignMesh();

        if (Globals.introOccurring || Globals.isRestart)
        {
            EnterIdleState();
        } 
        else
        {
            EnterPlayState();
        }
    }

    private void FixedUpdate()
    {
        if (state == BubberState.PLAY)
        {
            Move();
            CheckSlow();
            Jump();
        }

        CheckGround();
    }

    private void CheckInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canJump = true;
        }
    }

    #region Move
    private void Move()
    {
        // forward movement
        Vector3 forwardDir = 20f * Vector3.forward;

        // sideways movement
        Vector3 sidewaysDir = Vector3.zero;
        if (Mathf.Abs(horizontalInput) > 0.5f)
        {
            sidewaysDir = 50f * horizontalInput * transform.right;
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
    #endregion

    #region Jump
    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, bubberHeight + 0.5f, groundLayer);

        if (!isGrounded) {
            rb.velocity += new Vector3(0f, -fallMultiplier, 0f);
        }
    }

    private void Jump()
    {
        if (isGrounded && canJump)
        {
            canJump = false;
            rb.AddForce(20f * jumpForce * meshTransform.up, ForceMode.Impulse);
        }
    }

    private Vector3 refVel = Vector3.zero;
    private void AlignMesh()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            meshTransform.up = Vector3.SmoothDamp(meshTransform.up, hit.normal, ref refVel, isGrounded ? 0.1f : 0.35f);
        }
    }
    #endregion

    #region Slow
    public void Slow(float slowMultiplier)
    {
        speedScale = 1f - slowMultiplier;
        StartCoroutine(ISlow());
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

    IEnumerator ISlow()
    {
        slowCount++;

        yield return new WaitForSeconds(1.5f);

        slowCount--;
    }
    #endregion

    #region States
    public void EnterPlayState()
    {
        if (Time.timeScale != 0f)
        {
            DisableCursor();
        }
        state = BubberState.PLAY;
    }

    public void EnterIdleState()
    {
        state = BubberState.IDLE;
        rb.velocity = Vector3.zero;
        EnableCursor();
    }

    private void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion
}
