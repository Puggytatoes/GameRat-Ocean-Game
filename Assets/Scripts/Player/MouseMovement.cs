using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 mouseDirection;
    private bool facingRight;
    private bool isHoldingMouse;
    [SerializeField] private float moveSpeed = 4f;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D coll;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(1))
        {
            mouseDirection = mousePosition - gameObject.transform.position;
            mouseDirection = new Vector2(mouseDirection.x, mouseDirection.y);
            mouseDirection = mouseDirection.normalized;
            isHoldingMouse = true;
        }
        else
            isHoldingMouse = false;

        if (mousePosition.x < transform.position.x && !facingRight)
            Flip();
        else if (mousePosition.x > transform.position.x && facingRight)
            Flip();

        Animate();

        if (coll.OverlapPoint(mousePosition))
        {
            isHoldingMouse = false;
            rb.velocity = Vector3.zero;
        }


    }
    void FixedUpdate()
    {
        if (isHoldingMouse)
            rb.AddForce((mouseDirection) * moveSpeed * Time.deltaTime);
        else
            rb.AddForce(-rb.velocity * rb.mass * Time.deltaTime);
    }

    private void Flip()
    {
        rb.AddForce(-rb.velocity * rb.mass * 2 * Time.deltaTime);
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Animate()
    {
        if (rb.velocity.magnitude > 0.5f)
            anim.SetBool("isSwimming", true);
        else
            anim.SetBool("isSwimming", false);
    }
}
