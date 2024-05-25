using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject lostCanvas;

    private static Game1Manager instance = null;

    [SerializeField] private bool isGameRunning = false;
    [SerializeField] private bool gameOver = false;
    [SerializeField] private int GreekCounter = 0;

    private List<GuardModulController> guards;
    private PlayerGreekController playController;

    private void Awake()
    {
        MainManager.GetInstance().SetCurrentScene("Minigame 1");
        guards = new List<GuardModulController>();

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        playController = FindObjectOfType<PlayerGreekController>();

        mainCanvas.SetActive(true);
        winCanvas.SetActive(false);
        lostCanvas.SetActive(false);
        isGameRunning = false;
        gameOver = false;
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
            GreekCounter = playController.GetGreekCounter();
        }

        // Check if any guard was seen
        foreach (GuardModulController guard in guards)
        {
            if (guard.wasSeen)
            {
                // Activate the GameOver screen
                mainCanvas.SetActive(false);
                winCanvas.SetActive(false);
                lostCanvas.SetActive(true);
                isGameRunning = false;

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
        if (GreekCounter >= 1)
        {
            // Activate the WinScreen screen
            mainCanvas.SetActive(false);
            winCanvas.SetActive(true);
            lostCanvas.SetActive(false);
            isGameRunning = false;
            MainManager.GetInstance().SetGameCompleted(1);

            // Disable input by disabling the PlayerGreekController script
            if (playController != null)
            {
                playController.enabled = false;
            }

            // Exit the loop as win conditions are met
            return;
        }
    }

    public bool IsGameRunning()
    {
        return isGameRunning;
    }

    public void SetIsGameRunning(bool isRunning)
    {
        isGameRunning = isRunning;
        mainCanvas.SetActive(false);
    }
}
