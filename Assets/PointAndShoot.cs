using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    private Vector3 target;

    public GameObject crosshair;
    public GameObject player;
    //public GameObject bulletPrefab;
    public GameObject bulletStart;

    public float bulletSpeed = 60.0f;
    [SerializeField] private GameObject bulletSprite;
    [SerializeField] private CandyInteraction grabCandyCount;
    [SerializeField] private List<Sprite> candySprites;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


        //player.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (Input.GetMouseButtonDown(0) && grabCandyCount.getTotalCurrentCandy() > 0)  
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
            grabCandyCount.candyShot();
        }

        void fireBullet(Vector2 direction, float rotationZ)
        {
            int randomCandy = Random.Range(0, candySprites.Count);
            //GameObject candy = Instantiate(candyPrefabs[randomCandy], transform.position, Quaternion.identity);
            GameObject bullet = Instantiate(bulletSprite) as GameObject;
            bullet.GetComponent<SpriteRenderer>().sprite = candySprites[randomCandy];
            bullet.transform.position = bulletStart.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
}
