using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 mouseDirection;
    private bool facingRight;
    private bool isHoldingMouse;
    private float rotation_z;
    [SerializeField] private float moveSpeed = 4f;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D coll;
    private SpriteRenderer sr;

    private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    private float dashingTime = .2f;
    private float dashingCooldown = .5f;
    private float horizontal;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
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

        if (isDashing)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }

        Animate();

        if (mousePosition.x < transform.position.x && !facingRight)
            Flip();
        else if (mousePosition.x > transform.position.x && facingRight)
            Flip();

        //rotates player to follow cursor
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        if (coll.OverlapPoint(mousePosition))
        {
            isHoldingMouse = false;
            rb.velocity = Vector3.zero;
        }


    }
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (isHoldingMouse)
            rb.AddForce((mouseDirection) * moveSpeed * Time.deltaTime);
        else
            rb.AddForce(-rb.velocity * rb.mass * Time.deltaTime);
    }

    private void Flip()
    {
        rb.AddForce(-rb.velocity * rb.mass * 2 * Time.deltaTime);
        facingRight = !facingRight;
        sr.flipY = facingRight;
    }

    void Animate()
    {
        if (rb.velocity.magnitude > 0.5f)
            anim.SetBool("isSwimming", true);
        else
            anim.SetBool("isSwimming", false);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(mouseDirection.x * dashingPower, mouseDirection.y * dashingPower);
        coll.enabled = false;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        coll.enabled = true;
    }
}
