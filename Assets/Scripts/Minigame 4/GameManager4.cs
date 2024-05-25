using System.Collections;
using UnityEngine;

public class GameManager4 : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject lostCanvas;
    [SerializeField] private GameObject pauseCanvas;

    private int guardsToHit = 10;
    private int guardsToEscape = 5;

    private bool paused = false;
    private bool isGameRunning = false;
    private static readonly float throwCooldown = 0.5f;
    [SerializeField] private GameObject axe;
    private static GameObject currentAxe;
    private static bool isThrowing = false;
    private static bool allowToThrow = true;
    private static GameManager4 instance = null;

    private static int guardsHit = 0;
    private static int guardsEscaped = 0;

    public static bool gameFinished = false;

    public static GameManager4 GetInstance()
    {
        return instance;
    }


    private void Awake()
    {
        MainManager.GetInstance().SetCurrentScene("Minigame 4");

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

        isThrowing = false;
        allowToThrow = true;
        guardsHit = 0;
        guardsEscaped = 0;
        gameFinished = false;

        currentAxe = Instantiate(axe, new Vector3(0, -8, 0), Quaternion.identity);
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
    }

    public void IncreaseGuardsHit()
    {
        guardsHit++;
        if (guardsHit == guardsToHit)
        {
            gameFinished = true;
            isGameRunning = false;
            OnPlayerWon();
        }
    }
    public void IncreaseGuardsEscaped()
    {
        guardsEscaped++;
        if (guardsEscaped == guardsToEscape)
        {
            gameFinished = true;
            isGameRunning = false;
            OnPlayerDied();
        }
    }
    public static GameObject GetCurrentAxe()
    {
        return currentAxe;
    }
    public static bool GetIsThrowing()
    {
        return isThrowing;
    }
    public void ThrowAxe()
    {
        StartCoroutine(ThrowDelay());
    }
    private IEnumerator ThrowDelay()
    {
        isThrowing = true;
        allowToThrow = false;
        yield return new WaitForSeconds(throwCooldown);
        currentAxe = Instantiate(axe, new Vector3(0, -8, 0), Quaternion.identity);
        allowToThrow = true;
        isThrowing = false;
    }
    public bool GetAllowedToThrow()
    {
        return allowToThrow;
    }

    public void OnPlayerWon()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainCanvas.SetActive(false);
        winCanvas.SetActive(true);
        lostCanvas.SetActive(false);
        MainManager.GetInstance().SetGameCompleted(4);
    }

    public void OnPlayerDied()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainCanvas.SetActive(false);
        winCanvas.SetActive(false);
        lostCanvas.SetActive(true);
    }

    public void OnPause()
    {
        paused = true;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        paused = false;
        pauseCanvas.SetActive(false);
    }
}
