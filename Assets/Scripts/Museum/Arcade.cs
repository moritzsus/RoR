#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arcade : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerOrientation;
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneToLoad;
#endif
    [SerializeField] private string sceneName;

    private float interactDistance = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        if (sceneToLoad != null)
        {
            sceneName = sceneToLoad.name;
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerMuseum.GetInstance().GetIsGameRunning())
            return;

        float distance = (playerTransform.position - transform.position).magnitude;

        if (distance < interactDistance)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                MainManager.GetInstance().SetLastPlayerPosition(playerTransform.position);
                MainManager.GetInstance().SetLastPlayerRotation(playerOrientation.eulerAngles);
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
