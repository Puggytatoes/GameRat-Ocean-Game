using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    public float moveSpeed = 7f;

    Rigidbody2D rb;

    playermovement target;
    Vector2 moveDirection;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<playermovement>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y + 7);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
