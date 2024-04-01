using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    private static Game1Manager instance = null;

    public bool gameOver = false;
    public int GreekCounter = 0;

    private List<GuardModulController> guards = new List<GuardModulController>();
    private PlayerGreekController playController; // Reference to the PlayController

    private TextMeshProUGUI greekCounterText;

    public GameObject gameOverImage; // Reference to the GameOver image GameObject
    public GameObject winScreenImage; // Reference to the WinScreen image GameObject

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Get the TextMeshProUGUI component
        greekCounterText = FindObjectOfType<TextMeshProUGUI>();

        gameOverImage = GameObject.Find("GameOver");
        winScreenImage = GameObject.Find("WinScreen");

        // Find the PlayController in the scene and assign the reference
        playController = FindObjectOfType<PlayerGreekController>();

        // Disable the GameOver and WinScreen images initially
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(false);
        }
        if (winScreenImage != null)
        {
            winScreenImage.SetActive(false);
        }
    }

    public static Game1Manager GetInstance()
    {
        return instance;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void SetGameOver(bool value)
    {
        gameOver = value;
    }

    // Method to register a guard
    public void RegisterGuard(GuardModulController guard)
    {
        guards.Add(guard);
    }

    // Method to check if any guard was seen
    void FixedUpdate()
    {
        // Access the Greek counter from the PlayController
        if (playController != null)
        {
            if (GreekCounter < playController.GetGreekCounter())
            {
                foreach (GuardModulController guard in guards)
                {
                    guard.ResetToWalk(); // Call a method in GuardModulController to reset to walk state
                }
            }
            GreekCounter = playController.GetGreekCounter();
        }

        // Update the TextMeshProUGUI text
        if (greekCounterText != null)
        {
            greekCounterText.text = GreekCounter + "/3";
        }

        // Check if any guard was seen
        foreach (GuardModulController guard in guards)
        {
            if (guard.wasSeen)
            {
                // Activate the GameOver image
                if (gameOverImage != null)
                {
                    gameOverImage.transform.localScale = new Vector3(-1f, 1f, 1f);
                    Vector3 newPosition = gameOverImage.transform.position;
                    newPosition.x += 10f; // Add 10 to the x coordinate
                    gameOverImage.transform.position = newPosition;
                    gameOverImage.SetActive(true);
                }

                // Stop the game by setting Time.timeScale to 0
                Time.timeScale = 0f;

                // Disable input by disabling the PlayerGreekController script
                if (playController != null)
                {
                    playController.enabled = false;
                }

                // Exit the loop as game over conditions are met
                return;
            }
        }

        // Check if the player has won
        if (GreekCounter >= 3)
        {
            // Activate the WinScreen image
            if (winScreenImage != null)
            {
                winScreenImage.SetActive(true);
            }

            // Stop the game by setting Time.timeScale to 0
            Time.timeScale = 0f;

            // Disable input by disabling the PlayerGreekController script
            if (playController != null)
            {
                playController.enabled = false;
            }

            // Exit the loop as win conditions are met
            return;
        }
    }
}
