using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic3 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        GameManager3.GetInstance().SetIsGameRunning(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Resume()
    {
        GameManager3.GetInstance().OnResume();
    }
}
