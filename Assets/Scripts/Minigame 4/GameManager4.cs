using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager4 : MonoBehaviour
{
    private static readonly float throwCooldown = 1f;
    [SerializeField] private GameObject axe;
    private static GameObject currentAxe;
    private static bool isThrowing = false;
    private static bool allowToThrow = true;
    private static GameManager4 instance;

    private static int guardsHit = 0;
    private static int guardsEscaped = 0;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        currentAxe = Instantiate(axe, new Vector3(0, -8, 0), Quaternion.identity);
    }

    public static void IncreaseGuardsHit()
    {
        guardsHit++;
        //Debug.Log("Hit: " + guardsHit);
    }
    public static void IncreaseGuardsEscaped()
    {
        guardsEscaped++;
        //Debug.Log("Escaped: " + guardsEscaped);
    }
    public static GameObject GetCurrentAxe()
    {
        return currentAxe;
    }
    public static bool GetIsThrowing()
    {
        return isThrowing;
    }
    public void ThrowAxe()
    {
        StartCoroutine(ThrowDelay());
    }
    private IEnumerator ThrowDelay()
    {
        isThrowing = true;
        allowToThrow = false;
        yield return new WaitForSeconds(throwCooldown);
        currentAxe = Instantiate(axe, new Vector3(0, -8, 0), Quaternion.identity);
        allowToThrow = true;
        isThrowing = false;
    }
    public bool GetAllowedToThrow()
    {
        return allowToThrow;
    }
}
