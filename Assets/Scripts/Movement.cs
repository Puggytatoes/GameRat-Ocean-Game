using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = .2f;
    [SerializeField]
    private float dashingCooldown = .5f;
    private bool isFacingRight = true;
    private float horizontal;
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float jumpingPower = 16f;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpingPower);
        }
        if (Input.GetButtonUp("Jump") && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
