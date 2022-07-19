using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int level
    {
        get
        {
            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", 1);
            }

            return PlayerPrefs.GetInt("Level");

        }

        set => PlayerPrefs.SetInt("Level", value);
    }

    public event Action levelStart;
    private LevelData levelData;
    private int maxLevelObject=1;
    private int tempLevel;

    public Transform startPlatform, finishPlatform;

    public bool fpsLock=true;

    public ParticleSystem finishConfettiParticle;

    private void Awake()
    {
        Instance = this;

        if (fpsLock)
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }

        levelStart += CleanSceneObject;
        levelStart += SpawnSceneObject;
    }
    private void Start()
    {
        maxLevelObject=  Resources.LoadAll("LevelScriptableObjects", typeof(LevelData)).Length;

        StartLevelStartAction();
    }

    public void StartLevelStartAction()
    {
        levelStart?.Invoke();
    }

    private void CleanSceneObject()
    {
        ObjectPool.Instance.SceneObjectToPool();
    }
    private void SpawnSceneObject()
    {
        GetLevelData();

        startPlatform.parent.position = Vector3.zero;

        for (int i = levelData.platformCount-1; i >= 0; i--)
        {
           ObjectPool.Instance.SpawnFromPool("Platform", Vector3.forward * 10f * i, Quaternion.identity,Vector3.one);
        }
        for (int j = levelData.stackablesZPos.Length - 1; j >= 0; j--)
        {
            ObjectPool.Instance.SpawnFromPool("StackableJelly", Vector3.forward * levelData.stackablesZPos[j], Quaternion.identity, new Vector3(levelData.stackablesXScale[j],(1f/ levelData.stackablesXScale[j]),1f));
        }

        finishPlatform.parent.position = Vector3.forward * (levelData.platformCount) * 10f;
        finishPlatform.parent.localEulerAngles = new Vector3(0f,180f,0f);


    }
    private void GetLevelData()
    {
        tempLevel = (level % maxLevelObject) > 0 ? (level % maxLevelObject) : maxLevelObject;

        levelData = Resources.Load<LevelData>("LevelScriptableObjects/Level" + tempLevel);
    }
}
