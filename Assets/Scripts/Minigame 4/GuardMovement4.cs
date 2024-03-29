using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement4 : MonoBehaviour
{
    private float movementSpeed;
    private bool moveRight = true;
    private float destroyLeft = -14.65f;
    private float destroyRight = 14.65f;
    private float destroyRightTopLevel = -3.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < destroyLeft)
        {
            GameManager4.IncreaseGuardsEscaped();
            Destroy(gameObject);
        }
        if (transform.position.y > 2.4f && transform.position.y < 2.6f)
        {
            if(transform.position.x > destroyRightTopLevel)
            {
                GameManager4.IncreaseGuardsEscaped();
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.x > destroyRight)
            {
                GameManager4.IncreaseGuardsEscaped();
                Destroy(gameObject);
            }
        }

        transform.position += movementSpeed * Time.fixedDeltaTime * Vector3.right;
    }

    public void SetMoveRight(bool val) 
    {
        moveRight = val;
        if (moveRight)
        {
            movementSpeed = Random.Range(4f, 7f);
        }
        else
        {
            movementSpeed = Random.Range(-4f, -7f);
        }
    }
}
