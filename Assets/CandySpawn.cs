using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour
{
    private CircleCollider2D collider;
    private SpriteRenderer sr;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Update()
    {
        Boundaries();
    }

    private void Boundaries()
    { 
        Vector3 viewPos = transform.position;
        float playerWidth = sr.bounds.size.x / 2;
        float playerHeight = sr.bounds.size.y / 2;

        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + playerWidth, screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + playerHeight, screenBounds.y - playerHeight);
        transform.position = viewPos;
    }
}
