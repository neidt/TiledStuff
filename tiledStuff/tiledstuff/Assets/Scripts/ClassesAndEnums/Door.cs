using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Author Natalie Eidt
public class Door : MonoBehaviour
{
    [Tooltip("how much health this door has")]
    public float health;

    /// <summary>
    /// initializes object with health and properties
    /// </summary>
    public void Initialize()
    {
        this.health = 5;
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("WallsAndDoors");
        this.gameObject.tag = "Door";
    }

    /// <summary>
    /// takes damage, calls getDestroyed if no health is left
    /// </summary>
    /// <param name="amount"> the amount of damage to take </param>
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            GetDestroyed();
        }
    }

    /// <summary>
    /// destoys object
    /// </summary>
    public void GetDestroyed()
    {
        Destroy(this.gameObject);
    }
}
