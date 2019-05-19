using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Author Natalie Eidt
public class EnemyRangedAi : MonoBehaviour
{
    [Tooltip("is enemy attacking?")]
    public bool isAttacking = false;

    [Tooltip("Is Player in range?")]
    public bool playerInRange = false;

    [Tooltip("The rate of attacks")]
    public float attackRate = 5f;

    [Tooltip("time till next attack")]
    public float timeTillAttack = 0;

    [Tooltip("the amount of damage the player does")]
    public float attackDamage = 10f;

    [Tooltip("layermask for raycast to find the player")]
    public LayerMask playerLayer;

    [Tooltip("enemy animator component")]
    public Animator enemyAnim;

    /// <summary>
    /// time till next attack
    /// </summary>
    private float nextAttack;

    /// <summary>
    /// the player char
    /// </summary>
    private Transform playerToAttack;

    [Header("linerenderer stuff")]
    public LineRenderer lr;


    private void Start()
    {
        playerToAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAnim = this.gameObject.GetComponent<Animator>();

    }

    private void Update()
    {
        if (playerInRange && Time.time >= nextAttack)
        {
            playerToAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        Collider2D[] playerHits = Physics2D.OverlapCircleAll(this.transform.position, 4f);

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

