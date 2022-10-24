using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCandyCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Player.GetComponent<CandyInteraction>().getTotalCurrentCandy().ToString();
    }
}
