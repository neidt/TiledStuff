using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@Author Natalie Eidt
public class Fireball : MonoBehaviour
{
    [Tooltip("The speed of the fireball")]
    public float speed = 10f;

    [Tooltip("the fireball's rigidbody2D")]
    public Rigidbody2D rb;

    [Tooltip("the amount of damage this fireball does")]
    public float projDamage = 3f;

    /// <summary>
    /// this moves the fireball in the specified dir
    /// </summary>
    /// <param name="dir"> the direction to move in </param>
    public void MoveFireball(Vector2 dir)
    {
        rb.velocity = dir * speed;
        Destroy(this.gameObject, 3f);
    }

    /// <summary>
    /// checks for a collision on our trigger
    /// </summary>
    /// <param name="collider"> the collider hitting us </param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(projDamage);
            Destroy(this.gameObject);
        }
        else if(collider.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        else if(collider.tag == "Door")
        {
            collider.GetComponent<Door>().TakeDamage(projDamage-1f);
            Destroy(this.gameObject);
        }
        else if(collider.tag == "SturdyDoor")
        {
            collider.GetComponent<SturdyDoor>().TakeDamage(projDamage - 1f);
            Destroy(this.gameObject);
        }
    }
}
