using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic2 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        Game2Manager.GetInstance().SetIsGameRunning(true);
    }
}
