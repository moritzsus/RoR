using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic1 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        Game1Manager.GetInstance().SetIsGameRunning(true);
    }
}
