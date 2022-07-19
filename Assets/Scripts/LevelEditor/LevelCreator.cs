
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

using System;

public class LevelCreator : MonoBehaviour
{
    [HideInInspector] public int createdLevelNumber;
    private LevelData tempLevelData;
    private string savePath;

    private GameObject[] tempStackables;
    public void CreateLevel()
    {
        tempLevelData = ScriptableObject.CreateInstance<LevelData>();

        savePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelScriptableObjects/Level" + createdLevelNumber + ".asset");

        tempLevelData.platformCount = GameObject.FindGameObjectsWithTag("Platform").Length;

        tempStackables = GameObject.FindGameObjectsWithTag("StackableJelly");
        tempLevelData.stackablesXScale = new float[tempStackables.Length];
        tempLevelData.stackablesZPos = new float[tempStackables.Length];
        for (int i=tempStackables.Length-1;i>=0;i--)
        {
            tempLevelData.stackablesXScale[i] = tempStackables[i].transform.localScale.x;
            tempLevelData.stackablesZPos[i] = tempStackables[i].transform.position.z;
        }

        AssetDatabase.CreateAsset(tempLevelData, savePath);
        AssetDatabase.SaveAssets();

    }

}
#endif

