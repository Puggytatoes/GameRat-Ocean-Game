using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{

    PlayerScanner playerScanner;

    EnemyMovement enemyMovement;

    PlayerOutOfRange playerOutOfRange;

    EnemyAI enemyAI;

    public GameObject Zone;

    void Awake()
    {
        playerScanner = GetComponent<PlayerScanner>();

        enemyMovement = GetComponent<EnemyMovement>();

        enemyAI = GetComponent<EnemyAI>();

        playerOutOfRange = Zone.GetComponent<PlayerOutOfRange>();

    }
    // Update is called once per frame
    void Update()
    {
        if (playerScanner.PlayerDetected == true && playerOutOfRange.InRange == true)
        {
            enemyMovement.enabled = false;
        }
        else
        {
            enemyMovement.enabled = true;
        }


        if (playerScanner.PlayerDetected == true && playerOutOfRange.InRange == true)
        {
            enemyAI.enabled = true;
        }
        else
        {
            enemyAI.enabled = false;
        }

    }
}
