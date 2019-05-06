using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForGameOver : MonoBehaviour
{
    /// <summary>
    /// ref to map loader
    /// </summary>
    private MapLoader2 mapLoader;

    /// <summary>
    /// list of all the enemies in scene
    /// </summary>
    private GameObject[] enemies;

    /// <summary>
    /// the canvas to show when winning
    /// </summary>
    private Canvas winCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mapLoader = GameObject.Find("MapLoader2.0").GetComponent<MapLoader2>();
        winCanvas = GameObject.Find("WinCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        this.gameObject.SetActive(false);
        winCanvas.enabled = true;
    }
}
