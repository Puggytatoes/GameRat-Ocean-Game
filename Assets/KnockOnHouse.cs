using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnockOnHouse : MonoBehaviour
{
    [SerializeField] private float trickChance;
    [SerializeField] private float treatChance;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject Player;
    [SerializeField] private KeyCode interactKey = KeyCode.F;
    [SerializeField] private List<GameObject> candyPrefabs;
    [SerializeField] private int minCandy;
    [SerializeField] private int maxCandy;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int minEnemy;
    [SerializeField] private int maxEnemy;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Vector2 spawnForce;
    [SerializeField] private float spawnTime;
    //[SerializeField] private float spawnMultiplier;
    private BoxCollider2D knockZone;
    private float totalChance;
    private Vector2 center;
    private bool atHouse;
    private Image interactPrompt;

    // Start is called before the first frame update
    void Start()
    {
        knockZone = GetComponent<BoxCollider2D>();
        //interactButton.SetActive(false);
        interactPrompt = Player.GetComponentInChildren<Image>();
        interactPrompt.enabled = false;
        totalChance = trickChance + treatChance;
        center = transform.position;
        atHouse = false;
    }

    private void Awake()
    {
        //interactButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (atHouse && Input.GetKeyDown(interactKey))
        {
            float randomChance = Random.Range(0, totalChance);
            if (randomChance <= trickChance)
            {
                Debug.Log("TRICK");
                SpawnEnemies();
            }
            else
            {
                Debug.Log("TREAT");
                SpawnCandy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Enter");
            interactPrompt.enabled = true;
            //interactButton.SetActive(true);
            atHouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Exit");
            interactPrompt.enabled = false;
            //interactButton.SetActive(false);
            atHouse = false;
        }
    }

    private void SpawnEnemies()
    {
        int randomEnemyCount = Random.Range(minEnemy, maxEnemy);

        for (int i = 0; i < randomEnemyCount; i++)
        {
            float ang = Random.value * 360;
            Vector2 pos;
            pos.x = center.x + spawnRadius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + spawnRadius * Mathf.Cos(ang * Mathf.Deg2Rad);
            GameObject enemy = Instantiate(enemyPrefab, new Vector2(0,0), Quaternion.identity);

            StartCoroutine(SpawningPrefab(enemy, pos));
        }
    }

    private void SpawnCandy()
    {
        int randomCandyCount = Random.Range(minCandy, maxCandy);

        for (int i = 0; i < randomCandyCount; i++)
        {
            float ang = Random.value * 360;
            Vector2 pos;
            pos.x = center.x + spawnRadius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + spawnRadius * Mathf.Cos(ang * Mathf.Deg2Rad);
            int randomCandyPrefab = Random.Range(0, candyPrefabs.Count);
            GameObject candy = Instantiate(candyPrefabs[randomCandyPrefab], transform.position, Quaternion.identity);



            StartCoroutine(SpawningPrefab(candy, pos * spawnForce));
        }
    }

    private IEnumerator SpawningPrefab(GameObject prefab, Vector2 force)
    {
        prefab.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
        prefab.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(spawnTime);
        prefab.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        prefab.GetComponent<Collider2D>().enabled = true;
        interactButton.SetActive(false);
        Destroy(gameObject);
    }
}
