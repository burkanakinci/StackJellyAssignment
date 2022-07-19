using UnityEngine;

[CreateAssetMenu(fileName = "ShakeData", menuName = "Shake Data")]
public class ShakeData : ScriptableObject
{
    public float duration = 1f;
    public float strength = 0.4f;
    public int vibrato = 10;
}
