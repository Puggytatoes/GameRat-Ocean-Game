using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfRange : MonoBehaviour
{

    public bool InRange;

    [SerializeField]
    private string detectionTag = "Player";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            InRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            InRange = false;

        }
    }







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
