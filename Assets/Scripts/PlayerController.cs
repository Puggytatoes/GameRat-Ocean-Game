using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] Collider2D standingCollider, crouchingCollider;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform ceilingCheckCollider;
    [SerializeField] LayerMask groundLayer;

    public static float speed = 7;
    public static float jumpPower = 35;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] float ceilingCheckRadius = 0.4f;

    [SerializeField]  private float coyoteTime = 0.08f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.08f;
    private float jumpBufferCounter;

    float horizontalValue;
    [SerializeField] float crouchSpeedModifier = 0.35f;
    [SerializeField] public bool isGrounded;
    public static bool underCeiling;

    public static bool isCrouching;
    bool isJumping = false;
    public static bool facingRight = true;

    public Animator animator;

    float dirX;
    float moveSpeed = 5f;
    bool isMoving = false;
    public AudioSource rataudio;

    public static float currentX;
    public static float currentY;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rataudio = GetComponent<AudioSource>();
        speed = 7;
        jumpPower = 35;
    }
 
    void Update()
    {
        currentX = gameObject.transform.position.x;
        currentY = gameObject.transform.position.y;
        
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && isJumping == false)
        {
            jumpBufferCounter = jumpBufferTime;
            Jump();
        }
        else if (Input.GetButtonUp("Jump") && isJumping)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
            coyoteTimeCounter = 0f;
        }

        //If we press Crouch button enable crouch 
        if (Input.GetKey("s") && isGrounded)
        {
            isCrouching = true;
            
        }
        //Otherwise disable it
        else if (Input.GetKeyUp("s"))
        {
            isCrouching = false;
        }

        //Set the yVelocity Value
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
    }

    void Jump()
    {
        animator.SetBool("isJumping", true);
        if (coyoteTimeCounter > 0f && underCeiling == false)
        {
            rb.velocity = Vector2.up * jumpPower;
            isJumping = true;
            isCrouching = false;
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
        if (isGrounded == false)
            isCrouching = false;
        if (isGrounded)
            animator.SetBool("isJumping", false);
        Move(horizontalValue, isCrouching);

        horizontalValue = Input.GetAxisRaw("Horizontal");
        dirX = Input.GetAxis("Horizontal") * moveSpeed;

        if (rb.velocity.x == 14)
            isMoving = true;
        else if (rb.velocity.x == -14)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving && isGrounded)
        {
            if (!rataudio.isPlaying)
            {
                rataudio.Play();
            }
        }
        else
        {
            rataudio.Stop();
        }
        
    }



    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0 && underCeiling == false)
        {
            isGrounded = true;
            
        }
        animator.SetBool("isJumping", !isGrounded);

    }

    void Move(float dir, bool crouchFlag)
    {
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(ceilingCheckCollider.position, ceilingCheckRadius, groundLayer))
            {
                underCeiling = true;
                crouchFlag = true;
            }
            else
                underCeiling = false;
                
        }
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;


        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        if (crouchFlag)
        {
            xVal *= crouchSpeedModifier;
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }

        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }

        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            facingRight = true;
        }

        // Set float xVelocity value according to the x value of the rb2d velocity
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    public void OnDrawGizmosSelected() // This is here just for debugging purposes
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ceilingCheckCollider.position, ceilingCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckCollider.position, groundCheckRadius);
    }

    public static bool GetCeilingCheck()
    {
        return underCeiling;
    }
}