using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDmg = 10;
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Attack();
            
        }
    }

    void Attack()
    {
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            GameObject other = enemy.gameObject;
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDmg);

            EnemyKnockback enemyKnockback = other.GetComponent<EnemyKnockback>();
            enemyKnockback.Knockback(transform);
        }
    }

    void OnDrawGizmosSelected()
    {
         if(attackPoint ==null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
