using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int health;
    public int numOfHearts;

    public static int sHearts = 4;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

   

    void Update()
    {
        health = sHearts;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public static void RemoveHeart()
    {
        sHearts -= 1;
    }

    public static void AddHeart()
    {
        sHearts += 1;
    }
    public static int GetHearts()
    {
        return sHearts;
    }
}