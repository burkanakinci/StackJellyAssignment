using UnityEngine;

public class JellyMovementController : MonoBehaviour
{
    private Vector3 targetScale;
    private float tempScaleX;

    private Vector3 firstMousePos;
    private float verticalMovementChange, changeOfMousePos;

    private bool isCollidedObstacle;

    private float minScaleChangeMultiplier, verticalChangeMultiplier;

    [HideInInspector]public Vector3 movementParentScale;

    public Transform jellyMovementParent;
    public JellyMovementData movementData;

    private void Start()
    {
        targetScale = Vector3.one;
        movementParentScale = Vector3.one;
        tempScaleX = 1f;

        isCollidedObstacle = false;

        minScaleChangeMultiplier = 1 / movementData.maxScaleChangeMultiplier;
        verticalChangeMultiplier = 2f * (movementData.maxScaleChangeMultiplier - minScaleChangeMultiplier);
    }
    public void MovementForward()
    {
        if (isCollidedObstacle)
        {
            return;
        }

        jellyMovementParent.position = jellyMovementParent.position + Vector3.Lerp(Vector3.zero, Vector3.forward, movementData.forwardSpeed * Time.deltaTime);
    }

    public void ChangeScaleJelly()
    {

        transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, targetScale.x, movementData.scaleChangeLerpValue * Time.deltaTime),
                                            Mathf.Lerp(transform.localScale.y, targetScale.y, movementData.scaleChangeLerpValue * Time.deltaTime),
                                            transform.localScale.z);
    }
    public void CheckControls()
    {
        if (Input.GetMouseButtonDown(0))
        {

            firstMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            changeOfMousePos = Input.mousePosition.y - firstMousePos.y;

            if (Mathf.Abs(changeOfMousePos) > 0.1f)
            {
                verticalMovementChange = (changeOfMousePos * 1 / Screen.height);
                firstMousePos = Input.mousePosition;

                tempScaleX = Mathf.Clamp(targetScale.x - (verticalMovementChange * verticalChangeMultiplier), minScaleChangeMultiplier, movementData.maxScaleChangeMultiplier);
                targetScale = new Vector3(tempScaleX,
                                       (Mathf.Clamp(1f / tempScaleX, minScaleChangeMultiplier, movementData.maxScaleChangeMultiplier)),
                                       (targetScale.z)); ;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            verticalMovementChange = 0;
            firstMousePos = Input.mousePosition;
        }
    }
}
