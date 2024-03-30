using System.Collections;
using UnityEngine;

public class GuardModulController : MonoBehaviour
{
    public float speed = 5f;
    public float maxLeft;
    public float maxRight;

    float richtung = -1;
    public bool isIdle = false;
    private Animator anim;

    private string animIdle = "Idle";
    private string animAlerted = "Alerted"; // The name of the boolean parameter in the animator controller

    public bool wasSeen = false; // Variable to track if the guard was seen

    public GameObject alertObject; // Reference to the Alert child object

    private bool alertedAnimationPlayed = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (alertObject != null)
        {
            alertObject.SetActive(false); // Initially hide the alert object
        }
    }

    void FixedUpdate()
    {
        if (alertedAnimationPlayed)
        {
            anim.SetBool(animIdle, false); // Set the idle animation to false
            return;
        }

        // Check if the guard was seen (you can replace this logic with your own)
        if (CheckIfSeen())
        {
            wasSeen = true;
        }

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
        if (!isIdle && !wasSeen)
        {
            this.speed = UnityEngine.Random.Range(5, 8);
            transform.Translate(Vector2.right * speed * richtung * Time.deltaTime * 0.2f);
        }
        // Set the animator parameter based on the condition
        anim.SetBool(animAlerted, isIdle && wasSeen);

        // Activate/deactivate the alert object based on the condition
        if (alertObject != null)
        {
            alertObject.SetActive(isIdle && wasSeen);
            if (isIdle && wasSeen && !alertedAnimationPlayed)
            {
                PlayAlertAnimation();
            }
        }
    }

    // Method to simulate the logic of being seen
    private bool CheckIfSeen()
    {
        // You can implement your own logic here to determine if the guard was seen
        // For demonstration purposes, let's say the guard is seen if the player is within a certain distance
        GameObject player = GameObject.Find("Greek");
        if (wasSeen) return true;
        if (player != null)
        {
            if (player.transform.position.x < maxRight && player.transform.position.x > maxLeft && isIdle)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator IdleRightCoroutine()
    {
        anim.SetBool(animIdle, true); // Set the idle animation to true
        isIdle = true;
        float tempRichtung = richtung;
        richtung = 0;
        yield return new WaitForSeconds(2.0f);
        richtung = tempRichtung * -1;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        anim.SetBool(animIdle, false); // Set the idle animation to false
        isIdle = false;

    }

    private void PlayAlertAnimation()
    {
        if (alertObject != null)
        {
            Animator alertAnimator = alertObject.GetComponent<Animator>();
            if (alertAnimator != null)
            {
                alertAnimator.SetBool(animAlerted, true);
                alertedAnimationPlayed = true;
            }
        }
    }
}
