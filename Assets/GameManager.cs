using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager Instance { set; get; }
    public TMP_Text timerText;
    public TMP_Text messageText;
    public GameObject endGamePanel;
    public GameObject endGameButton;
    private bool isGameOver;
    public static int enemies;
    private float timer = 150;
    void Awake()
    {
        endGamePanel.SetActive(false);
        if (Instance && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        isGameOver = false;
    }
    
    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time left : " + Mathf.Ceil(timer).ToString("F0");
            if (enemies <= 0)
            {
                StopGame(1);
            }
            if (timer <= 0)
            {
                StopGame(0);
            }
        }

    }
    public static void StopGame(int reason)
    {
        if (Instance != null && !Instance.isGameOver)
        {
            Instance.isGameOver = true;

            string message = reason switch
            {
                0 => "Time's up! You lost... (check the timer next time)",
                1 => "WOOOOOOW! You won!",
                2 => "OHHH NOOOO you died!!!!!",
            };

            if (Instance.messageText != null)
            {
                Instance.messageText.text = message;
            }

            if (Instance.endGameButton != null)
            {
                Instance.endGameButton.SetActive(true);
            }
            if (Instance.endGamePanel != null)
            {
                Instance.endGamePanel.SetActive(true);
            }
        }
    }
}
