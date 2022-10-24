using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHurt : MonoBehaviour
{
    [SerializeField] private float knockbackVel;
    [SerializeField] private float knockbackWaitTime;
    [SerializeField] private AIDestinationSetter targetFinder;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isStunned;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet") 
        {
            Debug.Log("Hit");
            Knockback(collision.transform, collision);
        }
    }

    public void Knockback(Transform t, Collision2D collision)
    {
        var dir = transform.position - t.position;
        isStunned = true;
        rb.velocity = dir.normalized * knockbackVel;
        targetFinder.enabled = false;
        StartCoroutine(Unknockback(collision.gameObject));
    }

    private IEnumerator Unknockback(GameObject gameObject)
    {
        yield return new WaitForSeconds(knockbackWaitTime);
        isStunned = false;
        targetFinder.enabled = true;
        Destroy(gameObject);
    }

    void Animate()
    {
        if (isStunned)
            anim.SetBool("isStunned", true);
        else
            anim.SetBool("isStunned", false);
    }
}
