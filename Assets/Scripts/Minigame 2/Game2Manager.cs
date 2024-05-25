using UnityEngine;

public class Game2Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject lostCanvas;
    [SerializeField] private GameObject pauseCanvas;

    private static Game2Manager instance = null;

    private bool paused = false;
    private bool isGameRunning = false;
    private bool reachedFinish = false;
    private bool gameOver = false;
    private bool eggStolen = false;
    private bool chickenCaughtPlayer = false;

    public static Game2Manager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        MainManager.GetInstance().SetCurrentScene("Minigame 2");

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        mainCanvas.SetActive(true);
        winCanvas.SetActive(false);
        lostCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameRunning)
        {
            if (paused)
                OnResume();
            else
                OnPause();
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool GetReachedFinish()
    {
        return reachedFinish;
    }

    public void SetReachedFinish(bool value)
    {
        reachedFinish = value;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void SetGameOver(bool value)
    {
        gameOver = value;
    }

    public bool GetEggStolen()
    {
        return eggStolen;
    }

    public void SetEggStolen(bool value)
    {
        eggStolen = value;
    }

    public bool GetChickenCaughtPlayer()
    {
        return chickenCaughtPlayer;
    }

    public void SetChickenCaughtPlayer(bool value)
    {
        chickenCaughtPlayer = value;
    }

    public void OnPlayerWon()
    {
        mainCanvas.SetActive(false);
        winCanvas.SetActive(true);
        lostCanvas.SetActive(false);
        MainManager.GetInstance().SetGameCompleted(2);
    }

    public void OnPlayerDied()
    {
        mainCanvas.SetActive(false);
        winCanvas.SetActive(false);
        lostCanvas.SetActive(true);
    }

    public void OnPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        paused = true;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
