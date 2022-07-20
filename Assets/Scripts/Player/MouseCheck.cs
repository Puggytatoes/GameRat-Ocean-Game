using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCheck : MonoBehaviour
{
    public GameObject hoveredGO;
    public enum HoverState { HOVER, NONE };
    public HoverState hover_state = HoverState.NONE;
    
    /*
    void OnMouseOver()
    {
        Rigidbody2D rb = gameObject.GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;
        Debug.Log("STOP");
        rb.velocity = Vector2.zero;
    }
    */

    void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hover_state == HoverState.NONE)
            {
                hitInfo.collider.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
                hoveredGO = hitInfo.collider.gameObject;
                Debug.Log("TEST1");
            }
            hover_state = HoverState.HOVER;
            Debug.Log("TEST2");
        }
    }
}
