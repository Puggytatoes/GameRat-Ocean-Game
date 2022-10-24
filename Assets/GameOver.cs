using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private int threshold = 40; 
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private Sprite imageGood;
    [SerializeField] private Sprite imageBetter;
    [SerializeField] private GameObject background;

    private void Start()
    {
        int total = ReturnCandy.totalCandyCollected;
        number.text = total.ToString();
        if (total <= threshold)
        {
            background.GetComponent<Image>().sprite = imageGood; 
        }
        else
        {
            background.GetComponent<Image>().sprite = imageBetter; 
        }
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
