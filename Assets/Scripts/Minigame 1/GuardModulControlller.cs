using System.Collections;
using UnityEngine;

public class GuardModulController : MonoBehaviour
{
    public float speed = 5f;
    public float maxLeft = -6.5f;
    public float maxRight = -2.5f;
    float richtung  = -1;
    public bool isIdle = false;
    private Animator anim;
    private string animIdle = "Idle";

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        if (!isIdle && transform.localPosition.x > maxRight)
        {
            transform.localPosition = new Vector3(maxRight - 0.1f, transform.localPosition.y, transform.localPosition.z);
            StartCoroutine(IdleRightCoroutine());
        }
        else if (!isIdle && transform.localPosition.x < maxLeft)
        {
            transform.localPosition = new Vector3(maxLeft + 0.1f, transform.localPosition.y, transform.localPosition.z);
            StartCoroutine(IdleRightCoroutine());
        }
        if (!isIdle)
            this.speed = Random.Range(5, 8);
            transform.Translate(Vector2.right * speed * richtung * Time.deltaTime * 0.2f);
    }

    private IEnumerator IdleRightCoroutine()
    {
        anim.SetBool(animIdle, true);
        isIdle = true;
        float tempRichtung = richtung;
        richtung = 0;
        yield return new WaitForSeconds(2.0f);
        richtung = tempRichtung * -1;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        anim.SetBool(animIdle, false);
        isIdle = false;
    }
}