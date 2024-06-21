using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private ThirdPersonPlayerController playerController;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 2, -5);
    [SerializeField] private float rotationTime = 10f;
    [SerializeField] private float moveTime = 10f;
    [SerializeField] private float scrollSensitivity = 10f;
    [SerializeField] private Vector2 zClamp = new Vector2(1, 10f);
    [SerializeField] private bool slerpMovement = false;
    [SerializeField] private bool slerpRotation = false;

    private float moveTimer = -1;
    private float rotationTimer = -1;
    private Vector3 moveLerpStart;
    private Vector3 moveLerpTarget;
    private Quaternion rotateLerpStart;
    private Quaternion rotateLerpTarget;

    private Vector3 TargetPosition
    {
        get
        {
            return playerController.transform.position + cameraOffset;
        }
    }
    private Quaternion TargetRotation
    {
        get
        {
            return Quaternion.LookRotation(playerController.transform.position - transform.position);
        }
    }

    private void Update()
    {
        float wheelAxis = Input.GetAxisRaw("Mouse ScrollWheel");
        if (wheelAxis != 0)
        {
            float step = wheelAxis * scrollSensitivity;
            if(cameraOffset.z <= 0 && (cameraOffset.z + step < zClamp.y || cameraOffset.z + step > zClamp.x))
            {
                step = 0;
                //Debug.Log("Camera offset less than zero, and adding step will be less than clamp.");
            }
            else if(cameraOffset.z >= 0 && (cameraOffset.z + step > zClamp.y || cameraOffset.z + step < zClamp.x))
            {
                step = 0;
                //Debug.Log("Camera offset greater than zero, and adding step will be more than clamp.");
            }
            cameraOffset.z += step;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, TargetPosition) >= 0.05f)
        {
            MoveCameraToTarget();
        }

        if(Quaternion.Angle(transform.rotation, TargetRotation) >= 1f)
        {
            RotateCameraToTarget();
        }

        if (moveTimer >= 0)
        {
            moveTimer += Time.deltaTime;
            if (slerpMovement == true)
            {
                transform.position = Vector3.Slerp(moveLerpStart, moveLerpTarget, moveTimer / moveTime);
            }
            else
            {
                transform.position = Vector3.Lerp(moveLerpStart, moveLerpTarget, moveTimer / moveTime);
            }
            if(moveTimer >= moveTime)
            {
                moveTimer = -1;
            }
        }
        if(rotationTimer >= 0)
        {
            rotationTimer += Time.deltaTime;
            if (slerpRotation == true)
            {
                transform.rotation = Quaternion.Slerp(rotateLerpStart, rotateLerpTarget, rotationTimer / rotationTime);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(rotateLerpStart, rotateLerpTarget, rotationTimer / rotationTime);
            }
            if (rotationTimer >= rotationTime)
            {
                rotationTimer = -1;
            }
        }
    }

    private void MoveCameraToTarget()
    {
        if(moveLerpTarget != TargetPosition) 
        {
            moveLerpStart = transform.position;
            moveLerpTarget = TargetPosition;
            moveTimer = 0;
        }
    }

    private void RotateCameraToTarget()
    {
        rotateLerpStart = transform.rotation;
        rotateLerpTarget = TargetRotation;
        rotationTimer = 0;
    }
}
