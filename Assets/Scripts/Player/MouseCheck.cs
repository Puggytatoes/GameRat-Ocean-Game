using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCheck : MonoBehaviour
{
    public GameObject checkObject;
    
    public void OnMouseOver()
    {
        Debug.Log("STOP");
        Rigidbody2D rb = gameObject.GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;
        Debug.Log(rb);
        rb.velocity = Vector2.zero;
    }
}
