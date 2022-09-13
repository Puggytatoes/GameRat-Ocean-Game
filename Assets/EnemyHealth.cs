using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private float deathWaitTime;
    [SerializeField]
    private bool isDead;

    public Rigidbody2D enemyBody;
    public BoxCollider2D coll;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    private void Start()
    {
        currentHealth = maxHealth;
        coll = GetComponent<BoxCollider2D>();
        enemyBody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, GameObject sender)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        if (currentHealth <= 0)
        {
            Dead();
            isDead = true;
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
