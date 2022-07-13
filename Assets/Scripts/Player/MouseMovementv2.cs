using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementv2 : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private float moveSpeed = 0.1f;
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Might have to change Camera.main when using Cinemachine
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}
