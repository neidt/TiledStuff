using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//@Author Natalie Eidt
public class ExitSpot : MonoBehaviour
{
    /// <summary>
    /// ref to this objects collider
    /// </summary>
    private BoxCollider2D boxCollider;
    
    /// <summary>
    /// initializes this object
    /// </summary>
    public void Initialize()
    {
        boxCollider = this.gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        this.gameObject.layer = LayerMask.NameToLayer("Exit");
        this.gameObject.tag = "Exit";
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something hitting me");
        if (other.tag == "Player")
        {
            Debug.Log("Player hitting me, switching scenes");
            Destroy(other.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
