using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOnHouse : MonoBehaviour
{
    [SerializeField] private float trickChance;
    [SerializeField] private float treatChance;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private KeyCode interactKey = KeyCode.F;
    [SerializeField] private List<GameObject> candyPrefabs;
    [SerializeField] private GameObject enemyPrefab;
    private BoxCollider2D knockZone;
    private float totalChance;

    // Start is called before the first frame update
    void Start()
    {
        knockZone = GetComponent<BoxCollider2D>();
        interactButton.SetActive(false);
        totalChance = trickChance + treatChance;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactButton.activeInHierarchy && Input.GetKeyDown(interactKey))
        {
            float randomChance = Random.Range(0, totalChance);
            if (randomChance <= trickChance)
            {
                Debug.Log("TRICK");
            }
            else
            {
                Debug.Log("TREAT");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactButton.SetActive(false);
        }
    }

    private void SpawnEnemies()
    {

    }

    private void SpawnCandy()
    {

    }
}
