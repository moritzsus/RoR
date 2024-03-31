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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Get the TextMeshProUGUI component
        greekCounterText = FindObjectOfType<TextMeshProUGUI>();

        // Find the PlayController in the scene and assign the reference
        playController = FindObjectOfType<PlayerGreekController>();
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
            greekCounterText.text = GreekCounter + "/5";
        }

        // Check if the game is over
        if (GreekCounter >= 5)
        {
            gameOver = true;
            Debug.Log("Game Over");
        }
    }
}
