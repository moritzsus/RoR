using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private readonly float movementSpeed = 7.5f;
    private readonly float jumpForce = 23.0f;
    private readonly float finishLine = 316.0f;
    private readonly float deathHeight = -10f;

    private float groundCheckDistance;
    private bool jumpInputBuffer = false;
    private bool isPlayerInAir = false;
    private float jumpeTime;
    private bool isFalling = false;
    private float horizontalInput = 0;
    private float lastY;

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;
    private Animator anim;
    private SpriteRenderer playerSr;
    private LayerMask groundLayer;

    private readonly string animBoolIsRunning = "isRunning";
    private readonly string animBoolIsJumping = "isJumping";
    private readonly string animBoolIsFalling = "isFalling";

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        playerSr = gameObject.GetComponent<SpriteRenderer>();
        groundCheckDistance = (playerSr.bounds.size.y / 2.0f) + 0.05f;
        groundLayer = LayerMask.GetMask("Ground");

        lastY = playerTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // if only FixedUpdate checks for KeyDown it may skip some inputs because it does not run every frame
        if (Input.GetKeyDown(KeyCode.Space) && !jumpInputBuffer && !isPlayerInAir)
        {
            jumpInputBuffer = true;
        }
    }

    private void FixedUpdate()
    {
        if (!Game2Manager.GetGameOver() && !Game2Manager.GetReachedFinish())
        {
            HandleMovement();
            if (transform.position.x > finishLine)
                Game2Manager.SetReachedFinish(true);
            if (transform.position.y < deathHeight)
            {
                Game2Manager.SetGameOver(true);
                Destroy(gameObject);
            }
        }
        if (Game2Manager.GetGameOver())
        {
            // TODO menu -> restart
            Debug.Log("Game over!");
        }
        if (Game2Manager.GetReachedFinish())
        {
            anim.SetBool(animBoolIsRunning, false);
            anim.SetBool(animBoolIsJumping, false);
            anim.SetBool(animBoolIsFalling, false);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        lastY = playerTransform.position.y;
    }


    private void HandleMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            playerTransform.position += Time.fixedDeltaTime * movementSpeed * new Vector3(horizontalInput, 0, 0);
            playerTransform.localScale = new Vector3(horizontalInput, 1, 1);
            anim.SetBool(animBoolIsRunning, true);
        }
        else
        {
            anim.SetBool(animBoolIsRunning, false);
        }

        // Jumping
        if (jumpInputBuffer && !isPlayerInAir)
        {
            isPlayerInAir = true;
            jumpeTime = Time.time;
            jumpInputBuffer = false;
            anim.SetBool(animBoolIsJumping, true);
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Falling
        if (playerTransform.position.y < lastY)
        {
            if (!isFalling)
            {
                isFalling = true;
                isPlayerInAir = true;
                anim.SetBool(animBoolIsFalling, true);
                anim.SetBool(animBoolIsJumping, false);
            }
        }
        else
        {
            if (isFalling)
            {
                isFalling = false;
                isPlayerInAir = false;
                anim.SetBool(animBoolIsFalling, false);
            }
        }

        // grounded
        if ((isPlayerInAir && isFalling) || (isPlayerInAir && Time.time > jumpeTime + 0.5))
        {
            RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.down, groundCheckDistance, groundLayer);
            if (hit.collider != null)
            {
                isPlayerInAir = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("At Player P");
        }
    }
}
