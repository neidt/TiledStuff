using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Author Natalie Eidt
public class SturdyDoor : MonoBehaviour
{
    /// <summary>
    /// the health of the door
    /// </summary>
    private float health;

    /// <summary>
    /// initializes the object
    /// </summary>
    public void Initialize()
    {
        this.health = 10;
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("WallsAndDoors");
        this.gameObject.tag = "SturdyDoor";
    }

    /// <summary>
    /// takes damage, calls getDestroyed if no health is left
    /// </summary>
    /// <param name="amount"> the amount of damage to take </param>
    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0)
        {
            GetDestroyed();
        }
    }
    /// <summary>
    /// destroys object
    /// </summary>
    public void GetDestroyed()
    {
        Destroy(this.gameObject);
    }
}
