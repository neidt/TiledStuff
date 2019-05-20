using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    /// <summary>
    /// reference to playerhealth script
    /// </summary>
    private PlayerHealth phealth;

    /// <summary>
    /// reference to the ui health slider
    /// </summary>
    private Slider healthSlider;

    /// <summary>
    /// reference to the stamina/mana slider
    /// </summary>
    private Slider manaSlider;       


    /// <summary>
    /// assigns all the needed references
    /// </summary>
    void Start()
    {
        phealth = this.gameObject.GetComponent<PlayerHealth>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        healthSlider.value = phealth.playerMaxHealth;
        manaSlider = GameObject.Find("StaminaSlider").GetComponent<Slider>();
        manaSlider.value = phealth.playerMaxMana;
    }
    
    void Update()
    {
        healthSlider.value = phealth.playerCurrentHealth;
        manaSlider.value = phealth.playerCurrentMana;

        if(phealth.playerCurrentMana < phealth.playerMaxMana)
        {
            phealth.playerCurrentMana += .01f;
        }
    }
}
