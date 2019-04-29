using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("the player's max health")]
    public float playerMaxHealth = 100;

    [Tooltip("the player's current health")]
    public float playerCurrentHealth;

    [Tooltip("Player animator component")]
    public Animator playerAnimator;
    
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    /// <summary>
    /// this function damages the player 
    /// </summary>
    /// <param name="damage"> the amount of damage to deal </param>
    public void TakeDamage(float damage)
    {
        if(playerCurrentHealth > 0)
        {
            playerAnimator.SetTrigger("takedmg");
            playerCurrentHealth -= damage;
        }
        else
        {
            Die();
        }
    }
    /// <summary>
    /// this function heals the player
    /// usually called by health restore items
    /// </summary>
    /// <param name="amount"> the amount of health to give the player </param>
    public void HealPlayer(float amount)
    {
        if (playerCurrentHealth < playerMaxHealth)
        {
            playerCurrentHealth += amount;
        }
        else if(playerCurrentHealth >= playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
    }

    /// <summary>
    /// this function kills the player
    /// </summary>
    public void Die()
    {
        //kills player
        playerAnimator.SetTrigger("die");
        this.gameObject.SetActive(false);
    }
}
