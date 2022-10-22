using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel;

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
        //SceneManager.LoadScene("Start"); 
    }
}
