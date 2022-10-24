using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> houseSprites;
    [SerializeField] private float distanceFromPlayerHome;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int maxHouses;
    private Vector2 screenBounds;
    private bool canSpawnHouse;
    private GameObject[] houses;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //houses = GameObject.FindGameObjectsWithTag("EnemyHouse");
        canSpawnHouse = true;
        ReturnCandy.totalCandyCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnHouse)
        {
            StartCoroutine(spawnHouse());
        }
    }

    private GameObject randomHouse()
    {
        int house = Random.Range(0, houseSprites.Count);
        return houseSprites[house];
    }
    private Vector3 housePos()
    {
        SpriteRenderer house = houseSprites[0].GetComponent<SpriteRenderer>();

        Vector3 viewPos = transform.position;
        float houseWidth = house.bounds.size.x / 2;
        float houseHeight = house.bounds.size.y / 2;

        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + houseWidth, screenBounds.x - houseWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + houseHeight, screenBounds.y - houseHeight);

        int gridSelection = Random.Range(1, 5);
        if (gridSelection == 1)
        {
            return new Vector3(Random.Range(-screenBounds.x + houseWidth, -distanceFromPlayerHome), Random.Range(-screenBounds.y + houseHeight, screenBounds.y - houseHeight), 0);
        }
        else if (gridSelection == 2)
        {
            return new Vector3(Random.Range(-distanceFromPlayerHome, distanceFromPlayerHome), Random.Range(-screenBounds.y + houseHeight, -distanceFromPlayerHome), 0);
        }
        else if (gridSelection == 3)
        {
            return new Vector3(Random.Range(-distanceFromPlayerHome, distanceFromPlayerHome), Random.Range(screenBounds.y - houseHeight, + distanceFromPlayerHome), 0);
        }
        else
        {
            return new Vector3(Random.Range(distanceFromPlayerHome, screenBounds.x - houseWidth), Random.Range(-screenBounds.y + houseHeight, screenBounds.y - houseHeight), 0);
        }
    }

    private IEnumerator spawnHouse()
    {
        canSpawnHouse = false;
        yield return new WaitForSeconds(timeBetweenSpawns);
        Instantiate(randomHouse(), housePos(), Quaternion.identity);
        canSpawnHouse = true;
    }
}
