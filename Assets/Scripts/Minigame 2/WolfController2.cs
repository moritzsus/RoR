using System.Collections;
using UnityEngine;

public class WolfController2 : MonoBehaviour
{
    private bool playerreachedFinish = false;
    private int roarCount = 3;
    private float roarDelay = 1.3f;

    private Animator anim;

    private readonly string animTriggerRoar = "roar";

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Game2Manager.GetInstance().GetReachedFinish() && !playerreachedFinish)
        {
            playerreachedFinish = true;
            StartCoroutine(Roar());
        }
    }

    private IEnumerator Roar()
    {
        for (int i = 0; i < roarCount; i++)
        {
            anim.SetTrigger(animTriggerRoar);
            yield return new WaitForSeconds(roarDelay);
        }
    }
}
