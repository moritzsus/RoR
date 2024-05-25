using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float airMultiplier;

    public Transform orientation;

    private CharacterController controller;

    private void Awake()
    {
        MainManager.GetInstance().SetCurrentScene("Museum");
        Vector3 savedPosition = MainManager.GetInstance().GetLastPlayerPosition();
        Vector3 savedRotation = MainManager.GetInstance().GetLastPlayerRotation();
        transform.position = savedPosition;
        orientation.rotation = Quaternion.Euler(savedRotation);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!GameManagerMuseum.GetInstance().GetIsGameRunning())
            return;

        MyInput();
    }

    private void MyInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);

        MovePlayer(moveDirection.normalized);
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
