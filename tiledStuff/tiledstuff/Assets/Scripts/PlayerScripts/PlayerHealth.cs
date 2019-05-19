using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("the player's max health")]
    public float playerMaxHealth = 100;

    [Tooltip("the player's max stamina/mana")]
    public float playerMaxMana = 100;

    [Tooltip("the player's current health")]
    public float playerCurrentHealth;

    [Tooltip("the player's current mana")]
    public float playerCurrentMana;

    [Tooltip("amount to heal")]
    public float healPickupAmount = 5f;

    [Tooltip("Player animator component")]
    public Animator playerAnimator;

    private GameStateController gscontrol;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        playerCurrentMana = playerMaxMana;
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
    /// this functions depletes the player's mana
    /// </summary>
    /// <param name="amount"></param>
    public void UseMana(float amount)
    {
        if(playerCurrentMana <= 0)
        {
            playerCurrentMana = 0;
        }
        else
        {
            playerCurrentMana -= amount;
        }
    }

    /// <summary>
    /// this function heals the player's health and mana
    /// usually called by restore pickups
    /// </summary>
    /// <param name="amount"> the amount of health to give the player </param>
    public void HealPlayer(float amount)
    {
        if (playerCurrentHealth < playerMaxHealth || playerCurrentMana < playerMaxMana)
        {
            playerCurrentHealth += amount;
            playerCurrentMana += amount;
        }
        else if(playerCurrentHealth >= playerMaxHealth || playerCurrentMana >= playerMaxMana)
        {
            playerCurrentHealth = playerMaxHealth;
            playerCurrentMana = playerMaxMana;
        }
    }

    private void Update()
    {
        if (playerCurrentMana < playerMaxMana)
        {
            playerCurrentMana += .07f;
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
