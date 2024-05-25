using UnityEngine;

public class GameManager3 : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject lostCanvas;
    [SerializeField] private GameObject pauseCanvas;

    public static GameManager3 instance;

    private bool paused = false;
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
        if (isRunning)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnPlayerWon()
    {
        isGameRunning = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainCanvas.SetActive(false);
        winCanvas.SetActive(true);
        lostCanvas.SetActive(false);
        MainManager.GetInstance().SetGameCompleted(3);
    }

    public void OnPlayerDied()
    {
        isGameRunning = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
