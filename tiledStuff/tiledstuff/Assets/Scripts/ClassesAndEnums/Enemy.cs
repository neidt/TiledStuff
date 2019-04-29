using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private EnemyAttackAI enemyAttackAI;

    public void Initialize()
    {
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("Enemies");
        enemyHealth = this.gameObject.AddComponent<EnemyHealth>();
        enemyAttackAI = this.gameObject.AddComponent<EnemyAttackAI>();
    }

}
