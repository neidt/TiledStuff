using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAi : MonoBehaviour
{
    [Tooltip("is enemy attacking?")]
    public bool isAttacking = false;

    [Tooltip("Is Player in range?")]
    public bool playerInRange = false;
    
    [Tooltip("The rate of attacks")]
    public float attackRate = 1f;

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

    private Transform playerToAttack;

    private void Start()
    {
        playerToAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAnim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if ((Vector2.Distance(this.transform.position, playerToAttack.transform.position) <= 6f && Time.time >= nextAttack))
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
        nextAttack = Time.time - attackRate;

        RaycastHit2D rayHitLeft = Physics2D.Raycast(transform.position, Vector3.left, 4f);
        RaycastHit2D rayHitRight = Physics2D.Raycast(transform.position, Vector3.right, 4f);

        if (rayHitLeft.collider.tag == "Player")
        {
            Debug.Log("in range; player on the left; attacking ");

            enemyAnim.SetTrigger("attk");

            rayHitLeft.transform.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        if (rayHitRight.collider.tag == "Player")
        {
            Debug.Log("in range; player on the right; attacking ");

            enemyAnim.SetTrigger("attk");

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
    /// checks for collisions on our trigger
    /// </summary>
    /// <param name="other"> the thing hitting us </param>
    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
    }
}

