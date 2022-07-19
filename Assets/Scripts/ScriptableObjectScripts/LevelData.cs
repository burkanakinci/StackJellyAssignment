using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public float[] stackablesXScale;
    public float[] stackablesZPos;//stackable scale x and position z values
    public int platformCount;
}
