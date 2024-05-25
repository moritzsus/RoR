using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerController opponent;

    private int health = 15;
    private readonly float movementSpeed = 5.0f;
    private readonly int damage = 1;
    private readonly float attackRange = 1.75f;
    private readonly float tryAttackRange = 2.25f;
    private readonly float turnAroundRange = 1.0f;
    private readonly float movementCooldown = 0.5f;
    private readonly float attackCooldown = 1.4f;
    private readonly float skipAttackCooldown = 0.3f;

    private float distance = 0;
    private int runDirection = 0;
    private bool movementActive = false;
    private bool isDead = false;
    private bool canAttack = true;

    private Transform enemyTransform;
    private Animator anim;

    private readonly string animBoolIsRunning = "isRunning";
    private readonly string animTriggerAttack = "triggerAttack";
    private readonly string animBoolIsDead = "isDead";

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        enemyTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager3.GetInstance().IsGameRunning())
            return;

        if (isDead)
            return;

        HandleMovement();

        if (canAttack)
            HandleAttacking();
    }

    private void HandleMovement()
    {
        // Move depending on current movementDirection
        enemyTransform.position += Time.deltaTime * movementSpeed * new Vector3(runDirection, 0, 0);
        enemyTransform.localScale = new Vector3(runDirection == 0 ? 1 : -runDirection, 1, 1);
        anim.SetBool(animBoolIsRunning, runDirection != 0);

        if (movementActive)
            return;

        Vector3 opponentDirection = opponent.transform.position - enemyTransform.position;
        distance = opponentDirection.magnitude;

        if (distance < turnAroundRange)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
            {
                StartCoroutine(TurnAroundCoroutine());
            }
            else
            {
                StartCoroutine(StayCoroutine());
            }
            return;
        }

        int rand10 = Random.Range(0, 10);
        float getCloserProbability = rand10 * distance;
        if (getCloserProbability > 25)
        {
            StartCoroutine(CloseDistanceCoroutine());
        }
        else
        {
            StartCoroutine(StayCoroutine());
        }
    }

    private IEnumerator TurnAroundCoroutine()
    {
        runDirection = 1;
        movementActive = true;
        yield return new WaitForSeconds(movementCooldown);

        runDirection = 0;
        movementActive = false;
    }

    private IEnumerator StayCoroutine()
    {
        movementActive = true;
        yield return new WaitForSeconds(movementCooldown);
        movementActive = false;
    }

    private IEnumerator CloseDistanceCoroutine()
    {
        runDirection = -1;
        movementActive = true;
        yield return new WaitForSeconds(movementCooldown / 2);

        runDirection = 0;
        movementActive = false;
    }

    private void HandleAttacking()
    {
        Vector3 opponentDirection = opponent.transform.position - enemyTransform.position;
        distance = opponentDirection.magnitude;

        if (distance < tryAttackRange)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
            {
                StartCoroutine(AttackCoroutine());
            }
            else
            {
                StartCoroutine(SkipAttackCoroutine());
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false;
        anim.SetTrigger(animTriggerAttack);
        Attack();

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator SkipAttackCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(skipAttackCooldown);
        canAttack = true;
    }

    private void Attack()
    {
        // enemy has to look to the player
        if (enemyTransform.localScale.x != 1)
            return;

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
            GameManager3.GetInstance().OnPlayerWon();
            GameManager3.GetInstance().SetIsGameRunning(false);
        }

        Debug.Log("Enemy Health: " + health);
    }
}
