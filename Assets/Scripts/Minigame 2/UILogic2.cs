using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic2 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        Game2Manager.GetInstance().SetIsGameRunning(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Game2Manager.GetInstance().OnResume();
    }
}
