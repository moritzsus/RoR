using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Enemy opponent;

    private int health = 4;
    private readonly float movementSpeed = 5.0f;
    private readonly int damage = 1;
    private readonly float attackRange = 1.75f;
    private readonly float attackCooldown = 0.5f;

    private float horizontalInput = 0;
    private bool isDead = false;
    private bool canAttack = true;

    private Transform playerTransform;
    private Animator anim;

    private readonly string animBoolIsRunning = "isRunning";
    private readonly string animTriggerAttack = "triggerAttack";
    private readonly string animBoolIsDead = "isDead";

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager3.GetInstance().IsGameRunning())
            return;

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
            GameManager3.GetInstance().OnPlayerDied();
            GameManager3.GetInstance().SetIsGameRunning(false);
        }

        Debug.Log("Gladiator Health: " + health);
    }

}
