using UnityEngine;

public class GameManager3 : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject lostCanvas;

    public static GameManager3 instance;

    private bool isGameRunning = false;

    public static GameManager3 GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        MainManager.GetInstance().SetCurrentScene("Minigame 3");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mainCanvas.SetActive(true);
        winCanvas.SetActive(false);
        lostCanvas.SetActive(false);
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

    public void OnPlayerWon()
    {
        mainCanvas.SetActive(false);
        winCanvas.SetActive(true);
        lostCanvas.SetActive(false);
        MainManager.GetInstance().SetGameCompleted(3);
    }

    public void OnPlayerDied()
    {
        mainCanvas.SetActive(false);
        winCanvas.SetActive(false);
        lostCanvas.SetActive(true);
    }
}
