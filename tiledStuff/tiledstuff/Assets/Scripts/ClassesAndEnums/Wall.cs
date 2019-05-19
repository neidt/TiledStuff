using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public void Initialize()
    {
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("WallsAndDoors");
        this.gameObject.tag = "Wall";
    }
}
