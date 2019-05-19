using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    /// <summary>
    /// ref to this objects collider
    /// </summary>
    private BoxCollider2D boxCollider;

    [Tooltip("The amount to heal when this item is picked up")]
    public float healAmount = 5f;

    /// <summary>
    /// initializes this object
    /// </summary>
    public void Initialize()
    {
        boxCollider = this.gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        this.gameObject.layer = LayerMask.NameToLayer("Pickups");
        this.gameObject.tag = "Pickup";
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().HealPlayer(healAmount);
            Destroy(this.gameObject);
        }
    }
}
