using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogicMuseum : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        if (sceneName == "Museum")
        {
            MainManager.GetInstance().ResetGamesCompleted();
            MainManager.GetInstance().ResetPosition();
        }
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
