using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private int health = 15;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 1.75f;
    [SerializeField] private float tryAttackRange = 2.5f;
    [SerializeField] private float turnAroundRange = 1.0f;
    [SerializeField] private PlayerController opponent;

    private Transform enemyTransform;
    private Animator anim;
    private bool movementActive = false;
    private readonly float movementCooldown = 0.5f;
    private bool isDead = false;
    private bool canAttack = true;
    private float distance = 0;
    private int runDirection = 0;
    private readonly float attackCooldown = 2.0f;
    private readonly float skipAttackCooldown = 0.4f;

    private readonly string animBoolIsRunning = "isRunning";
    private readonly string animTriggerAttack = "triggerAttack";
    private readonly string animBoolIsDead = "isDead";

    // Start is called before the first frame update
    void Start()
    {
        if (opponent == null)
        {
            Debug.LogWarning("opponent null: Assign opponent field for enemy in the editor");
        }
        anim = gameObject.GetComponent<Animator>();
        enemyTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
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
                Debug.Log("TA");
                StartCoroutine(TurnAroundCoroutine());
            }
            else
            {
                Debug.Log("Stay");
                StartCoroutine(StayCoroutine());
            }
            return;
        }

        int rand10 = Random.Range(0, 10);
        float getCloserProbability = rand10 * distance;
        if (getCloserProbability > 25)
        {
            Debug.Log("CC");
            StartCoroutine(CloseDistanceCoroutine());
        }
        else
        {
            Debug.Log("Stay");
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
                Debug.Log("Attack");
                StartCoroutine(AttackCoroutine());
            }
            else
            {
                Debug.Log("Skip");
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
        // TODO ändern zu flipX
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
        }

        Debug.Log("Enemy Health: " + health);
    }
}
