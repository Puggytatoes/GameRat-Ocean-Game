using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{

    PlayerScanner playerScanner;

    EnemyMovement enemyMovement;

    PlayerOutOfRange playerOutOfRange;

    void Awake()
    {
        playerScanner = GetComponent<PlayerScanner>();

        enemyMovement = GetComponent<EnemyMovement>();

        playerOutOfRange = GetComponent<PlayerOutOfRange>();
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


    }
}
