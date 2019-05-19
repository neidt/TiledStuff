using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SturdyDoor : MonoBehaviour
{
    /// <summary>
    /// the health of the door
    /// </summary>
    private float health;

    public void Initialize()
    {
        this.health = 10;
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("WallsAndDoors");
        this.gameObject.tag = "SturdyDoor";
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0)
        {
            GetDestroyed();
        }
    }

    public void GetDestroyed()
    {
        Destroy(this.gameObject);
    }
}
