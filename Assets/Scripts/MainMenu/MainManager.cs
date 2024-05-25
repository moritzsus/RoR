using UnityEngine;

public class MainManager : MonoBehaviour
{
    private static MainManager instance = null;
    private Vector3 lastPlayerPosition;
    private Vector3 lastPlayerRotation;
    private string currentScene = "MainMenu";

    private bool[] gamesCompleted = new bool[] { false, false, false, false };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            lastPlayerPosition = new Vector3(7.0f, 1.1f, 0f);
            lastPlayerRotation = Vector3.zero;
        }
        else
            Destroy(gameObject);
    }

    public static MainManager GetInstance()
    {
        return instance;
    }

    public Vector3 GetLastPlayerPosition()
    {
        return lastPlayerPosition;
    }

    public void SetLastPlayerPosition(Vector3 pos)
    {
        lastPlayerPosition = new Vector3(pos.x, pos.y, pos.z);
    }

    public Vector3 GetLastPlayerRotation()
    {
        return lastPlayerRotation;
    }

    public void SetLastPlayerRotation(Vector3 rot)
    {
        lastPlayerRotation = new Vector3(rot.x, rot.y, rot.z);
    }

    public void ResetPosition()
    {
        lastPlayerPosition = new Vector3(7.0f, 1.1f, 0f);
        lastPlayerRotation = Vector3.zero;
    }

    public string GetCurrentScene()
    {
        return currentScene;
    }

    public void SetCurrentScene(string scene)
    {
        currentScene = scene;

        if (scene == "Museum")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public int GetAmountOfGamesCompleted()
    {
        int num = 0;
        foreach (bool b in gamesCompleted)
        {
            if (b)
                num++;
        }
        return num;
    }

    public void SetGameCompleted(int gameNumber)
    {
        if (gameNumber > 0 && gameNumber < 5)
            gamesCompleted[gameNumber - 1] = true;
    }

    public void ResetGamesCompleted()
    {
        gamesCompleted = new bool[] { false, false, false, false };
    }
}
