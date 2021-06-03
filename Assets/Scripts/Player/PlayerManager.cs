using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public static bool gameOver;
    public static bool isStart;
    public GameObject startText;
    public Text coinText;
    public static int noOfCoins;
    void Start()
    {
        gameOver = false;
        isStart = false;
        Time.timeScale = 1;
        noOfCoins = 0;
    }

    
    void Update()
    {
        coinText.text = "Coins: " + noOfCoins;

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        if (isStart)
        {
            startText.SetActive(false);
        }
    }
}
