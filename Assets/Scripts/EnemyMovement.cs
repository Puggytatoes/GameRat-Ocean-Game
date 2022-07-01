using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    public int startingPoint;

    public Transform[] points;

    private int i;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    

    void Start()
    {
        

        transform.position = points[startingPoint].position;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

       
    }

}