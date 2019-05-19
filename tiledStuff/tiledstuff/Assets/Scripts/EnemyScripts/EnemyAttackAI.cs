using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Author Natalie Eidt
public class EnemyAttackAI : MonoBehaviour
{
    [Tooltip("is enemy attacking?")]
    public bool isAttacking = false;

    [Tooltip("Is Player in range?")]
    public bool playerInRange = false;

    [Tooltip("Speed of this enemy")]
    public float speed = 2f;

    [Tooltip("The rate of attacks")]
    public float attackRate = 1.5f;

    [Tooltip("time till next attack")]
    public float timeTillAttack = 0;

    [Tooltip("the amount of damage the player does")]
    public float attackDamage = 1f;

    [Tooltip("layermask for raycast to find the player")]
    public LayerMask playerLayer;

    [Tooltip("enemy animator component")]
    public Animator enemyAnim;
    /// <summary>
    /// time till next attack
    /// </summary>
    private float nextAttack;

    //chasing stuff

    /// <summary>
    /// player reference
    /// </summary>
    private Transform playerToChase;


    private void Start()
    {
        playerToChase = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(playerToChase == null)
        {
            playerToChase = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if (playerInRange && Time.time >= nextAttack)
        {
            Attack();
        }
        else if (!playerInRange && (Vector2.Distance(this.transform.position, playerToChase.transform.position) <= 2.5f))
        {
            transform.position = Vector2.MoveTowards(transform.position, playerToChase.transform.position, speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// attack the player if in range
    /// </summary>
    public void Attack()
    {
        isAttacking = true;
        nextAttack = Time.time + attackRate;

        Collider2D[] playerHits = Physics2D.OverlapCircleAll(this.transform.position, 1.5f);

        foreach (Collider2D collider in playerHits)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("hitting player; attacking ");

                enemyAnim.SetTrigger("attk");

                collider.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
            else
            {
                Debug.Log("enemy hitting nothing...");
            }
        }
    }

    /// <summary>
    /// checks for collision around the object
    /// </summary>
    /// <param name="other"> the other collider hitting our trigger </param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
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
