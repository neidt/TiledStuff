using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Author Natalie Eidt
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

    [Tooltip("The prefab of the projectile")]
    public GameObject fireballTemplate;

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
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.left, .5f, enemiesAndDoors);
            if (rayHit2D)
            {
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
            else { Debug.Log("nothing in range..."); }
        }
        if (playerMoveScript.isFacingRight)
        {
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.right, .5f, enemiesAndDoors);
            if (rayHit2D)
            {
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
            else { Debug.Log("nothing in range..."); }
        }
        if (playerMoveScript.isFacingUp)
        {
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.up, .5f, enemiesAndDoors);
            if (rayHit2D)
            {
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
            else { Debug.Log("nothing in range..."); }
        }

        if (playerMoveScript.isFacingDown)
        {
            RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.down, .5f, enemiesAndDoors);
            if (rayHit2D)
            {
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
            else { Debug.Log("nothing in range..."); }
        }

    }

    /// <summary>
    /// this function attacks the enemy in front of the long raycasting object
    /// and calls the enemy's takeDamage function
    /// </summary>
    public void RangedAttack()
    {
        playerAnimator.SetTrigger("attk");
        playerHealthScript.UseMana(rangedManaCost);

        if (playerMoveScript.isFacingUp || playerMoveScript.isMovingUp)
        {
            GameObject fireball = Instantiate(fireballTemplate, hitBox.transform);
            fireball.GetComponent<Fireball>().MoveFireball(transform.up);
        }

        if (playerMoveScript.isFacingDown || playerMoveScript.isMovingDown)
        {
            GameObject fireball = Instantiate(fireballTemplate, hitBox.transform);
            fireball.GetComponent<Fireball>().MoveFireball(-transform.up);
        }
        if (playerMoveScript.isFacingRight || playerMoveScript.isMovingRight)
        {
            GameObject fireball = Instantiate(fireballTemplate, hitBox.transform);
            fireball.GetComponent<Fireball>().MoveFireball(transform.right);
        }

        if (playerMoveScript.isFacingLeft || playerMoveScript.isMovingLeft)
        {
            GameObject fireball = Instantiate(fireballTemplate, hitBox.transform);
            fireball.GetComponent<Fireball>().MoveFireball(-transform.right);
        }

    }
}
