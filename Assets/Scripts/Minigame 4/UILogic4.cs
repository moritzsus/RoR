using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic4 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        GameManager4.GetInstance().SetIsGameRunning(true);
    }
}
