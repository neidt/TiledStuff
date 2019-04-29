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

    //private Slider manaSlider;
    

    

        

    void Start()
    {
        phealth = this.gameObject.GetComponent<PlayerHealth>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        healthSlider.value = phealth.playerMaxHealth;
        
    }

    

    void Update()
    {
        healthSlider.value = phealth.playerCurrentHealth;
    }
}
