using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("The maximum value of this object's health")]
    public float enemyMaxHealth = 20f;

    [Tooltip("The current health of this object")]
    public float enemyCurrentHealth;

    /// <summary>
    /// sets up the variables
    /// </summary>
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    /// <summary>
    /// this function damages this object
    /// </summary>
    /// <param name="damage"> the amount of damage to deal </param>
    public void TakeDamage(float damage)
    {
        if (enemyCurrentHealth > 0)
        {
            enemyCurrentHealth -= damage;
        }
        else
        {
            Die();
        }
    }

    /// <summary>
    /// this function kills this object
    /// </summary>
    public void Die()
    {
        //kills player
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 3f);
    }
}
