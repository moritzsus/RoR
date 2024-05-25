using UnityEngine;

public class Follow_player : MonoBehaviour
{

    public Transform player;

    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(11, 6, -13);
    }
}