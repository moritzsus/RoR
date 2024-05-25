using TMPro;
using UnityEngine;

public class GameManagerMuseum : MonoBehaviour
{
    [SerializeField] private GameObject exitCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;

    private static GameManagerMuseum instance;

    private bool isGameRunning = true;

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
        isGameRunning = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
}
