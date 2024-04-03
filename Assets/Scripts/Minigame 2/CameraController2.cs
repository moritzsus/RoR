using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    private void LateUpdate()
    {
        if (!Game2Manager.GetGameOver())
            transform.position = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
    }
}
