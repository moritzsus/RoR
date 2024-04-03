using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGreekController : MonoBehaviour
{
    private readonly float movementSpeed = 5.0f;
    public bool isIdle = false;
    private string animIdle = "Idle";

    private int counter = 0;

    public float horizontalInput = -1;

    public float lastHoritzontalInput = 1;
    public float verticalInput = 1;
    private Transform playerTransform;

    private float maxLeft = -72;
    private float maxRight = 14;

    private float maxDown = -7;
    private float maxUp = -5.6f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            isIdle = false;
            anim.SetBool(animIdle, false);
        }
        else
        {
            isIdle = true;
            anim.SetBool(animIdle, true);
            if(lastHoritzontalInput !=0)
            playerTransform.localScale = new Vector3(lastHoritzontalInput, 1, 1);
        }
        if (!isIdle)
        {
            HandleMovement();
        }

    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0)
        {
            playerTransform.localScale = new Vector3(horizontalInput, 1, 1);
            if (IsInBoundaries(playerTransform.position.x + (horizontalInput * Time.deltaTime * movementSpeed), playerTransform.position.y))
            {
                playerTransform.position += Time.deltaTime * movementSpeed * new Vector3(horizontalInput, 0, 0);
            }
            if (IsinHorse(playerTransform.position.x, playerTransform.position.y))
            {
                ResetToStart();
            }
            this.lastHoritzontalInput = -horizontalInput;
        }
        if (verticalInput != 0)
        {
            playerTransform.localScale = new Vector3(-lastHoritzontalInput, 1, 1);
            if (IsInBoundaries(playerTransform.position.x, playerTransform.position.y + (verticalInput * Time.deltaTime * movementSpeed)))
            {
                playerTransform.position += Time.deltaTime * movementSpeed * new Vector3(0, verticalInput, 0);
            }
            if (IsinHorse(playerTransform.position.x, playerTransform.position.y))
            {
                ResetToStart();
            }
        }
    }

    private bool IsInBoundaries(float x, float y)
    {
        if (x > maxLeft && x < maxRight && y > maxDown && y < maxUp)
            return true;
        else return false;
    }

    private bool IsinHorse(float x, float y)
    {
        if (x > -4.2 && x < 2.6 && y > -6.1f)
            return true;
        else return false;
    }

    private void ResetToStart(){
        counter++;
        if(counter<1){
        playerTransform.position = new Vector3(-72, -6.75f, 0);
        }
    }
        internal int GetGreekCounter()
    {
        return counter;
    }

}

