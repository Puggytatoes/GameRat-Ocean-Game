using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField]
    public float movement = 5f;
    //Vector2 movement;
    [SerializeField]
    public float jumpForce = 20f;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public bool grounded = false;
    public LayerMask isground;
    public int atck = 0;
    public int atck2 = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

  
    // Update is called once per frame
    IEnumerator attackAni()
    {
      
        Debug.Log("target reached");
        atck = 1;
        yield return new WaitForSeconds (.733f);
        atck = 0;
        Debug.Log("done");
    }
    IEnumerator attackAni2()
    {
        atck2 = 1;
        yield return new WaitForSeconds(.333f);
        atck2 = 0;
    }


    void Update()
    {



        animator.SetFloat("attack", atck);
        animator.SetFloat("attack2", atck2);
        animator.SetFloat("horizontal", rb.velocity.x);



        bool shoot = Input.GetKeyDown("i");
        bool atk2 = Input.GetKeyDown("o");
        if (atk2)
        {
            StartCoroutine(attackAni2());
        }
        if (shoot)
        {
            StartCoroutine(attackAni());

        }

        movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);

        

        if (Input.GetButtonDown("Jump") && grounded)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }

        


        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, isground);


        Quaternion temp;

        temp = transform.localRotation;


        if (rb.velocity.x < 0)
        {
            
            temp.y = 180;

        }
        if (rb.velocity.x > 0)
        {
            
            temp.y = 0;

        }

        transform.localRotation = temp;
    }

    /*void FixedUpdate()
    {
    rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }*/
}
