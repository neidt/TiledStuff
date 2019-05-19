using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    [Tooltip("is enemy attacking?")]
    public bool isAttacking = false;

    [Tooltip("Is Player in range?")]
    public bool playerInRange = false;

    [Tooltip("Speed of this enemy")]
    public float speed = 2f;

    [Tooltip("The rate of attacks")]
    public float attackRate = 1f;

    [Tooltip("time till next attack")]
    public float timeTillAttack = 0;

    [Tooltip("the amount of damage the player does")]
    public float attackDamage = 1f;

    [Tooltip("layermask for raycast to find the player")]
    public LayerMask playerLayer;

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

        if ((Vector2.Distance(this.transform.position, playerToChase.transform.position) <= 1.5f && Time.time >= nextAttack))
        {
            Attack();
        }
        else if (Vector2.Distance(this.transform.position, playerToChase.transform.position) >= 1.5f &&
            (Vector2.Distance(this.transform.position, playerToChase.transform.position) <= 3f))
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
        if (other.tag == "Player")
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
