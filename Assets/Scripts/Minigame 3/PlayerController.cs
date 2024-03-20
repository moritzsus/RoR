using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;

    private Transform playerTransform;
    private SpriteRenderer playerSprite;
    private Animator anim;
    private float horizontalInput = 0;

    private string animIsRunning = "isRunning";

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        AnimatePlayer();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            playerTransform.position += Time.deltaTime * movementSpeed * new Vector3(horizontalInput, 0, 0);
            playerTransform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    private void AnimatePlayer()
    {
        anim.SetBool(animIsRunning, horizontalInput != 0);
    }
}
