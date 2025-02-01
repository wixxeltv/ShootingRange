using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager Instance { set; get; }
    public TMP_Text timerText;
    public TMP_Text messageText;
    public TMP_Text targetsText;
    public GameObject endGamePanel;
    public GameObject endGameButton;
    private bool isGameOver;
    public static int targets;
    private float timer = 120;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            targetsText.text = "Targets :  " + targets;
            timerText.text = "Time left : " + Mathf.Ceil(timer).ToString("F0");
            if (targets <= 0)
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (Instance != null && !Instance.isGameOver)
        {
            Instance.isGameOver = true;

            string message = reason switch
            {
                0 => "Wowow there is no time left, the game is over.",
                1 => "No targets left.. You are a real pro shooter.",
                
            };

            if (Instance.messageText != null)
            {
                Instance.messageText.text = message;
            }
            if (Instance.endGamePanel != null)
            {
                Instance.endGamePanel.SetActive(true);
            }
        }
    }
    
}
