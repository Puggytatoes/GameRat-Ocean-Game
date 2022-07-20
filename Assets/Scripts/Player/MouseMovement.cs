using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 mouseDirection;
    private bool isHoldingMouse;
    [SerializeField] private float moveSpeed = 4f;
    private Rigidbody2D rb;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            mouseDirection = mousePosition - gameObject.transform.position;
            mouseDirection = new Vector2(mouseDirection.x, mouseDirection.y);
            mouseDirection = mouseDirection.normalized;
            isHoldingMouse = true;
        }
        else
            isHoldingMouse = false;

    }
    void FixedUpdate()
    {
        Debug.Log(rb.velocity.magnitude);
        if (isHoldingMouse)
            rb.AddForce((mouseDirection) * moveSpeed * Time.deltaTime);
        else
            rb.AddForce(-rb.velocity * rb.mass * Time.deltaTime);

        if (rb.velocity.magnitude > 0.5)
            animator.SetBool("isSwimming", true);
        else
            animator.SetBool("isSwimming", false);
    }

}
