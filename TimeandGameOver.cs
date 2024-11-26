using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerAndGameOver : MonoBehaviour
{
    public Text timerText;        // UI Text to display the timer
    public Text scoreText;        // UI Text for the score (optional, if needed)
    public GameObject gameOverPanel; // Game over UI panel

    private float timer = 0f;     // Timer starts at 0
    private bool isGameRunning = true;

    void Start()
    {
        // Ensure the game over panel is hidden initially
        gameOverPanel.SetActive(false);

        // Initialize the timer display
        UpdateTimerText();
    }

    void Update()
    {
        // Check if the player still exists and the game is running
        if (isGameRunning && GameObject.FindGameObjectWithTag("Player") != null)
        {
            timer += Time.deltaTime;  // Increment the timer
            UpdateTimerText();       // Update the timer UI
        }
        else if (isGameRunning) // Game over logic when player is destroyed
        {
            isGameRunning = false;
            gameOverPanel.SetActive(true);
        }
    }

    void UpdateTimerText()
    {
        // Format the timer to display minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
