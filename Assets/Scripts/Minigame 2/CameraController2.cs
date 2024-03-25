using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
    }
}
