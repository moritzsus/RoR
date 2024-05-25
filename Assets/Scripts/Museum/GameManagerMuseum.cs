using TMPro;
using UnityEngine;

public class GameManagerMuseum : MonoBehaviour
{
    [SerializeField] private GameObject exitCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;

    private static GameManagerMuseum instance;

    private bool isGameRunning = true;
    private bool paused = false;

    public static GameManagerMuseum GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        MainManager.GetInstance().SetCurrentScene("Museum");

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        exitCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        isGameRunning = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    public bool GetIsGameRunning()
    {
        return isGameRunning;
    }

    public void OnPlayerExit()
    {
        isGameRunning = false;
        exitCanvas.SetActive(true);
        scoreText.text = "Score: " + MainManager.GetInstance().GetAmountOfGamesCompleted() + "/4";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPause()
    {
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        pauseCanvas.SetActive(false);
    }
}
