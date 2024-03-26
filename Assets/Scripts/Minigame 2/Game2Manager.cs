using UnityEngine;

public class Game2Manager : MonoBehaviour
{
    private static Game2Manager instance = null;

    private static bool reachedFinish = false;
    private static bool gameOver = false;
    private static bool eggStolen = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static Game2Manager GetInstance()
    {
        return instance;
    }

    public static bool GetReachedFinish()
    {
        return reachedFinish;
    }

    public static void SetReachedFinish(bool value)
    {
        reachedFinish = value;
    }

    public static bool GetGameOver()
    {
        return gameOver;
    }

    public static void SetGameOver(bool value)
    {
        gameOver = value;
    }

    public static bool GetEggStolen()
    {
        return eggStolen;
    }

    public static void SetEggStolen(bool value)
    {
        eggStolen = value;
    }
}
