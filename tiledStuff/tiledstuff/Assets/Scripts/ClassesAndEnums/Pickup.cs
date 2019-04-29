using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public void Initialize()
    {
        boxCollider = this.gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        this.gameObject.layer = LayerMask.NameToLayer("Pickups");
    }
}
