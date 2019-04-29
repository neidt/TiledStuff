using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Tooltip("hitbox of the weapon")]
    public GameObject hitBox;

    [Tooltip("the amount of damage the player does")]
    public float playerDamage = 5f;

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
            Attack();
        }
    }

    /// <summary>
    /// this function attacks the enemy in front of the raycasting object
    /// and calls the enemy's takeDamage function
    /// </summary>
    public void Attack()
    {
        RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector3.left, .7f, enemies);
        if (rayHit2D.collider.tag == "Enemy")
        {
            Debug.Log("in range; staff hitting enemy; attacking");

            playerAnimator.SetTrigger("attk");

            rayHit2D.transform.GetComponent<EnemyHealth>().TakeDamage(playerDamage);
        }
    }
}
