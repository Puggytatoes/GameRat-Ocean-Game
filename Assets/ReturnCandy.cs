using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnCandy : MonoBehaviour
{
    //[SerializeField] private GameObject interactButton;
    [SerializeField] private KeyCode interactKey = KeyCode.F;
    [SerializeField] private CandyInteraction grabCandyAmount;
    [SerializeField] private List<int> candyTiers;
    [SerializeField] private List<int> candyBonuses;
    [SerializeField] private List<float> timeBonuses;
    [SerializeField] private TimerCountdown timer;
    public static int totalCandyCollected;
    private bool atHome;
    private Image interactPrompt;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        interactPrompt = Player.GetComponentInChildren<Image>();
        interactPrompt.enabled = false;
        atHome = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (atHome && Input.GetKeyDown(interactKey))
        {
            calculateBonuses();
            totalCandyCollected += grabCandyAmount.getTotalCurrentCandy();
            Debug.Log("total candy deposited:" + totalCandyCollected);
            grabCandyAmount.clearCandy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enter");
            interactPrompt.enabled = true;
            //interactButton.SetActive(true);
            atHome = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Exit");
            interactPrompt.enabled = false;
            //interactButton.SetActive(false);
            atHome = false;
        }
    }

    public int totalCandyAmount()
    {
        return totalCandyCollected;
    }

    private void calculateBonuses()
    {
        int tier = -1;
        for (int i = 0; i < candyTiers.Count; i++)
        {
            if (grabCandyAmount.getTotalCurrentCandy() >= candyTiers[i])
            {
                tier++;
            }
        }
        if (tier >= 0)
        {
            totalCandyCollected += candyBonuses[tier];
            timer.AddTime(timeBonuses[tier]);
        }
    }
}
