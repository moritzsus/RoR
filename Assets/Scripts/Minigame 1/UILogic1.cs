using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic1 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        Game1Manager.GetInstance().SetIsGameRunning(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Game1Manager.GetInstance().OnResume();
    }
}
