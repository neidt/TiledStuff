using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [Tooltip("how much health this door has")]
    public float health;

    public void Initialize()
    {
        this.health = 5;
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("WallsAndDoors");
        this.gameObject.tag = "Door";
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            GetDestroyed();
        }
    }

    public void GetDestroyed()
    {
        Destroy(this.gameObject);
    }
}
