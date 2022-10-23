using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;

public class MainMenuUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    [SerializeField] private GameObject candyImage;
    [SerializeField] private Sprite[] candySprites; 
    void Update()
    {
        if (mouse_over)
        {
            Debug.Log("Mouse Over");
        }
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        int num = Random.Range(0, candySprites.Length);
        candyImage.GetComponent<Image>().sprite = candySprites[num]; 
        candyImage.SetActive(true);
        Debug.Log("Mouse enter");
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        candyImage.SetActive(false);
        Debug.Log("Mouse exit");
    }
}
