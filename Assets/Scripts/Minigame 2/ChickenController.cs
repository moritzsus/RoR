using System.Collections;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] private GameObject targetToChase;

    private bool eggStolen = false;
    private bool eggStolenCalled = false;
    private bool chasePlayer = false;
    private readonly float defaultMovementSpeed = 7.7f;

    private Animator anim;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private readonly string animTriggerAngry = "angry";
    private readonly string animBoolIsChasing = "isChasing";

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
            ChasePlayer();
        }
    }

    private IEnumerator HandleEggStolen()
    {
        anim.SetTrigger(animTriggerAngry);

        yield return new WaitForSeconds(0.9f);

        boxCollider.enabled = false;
        rigidBody.simulated = false;
        anim.SetTrigger(animBoolIsChasing);
        spriteRenderer.sortingLayerName = "Chicken";
        chasePlayer = true;
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
        if (targetToChase != null)
        {
            Vector3 direction = (targetToChase.transform.position - transform.position).normalized;
            transform.position += defaultMovementSpeed * Time.fixedDeltaTime * direction;
        }
    }
}
