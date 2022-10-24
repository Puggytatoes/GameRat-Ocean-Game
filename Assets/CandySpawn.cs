using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour
{
    private CircleCollider2D collider;

    // Start is called before the first frame update
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
    }
}
