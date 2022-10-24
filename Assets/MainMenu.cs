using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("menu");
    }

    public void OpenSubMenu(GameObject panel)
    {
        panel.SetActive(true);
        currentPanel.SetActive(false);
        currentPanel = panel; 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().StopPlaying("menu");
        FindObjectOfType<AudioManager>().Play("game theme");
        //SceneManager.LoadScene("Start"); 
    }

}
