using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Tooltip("hitbox of the weapon")]
    public GameObject hitBox;

    [Tooltip("the amount of damage the player does")]
    public float playerMeleeDamage = 5f;

    [Tooltip("the amount of damage the player does")]
    public float playerRangedDamage = 1f;
    
    [Tooltip("Is the player using ranged attack?")]
    public bool isRangedAttacking = false;

    [Tooltip("Mana use values for ranged and melee attacks")]
    public float rangedManaCost = 2f;
    public float meleeManaCost = 5f;

    [Tooltip("layermask for raycast to find enemies and doors")]
    public LayerMask enemiesAndDoors;

    [Tooltip("Player animator component")]
    public Animator playerAnimator;

    /// <summary>
    /// ref to the movement script and health script
    /// </summary>
    private PlayerMove playerMoveScript;
    private PlayerHealth playerHealthScript;

    /// <summary>
    /// ref to the player object
    /// </summary>
    private GameObject player;

    private void Start()
    {
        playerMoveScript = this.gameObject.GetComponent<PlayerMove>();
        playerHealthScript = this.gameObject.GetComponent<PlayerHealth>();
        player = this.gameObject;

        ////linerenderer stuffs
        //lazer = this.gameObject.GetComponent<LineRenderer>();
        //lazer.enabled = true;
        //lazer.useWorldSpace = false;
    }

    /// <summary>
    /// checks for attacking input
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RangedAttack();
        }
    }

    /// <summary>
    /// this function attacks the enemy in front of the raycasting object
    /// and calls the enemy's takeDamage function
    /// </summary>
    public void BasicAttack()
    {
        playerAnimator.SetTrigger("attk");
        playerHealthScript.UseMana(meleeManaCost);

        if (playerMoveScript.isFacingLeft)
        {
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.left, .7f, enemiesAndDoors);
            if (rayHit2D.collider.tag == "Enemy")
            {
                Debug.Log("in range; staff hitting enemy; attacking");

                rayHit2D.transform.GetComponent<EnemyHealth>().TakeDamage(playerMeleeDamage);
            }

            else if (rayHit2D.collider.tag == "Door" || rayHit2D.collider.tag == "SturdyDoor")
            {
                Debug.Log("hitting door");

                rayHit2D.transform.GetComponent<Door>().TakeDamage(playerMeleeDamage);
            }
        }
        if (playerMoveScript.isFacingRight)
        {
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.right, .7f, enemiesAndDoors);
            if (rayHit2D.collider.tag == "Enemy")
            {
                Debug.Log("in range; staff hitting enemy; attacking");

                rayHit2D.transform.GetComponent<EnemyHealth>().TakeDamage(playerMeleeDamage);
            }

            else if (rayHit2D.collider.tag == "Door" || rayHit2D.collider.tag == "SturdyDoor")
            {
                Debug.Log("hitting door");

                rayHit2D.transform.GetComponent<Door>().TakeDamage(playerMeleeDamage);
            }
        }
        if (playerMoveScript.isFacingUp)
        {
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.up, .7f, enemiesAndDoors);
            if (rayHit2D.collider.tag == "Enemy")
            {
                Debug.Log("in range; staff hitting enemy; attacking");

                rayHit2D.transform.GetComponent<EnemyHealth>().TakeDamage(playerMeleeDamage);
            }
            else if (rayHit2D.collider.tag == "Door" || rayHit2D.collider.tag == "SturdyDoor")
            {
                Debug.Log("hitting door");

                rayHit2D.transform.GetComponent<Door>().TakeDamage(playerMeleeDamage);
            }
        }

        if (playerMoveScript.isFacingDown)
        {
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.down, .7f, enemiesAndDoors);
            if (rayHit2D.collider.tag == "Enemy")
            {
                Debug.Log("in range; staff hitting enemy; attacking");

                rayHit2D.transform.GetComponent<EnemyHealth>().TakeDamage(playerMeleeDamage);
            }
           else if(rayHit2D.collider.tag == "Door" || rayHit2D.collider.tag == "SturdyDoor")
            {
                Debug.Log("hitting door");

                rayHit2D.transform.GetComponent<Door>().TakeDamage(playerMeleeDamage);
            }
        }

    }

    /// <summary>
    /// this function attacks the enemy in front of the long raycasting object
    /// and calls the enemy's takeDamage function
    /// </summary>
    public void RangedAttack()
    {
        isRangedAttacking = true;

        //lazerStart = GameObject.Find("staff").GetComponent<Transform>();

        playerAnimator.SetTrigger("attk");
        playerHealthScript.UseMana(rangedManaCost);

        if (playerMoveScript.isMovingUp)
        {
            RaycastHit2D rayHit2DUp = Physics2D.Raycast(transform.position, Vector3.up, 2f, enemiesAndDoors);

            ////line renderer stuff
            //lazerStart = this.gameObject.transform;
            //lazer.SetPosition(0, lazerStart.position);
            //lazer.SetPosition(1, rayHit2DUp.transform.position);
            //lazer.enabled = true;
            
            if (rayHit2DUp.collider.tag == "Enemy")
            {
                Debug.Log("in range; beam hitting enemy up, ranged attacking");
                            
                rayHit2DUp.transform.GetComponent<EnemyHealth>().TakeDamage(playerRangedDamage);
            }
        }

        if (playerMoveScript.isMovingDown)
        {
            RaycastHit2D rayHit2DDown = Physics2D.Raycast(transform.position, Vector3.down, 2f, enemiesAndDoors);

            ////line renderer stuff
            //lazerStart = this.gameObject.transform;
            //lazer.SetPosition(0, lazerStart.position);
            //lazer.SetPosition(1, rayHit2DDown.transform.position);
            //lazer.enabled = true;

            if (rayHit2DDown.collider.tag == "Enemy")
            {
                Debug.Log("in range; beam hitting enemy down, ranged attacking");

                rayHit2DDown.transform.GetComponent<EnemyHealth>().TakeDamage(playerRangedDamage);
            }
        }
        if (playerMoveScript.isMovingRight)
        {
            RaycastHit2D rayHit2DRight = Physics2D.Raycast(transform.position, Vector3.right, 2f, enemiesAndDoors);

            ////line renderer stuff
            //lazerStart = this.gameObject.transform;
            //lazer.SetPosition(0,lazerStart.position);
            //lazer.SetPosition(1, rayHit2DRight.transform.position);
            //lazer.enabled = true;

            if (rayHit2DRight.collider.tag == "Enemy")
            {
                Debug.Log("in range; beam hitting enemy right, ranged attacking");

                rayHit2DRight.transform.GetComponent<EnemyHealth>().TakeDamage(playerRangedDamage);
            }
        }

        if (playerMoveScript.isMovingLeft)
        {
            RaycastHit2D rayHit2DLeft = Physics2D.Raycast(transform.position, Vector3.left, 2f, enemiesAndDoors);

            ////line renderer stuff
            //lazerStart = this.gameObject.transform;
            //lazer.SetPosition(0, lazerStart.position);
            //lazer.SetPosition(1, rayHit2DLeft.transform.position);
            //lazer.enabled = true;

            if (rayHit2DLeft.collider.tag == "Enemy")
            {
                Debug.Log("in range; beam hitting enemy left, ranged attacking");

                rayHit2DLeft.transform.GetComponent<EnemyHealth>().TakeDamage(playerRangedDamage);
            }
        }

    }

    /// <summary>
    /// this stops the ranged attack
    /// </summary>
    public void StopAttack()
    {
        isRangedAttacking = false;
    }
}
