using System.Collections;
using UnityEngine;

public class GuardLeftController : MonoBehaviour
{
    public float speed = 5f;
    public float maxLeft = -12.5f;
    public float maxRight = -5f;
    float richtung  = -1;
    public bool isIdle = false;
    private Animator anim;
    private string animIdle = "Idle";

    void Start(){
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate(){
    
    if(!isIdle && transform.position.x > maxRight)
    {
        transform.position = new Vector3(maxRight - 0.1f, transform.position.y, transform.position.z);
        StartCoroutine(IdleLeftCoroutine());
    }
    else if (!isIdle && transform.position.x < maxLeft){
        transform.position = new Vector3(maxLeft + 0.1f, transform.position.y, transform.position.z);
        StartCoroutine(IdleLeftCoroutine());
    }
    if(!isIdle)
        transform.Translate(Vector2.right * speed * richtung * Time.deltaTime*0.2f);
    }

    private IEnumerator IdleLeftCoroutine() {
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