using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private CharacterController controller;

    private void Awake()
    {
        TryGetComponent(out controller);
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveStep = Vector3.zero;
        moveStep += transform.forward * inputY * Time.deltaTime;
        moveStep += transform.right * inputX * Time.deltaTime;
        controller.Move(moveStep.normalized * moveSpeed * Time.deltaTime);
    }
}
