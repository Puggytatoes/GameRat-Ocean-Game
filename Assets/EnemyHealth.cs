using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private float deathWaitTime;

    public Rigidbody2D enemyBody;
    public BoxCollider2D coll;

    private void Start()
    {
        currentHealth = maxHealth;
        coll = GetComponent<BoxCollider2D>();
        enemyBody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        GetComponent<EnemyMovement>().enabled = false;
        coll.enabled = false;
        enemyBody.bodyType = RigidbodyType2D.Static;
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(deathWaitTime);
        Destroy(gameObject);
    }
}
