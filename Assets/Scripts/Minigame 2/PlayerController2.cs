using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private readonly float movementSpeed = 7.5f;
    private readonly float jumpForce = 7.0f;

    private bool isPlayerInAir = false;
    private bool isFalling = false;
    private float horizontalInput = 0;
    private float lastY;

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;
    private Animator anim;

    private readonly string groundTag = "Ground";
    private readonly string animBoolIsRunning = "isRunning";
    private readonly string animBoolIsJumping = "isJumping";
    private readonly string animBoolIsFalling = "isFalling";

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        lastY = playerTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        lastY = playerTransform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isPlayerInAir = false;
            isFalling = false;
            anim.SetBool(animBoolIsJumping, false);
            anim.SetBool(animBoolIsFalling, false);
        }
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            playerTransform.position += Time.deltaTime * movementSpeed * new Vector3(horizontalInput, 0, 0);
            playerTransform.localScale = new Vector3(horizontalInput, 1, 1);
            anim.SetBool(animBoolIsRunning, true);
        }
        else
        {
            anim.SetBool(animBoolIsRunning, false);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayerInAir)
        {
            isPlayerInAir = true;
            anim.SetBool(animBoolIsJumping, true);
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Falling
        if (playerTransform.position.y < lastY)
        {
            if (!isFalling)
            {
                isFalling = true;
                anim.SetBool(animBoolIsFalling, true);
            }
        }
    }
}
