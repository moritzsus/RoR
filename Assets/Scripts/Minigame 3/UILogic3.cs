using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic3 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void HideCanvas()
    {
        GameManager3.GetInstance().SetIsGameRunning(true);
    }
}
