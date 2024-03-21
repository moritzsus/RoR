using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private int health = 3;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 1.75f;
    [SerializeField] private Enemy opponent;

    private Transform playerTransform;
    private SpriteRenderer playerSprite;
    private Animator anim;
    private float horizontalInput = 0;
    private bool isDead = false;
    private bool canAttack = true;
    private readonly float attackCooldown = 0.5f;

    private readonly string animBoolIsRunning = "isRunning";
    private readonly string animTriggerAttack = "triggerAttack";
    private readonly string animBoolIsDead = "isDead";

    // Start is called before the first frame update
    void Start()
    {
        if (opponent == null)
        {
            Debug.LogWarning("opponent null: Assign opponent field for player in the editor");
        }
        playerTransform = gameObject.GetComponent<Transform>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

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
        anim.SetBool(animBoolIsRunning, horizontalInput != 0);

        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            anim.SetTrigger(animTriggerAttack);
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false;
        Attack();

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Attack()
    {
        // player has to look to the enemy
        if (playerTransform.localScale.x != 1)
            return;

        Vector3 opponentDirection = opponent.transform.position - playerTransform.position;
        float distance = opponentDirection.magnitude;

        if (distance < attackRange)
        {
            opponent.RecieveDamage(damage);
        }
    }

    public void RecieveDamage(int amount)
    {
        health -= amount;
        if (health < 1)
        {
            anim.SetBool(animBoolIsDead, true);
            isDead = true;
        }

        Debug.Log("Gladiator Health: " + health);
    }
}
