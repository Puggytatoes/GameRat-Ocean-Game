using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyTracker : MonoBehaviour
{
    private CandyInteraction candyCount;
    [SerializeField] private TMPro.TextMeshProUGUI candyCountText;

    // Start is called before the first frame update
    void Start()
    {
        candyCount = GameObject.FindGameObjectWithTag("Player").GetComponent<CandyInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        candyCountText.text = candyCount.getTotalCurrentCandy().ToString();
    }
}
