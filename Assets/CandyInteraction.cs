using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyInteraction : MonoBehaviour
{
    [SerializeField] private int maxCandyCount;
    [SerializeField] private MouseMovement mouseMovement;
    private int candyCounter;
    private bool canCollect;
    private bool isStunned;

    // Start is called before the first frame update
    void Start()
    {
        candyCounter = 0;
        canCollect = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (candyCounter < maxCandyCount && !mouseMovement.returnStunned())
        {
            canCollect = true;
        }
        else
        {
            canCollect = false;
        }

        mouseMovement.setMoveSpeed(candyCounter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollect)
        {
            if (collision.gameObject.tag == "Candy")
            {
                candyCounter++;
                Debug.Log("current candy:" + candyCounter);
                Destroy(collision.gameObject);
            }
        }
    }

    public void clearCandy()
    {
        candyCounter = 0;
    }

    public int getTotalCurrentCandy()
    {
        return candyCounter;
    }

    public void candyShot()
    {
        candyCounter--;
    }
}
