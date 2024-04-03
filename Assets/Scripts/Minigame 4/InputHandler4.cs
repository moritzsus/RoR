using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputHandler4 : MonoBehaviour
{
    [SerializeField] private GameObject axe;

    public Transform throwStartPosition;
    public float throwSpeed = 5f;
    private float xFromCenter = 13.75f;
    private float yFromCenter = 7.79f;

    [SerializeField] private GameObject gameManager;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if (GameManager4.gameFinished)
            return;

        GameManager4 gameManagerScript = gameManager.GetComponent<GameManager4>();
        if (!gameManagerScript.GetAllowedToThrow())
            return;
        if (!context.started) return;


        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        Axe axe = ThrowAxe(Mouse.current.position.ReadValue());
        gameManagerScript.ThrowAxe();
        if (rayHit.collider)
            axe.SetTargetObject(rayHit.collider.gameObject);
        if (!rayHit.collider) return;

        GameManager4.IncreaseGuardsHit();
    }
    private Axe ThrowAxe(Vector3 targetPosition)
    {        
        float xFactor = targetPosition.x / Screen.width;
        float yFactor = targetPosition.y / Screen.height;
        xFactor *= 2;
        xFactor -= 1;
        yFactor *= 2;
        yFactor -= 1;
        float xDir = xFromCenter * xFactor;
        float yDir = yFromCenter * yFactor;


        GameObject axeInstance = GameManager4.GetCurrentAxe();
        Axe axeScript = axeInstance.GetComponent<Axe>();
        axeScript.SetTarget(new Vector3(xDir, yDir, 0));
        return axeScript;
    }
}
