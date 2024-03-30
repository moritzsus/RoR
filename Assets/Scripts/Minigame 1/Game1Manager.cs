using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    private static Game1Manager instance = null;

    public int GreekCounter = 0;

    public bool gameOver = false;

    private List<GuardModulController> guards = new List<GuardModulController>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
        foreach (GuardModulController guard in guards)
        {
            if (guard.wasSeen)
            {   GreekCounter++;
                gameOver = true;
                Debug.Log("Game Over");
                break;
            }
        }
    }
}
