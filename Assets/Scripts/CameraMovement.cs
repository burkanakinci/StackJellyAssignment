using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Space]
    [SerializeField] private Vector3 offsetOnPlay;
    [SerializeField] private Transform jelly;


    [Space]
    [SerializeField] private Vector3 offsetOnFinish;
    [SerializeField] private Transform plasticBag;

    private Vector3 lookPos;
    private Quaternion rotation;
    [SerializeField] float rotationLerpValue= 2f;
    [SerializeField] float movementLerpValue = 2f;

    private void LateUpdate()
    {
        MoveCamera();
        LookTarget();
    }
    private void MoveCamera()
    {

        transform.position = Vector3.Lerp(transform.position,
            (JellyManager.Instance.GetJellyGameStateMachine().GetCurrentState()==JellyManager.Instance.GetJellyGameStateMachine().finishState)?(plasticBag.position + offsetOnFinish) :(jelly.position+offsetOnPlay),
            Time.deltaTime * movementLerpValue);
    }
    private void LookTarget()
    {
        lookPos = (JellyManager.Instance.GetJellyGameStateMachine().GetCurrentState() == JellyManager.Instance.GetJellyGameStateMachine().finishState) ?
                (plasticBag.position) : (jelly.position);
         lookPos -= transform.position;
         rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationLerpValue);
    }
    
}