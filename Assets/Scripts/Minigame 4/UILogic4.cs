using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic4 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        GameManager4.GetInstance().SetIsGameRunning(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Resume()
    {
        GameManager4.GetInstance().OnResume();
    }
}
