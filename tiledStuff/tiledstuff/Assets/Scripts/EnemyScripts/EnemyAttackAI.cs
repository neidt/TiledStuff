using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{

    public bool isAttacking = false;

    public bool playerInRange = false;

    public float attackRate = 1f;

    private float nextAttack;

    [Tooltip("time till next attack")]
    public float timeTillAttack = 0;

    [Tooltip("the amount of damage the player does")]
    public float attackDamage =1f;

    [Tooltip("layermask for raycast to find the player")]
    public LayerMask playerLayer;

    private void Update()
    {
        if (playerInRange && Time.time > nextAttack)
        {
            Attack();
        }
    }

    /// <summary>
    /// attack the player if in range
    /// </summary>
    public void Attack()
    {
        isAttacking = true;
        nextAttack = Time.time + attackRate;

        RaycastHit2D rayHitLeft = Physics2D.Raycast(transform.position, Vector3.left, 1.5f);
        RaycastHit2D rayHitRight = Physics2D.Raycast(transform.position, Vector3.right, 1.5f);

        if (rayHitLeft.collider.tag == "Player")
        {
            Debug.Log("in range; player on the left; attacking ");

            //enemyAnimator.SetTrigger("attk");

            rayHitLeft.transform.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        if (rayHitRight.collider.tag == "Player")
        {
            Debug.Log("in range; player on the right; attacking ");

            //enemyAnimator.SetTrigger("attk");

            rayHitRight.transform.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    
    /// <summary>
    /// checks for collision around the object
    /// </summary>
    /// <param name="other"> the other collider hitting our trigger </param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    /// <summary>
    /// checks if player in range
    /// </summary>
    /// <param name="other"> the thing hitting us </param>
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }
    /// <summary>
    /// checks for collisions on our trigger
    /// </summary>
    /// <param name="other"> the thing hitting us </param>
    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
    }
}
