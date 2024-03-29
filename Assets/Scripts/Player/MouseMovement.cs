using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 mouseDirection;
    private bool facingRight;
    private bool isHoldingMouse;
    private float rotation_z;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float speedReductionMultiplier;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D coll;
    private SpriteRenderer sr;
    private Vector2 screenBounds;

    [SerializeField] private Transform center;
    [SerializeField] private GameObject bulletStart;
    [SerializeField] private float knockbackVel = 8f;
    [SerializeField] private bool knockbacked;
    [SerializeField] private float knockbackWaitTime = 0.5f;
    [SerializeField] private string enemyTag;
    [SerializeField] private CandyInteraction candyInteration;
    [SerializeField] private List<GameObject> candyPrefabs;
    [SerializeField] private float dropTime;
    [SerializeField] private Vector2 dropForce;

    private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private int dashChargesStart;
    private int currentDashCharges;
    private bool currentlyRechargingDash;
    private float dashingTime = .2f;
    [SerializeField] private float dashingCooldown = .5f;
    [SerializeField] private float dashRechargeTime;
    private float horizontalMovement;
    private float verticalalMovement;
    private Vector3 moveDirection;
    private float currentMoveSpeed;
    private bool isStunned;

    [SerializeField] private List<GameObject> dashIcons;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        currentDashCharges = dashChargesStart;
        currentlyRechargingDash = false;
        currentMoveSpeed = moveSpeed;
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        ClampMovement();
        updateCharges();

        if (isDashing)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentDashCharges > 0)
        {
            StartCoroutine(Dash());
            currentDashCharges--;
        }

        Animate();
        
        if (mousePosition.x < transform.position.x && !facingRight)
            Flip();
        else if (mousePosition.x > transform.position.x && facingRight)
            Flip();

        if (coll.OverlapPoint(mousePosition))
        {
            isHoldingMouse = false;
            //rb.velocity = Vector3.zero;
        }


    }
    void FixedUpdate()
    {
        PlayerInput();

        if (!knockbacked)
        {

            if (isDashing)
            {
                return;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            Knockback(collision.transform);
        }
    }

    private void PlayerInput()
    {
        if (!isStunned)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalalMovement = Input.GetAxisRaw("Vertical");

            moveDirection = this.transform.up * verticalalMovement + this.transform.right * horizontalMovement;

            rb.AddForce(moveDirection * currentMoveSpeed, ForceMode2D.Force);
        }
    }

    private void ClampMovement()
    {
        Vector3 viewPos = transform.position;
        float playerWidth = sr.bounds.size.x / 2;
        float playerHeight = sr.bounds.size.y / 2;

        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + playerWidth, screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + playerHeight, screenBounds.y - playerHeight);
        transform.position = viewPos;
    }

    private void Flip()
    {
        rb.AddForce(-rb.velocity * rb.mass * 2 * Time.deltaTime);
        facingRight = !facingRight;
        bulletStart.transform.localPosition = new Vector3(-bulletStart.transform.localPosition.x, 0, 0);
        sr.flipX = facingRight;
    }

    void Animate()
    {
        if (isStunned)
            anim.SetBool("IsStunned", true);
        else
            anim.SetBool("IsStunned", false);
    }

    private IEnumerator Dash()
    {
        removeCharge();
        StartCoroutine(DashRecharge());
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashingPower, moveDirection.y * dashingPower);
        coll.enabled = false;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        coll.enabled = true;
    }

    private IEnumerator DashRecharge()
    {
        //currentlyRechargingDash = true;
        yield return new WaitForSeconds(dashRechargeTime);
        if (currentDashCharges < dashChargesStart)
        {
            currentDashCharges++;
        }
        //currentlyRechargingDash = false;
    }

    public void Knockback(Transform t)
    {
        //List<GameObject> candies = new List<GameObject>();
        if (candyInteration.getTotalCurrentCandy() > 0)
        {
            for (int i = 0; i < candyInteration.getTotalCurrentCandy(); i++)
            {
                int randomCandy = Random.Range(0, candyPrefabs.Count);
                GameObject candy = Instantiate(candyPrefabs[randomCandy], transform.position, Quaternion.identity);
                StartCoroutine(DropCandy(candy, dropForce));
            }
        }
        var dir = center.position - t.position;
        knockbacked = true;
        isStunned = true;
        rb.velocity = dir.normalized * knockbackVel;
        StartCoroutine(Unknockback());
    }

    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(knockbackWaitTime);
        knockbacked = false;
        isStunned = false;
        candyInteration.clearCandy();
    }

    public void setMoveSpeed(int candyCount)
    {
        float speedReduction = candyCount * speedReductionMultiplier;
        currentMoveSpeed = moveSpeed - speedReduction;
    }

    public bool returnStunned()
    {
        return isStunned;
    }

    private IEnumerator DropCandy(GameObject prefab, Vector2 force)
    {
        prefab.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
        prefab.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(dropTime);
        prefab.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        prefab.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void updateCharges()
    {
        for (int i = 0; i < currentDashCharges; i++)
        {
            dashIcons[i].GetComponent<Image>().enabled = true;
        }
    }

    private void removeCharge()
    {
        dashIcons[currentDashCharges - 1].GetComponent<Image>().enabled = false;
    }
}
