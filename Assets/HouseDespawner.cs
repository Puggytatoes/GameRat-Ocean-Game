using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDespawner : MonoBehaviour
{
    [SerializeField] private float lifeDuration;

    private void Awake()
    {
        StartCoroutine(lifeTimer());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator lifeTimer()
    {
        yield return new WaitForSeconds(lifeDuration);
        Destroy(gameObject);
    }
}
