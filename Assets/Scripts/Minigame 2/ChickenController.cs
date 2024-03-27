using System.Collections;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] private GameObject targetToChase;

    private bool eggStolen = false;
    private bool eggStolenCalled = false;
    private bool chasePlayer = false;
    private bool caughtPlayer = false;
    private bool fleeFromWolf = false;

    private readonly float caughtPlayerDistance = 0.8f;
    private readonly float defaultMovementSpeed = 7.85f;
    private readonly float maxFollowX = 310.0f;
    private readonly float deathHeight = -10f;

    private Animator anim;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private readonly string animTriggerAngry = "angry";
    private readonly string animBoolIsChasing = "isChasing";
    private readonly string animBoolIsPecking = "isPecking";

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < deathHeight)
        {
            Game2Manager.SetGameOver(true);
            Destroy(gameObject);
        }

        if (Game2Manager.GetEggStolen() && !eggStolen)
        {
            eggStolen = true;
        }

        if (eggStolen && !eggStolenCalled)
        {
            eggStolenCalled = true;
            StartCoroutine(HandleEggStolen());
        }

        if (chasePlayer)
        {
            if (Game2Manager.GetReachedFinish() && !fleeFromWolf)
            {
                chasePlayer = false;
                StartCoroutine(HandlePlayerInFinish());
                return;
            }
            ChasePlayer();
        }

        if (fleeFromWolf)
        {
            transform.position += defaultMovementSpeed * Time.fixedDeltaTime * Vector3.left;
        }
    }

    private IEnumerator HandleEggStolen()
    {
        anim.SetTrigger(animTriggerAngry);

        yield return new WaitForSeconds(0.9f);

        boxCollider.enabled = false;
        rigidBody.simulated = false;
        anim.SetBool(animBoolIsChasing, true);
        spriteRenderer.sortingLayerName = "Chicken";
        chasePlayer = true;
    }

    private IEnumerator HandlePlayerInFinish()
    {
        anim.SetBool(animBoolIsChasing, false);
        anim.SetTrigger(animTriggerAngry);

        yield return new WaitForSeconds(0.9f);

        fleeFromWolf = true;
        transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        anim.SetBool(animBoolIsChasing, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("At Player");
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x > maxFollowX)
            return;

        if (targetToChase != null && !caughtPlayer)
        {
            Vector3 diff = targetToChase.transform.position - transform.position;
            Vector3 direction = diff.normalized;
            float distance = diff.magnitude;
            transform.position += defaultMovementSpeed * Time.fixedDeltaTime * direction;

            if (targetToChase.transform.position.x < transform.position.x)
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            else
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            if (distance < caughtPlayerDistance)
            {
                anim.SetBool(animBoolIsPecking, true);
                caughtPlayer = true;
                boxCollider.enabled = true;
                rigidBody.simulated = true;
                Game2Manager.SetChickenCaughtPlayer(true);
            }
        }
    }
}
