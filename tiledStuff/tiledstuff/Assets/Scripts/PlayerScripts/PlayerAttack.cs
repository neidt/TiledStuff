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

    [Tooltip("layermask for raycast to find enemies")]
    public LayerMask enemies;

    [Tooltip("Player animator component")]
    public Animator playerAnimator;

    /// <summary>
    /// checks for attacking input
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BasicAttack();
        }
        if (Input.GetKey(KeyCode.R))
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
        RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.left, .7f, enemies);
        if (rayHit2D.collider.tag == "Enemy")
        {
            Debug.Log("in range; staff hitting enemy; attacking");

            playerAnimator.SetTrigger("attk");

            rayHit2D.transform.GetComponent<EnemyHealth>().TakeDamage(playerMeleeDamage);
        }
    }

    /// <summary>
    /// this function attacks the enemy in front of the long raycasting object
    /// and calls the enemy's takeDamage function
    /// </summary>
    public void RangedAttack()
    {
        RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.left, 3f, enemies);
        if(rayHit2D.collider.tag == "Enemy")
        {
            Debug.Log("in range; beam hitting enemy, ranged attacking");

            playerAnimator.SetTrigger("attk");
            rayHit2D.transform.GetComponent<EnemyHealth>().TakeDamage(playerRangedDamage);
        }
    }
}
