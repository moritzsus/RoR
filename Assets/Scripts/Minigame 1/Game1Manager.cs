using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    private static Game1Manager instance = null;

    private static float GreekCounter = 0;
    private static bool gameOver = false;


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

    public static float GetGreekCounter()
    {
        return GreekCounter;
    }

    public static void SetGreekCounter()
    {
        GreekCounter++;
    }

    public static bool GetGameOver()
    {
        return gameOver;
    }

    public static void SetGameOver(bool value)
    {
        gameOver = value;
    }
}
