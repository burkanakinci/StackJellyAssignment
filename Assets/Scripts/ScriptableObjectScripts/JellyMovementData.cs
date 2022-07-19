using UnityEngine;

[CreateAssetMenu(fileName = "JellyMovementData", menuName = "Jelly Movement Data")]
public class JellyMovementData : ScriptableObject
{
    public float maxScaleChangeMultiplier = 2.5f;
    public float scaleChangeLerpValue = 8f;
    public float forwardSpeed = 5f;

    [Space]
    public float jumpPower = 2f;
    public float jumpDuration = 1f;

    [Space]
    public float finishJumpPower =5f;
    public float finishJumpDuration = 1f;
    public float finishJumpRate = 0.6f;

}
