using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Initialize()
    {
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("WallsAndDoors");
    }
}
