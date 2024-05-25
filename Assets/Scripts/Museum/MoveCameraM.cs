using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    // Update is called once per frame
    private void Update()
    {
        if (!GameManagerMuseum.GetInstance().GetIsGameRunning())
            return;

        transform.position = cameraPosition.position;
    }
}
