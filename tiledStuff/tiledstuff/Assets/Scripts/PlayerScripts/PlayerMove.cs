using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("the player's base move speed")]
    public float speed = 5f;

    [Tooltip("layermask for the direction checking raycast")]
    public LayerMask walls;

    [Tooltip("the length of the raycast2d")]
    public float rayLength = .5f;

    [Tooltip("Player animator component")]
    public Animator playerAnimator;

    [Tooltip("bools for movement buttons")]
    public bool isMovingRight = false;
    public bool isMovingLeft = false;
    public bool isMovingUp = false;
    public bool isMovingDown = false;


    void Update()
    {
        if (isMovingRight)
        {
            MoveRight();
        }
        if (isMovingLeft)
        {
            MoveLeft();
        }
        if (isMovingUp)
        {
            MoveUp();
        }
        if (isMovingDown)
        {
            MoveDown();
        }
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        playerAnimator.SetTrigger("running");
    //        speed += 2f;
    //    }

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        MoveUp();
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        MoveDown();
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        MoveLeft();

    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        MoveRight();
    //    }
    }

    #region eh
    public void SetMovingRight()
    {
        StopMoving();
        isMovingRight = true;
    }

    public void SetMovingLeft()
    {
        StopMoving();
        isMovingLeft = true;
    }

    public void SetMovingDown()
    {
        StopMoving();
        isMovingDown = true;
    }

    public void SetMovingUp()
    {
        StopMoving();
        isMovingUp = true;
    }
    
    public void StopMoving()
    {
        isMovingUp = false;
        isMovingDown = false;
        isMovingRight = false;
        isMovingLeft = false;
    }
    #endregion

    public void MoveRight()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, rayLength, walls).collider == null && isMovingRight)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void MoveLeft()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, rayLength, walls).collider == null && isMovingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void MoveDown()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, rayLength, walls).collider == null && isMovingDown)
            transform.position += Vector3.down * Time.deltaTime * speed;
    }
    
    public void MoveUp()
    {
        if (Physics2D.Raycast(transform.position, Vector3.up, rayLength, walls).collider == null && isMovingUp)
            transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
