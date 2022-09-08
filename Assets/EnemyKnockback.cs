using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float knockbackVel = 8f;
    [SerializeField] private bool knockbacked; //not useful rn
    [SerializeField] private float knockbackWaitTime = 0.5f;
    public Rigidbody2D rb;
    public void Knockback(Transform t)
    {
        var dir = center.position - t.position;
        knockbacked = true;
        rb.velocity = dir.normalized * knockbackVel;
        StartCoroutine(Unknockback());
    }

    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(knockbackWaitTime);
        knockbacked = false;
    }
}
