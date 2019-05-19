using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Author Natalie Eidt
public class Wall : MonoBehaviour
{
    /// <summary>
    /// initializes the object
    /// </summary>
    public void Initialize()
    {
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("WallsAndDoors");
        this.gameObject.tag = "Wall";
    }
}
