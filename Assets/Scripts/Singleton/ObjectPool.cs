using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string prefabTag;
        public GameObject pooledPrefab;
        public int size=1;
        public Transform prefabParent;
    }
    public static ObjectPool Instance { get; private set; }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<IPooledObject>> poolDictionary;
    private Dictionary<string, Queue<IPooledObject>> activeOnScene;

    private GameObject spawnOnPool;
    private GameObject tempSpawned;
    private IPooledObject tempPooled;

    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<IPooledObject>>();
        activeOnScene = new Dictionary<string, Queue<IPooledObject>>();

        for (int i = 0; i < pools.Count; i++)
        {
            Queue<IPooledObject> activeOnPool = new Queue<IPooledObject>();
            activeOnScene.Add(pools[i].prefabTag,activeOnPool);
            Queue<IPooledObject> objectsOnPool = new Queue<IPooledObject>();

            for (int j = 0; j < pools[i].size; j++)
            {
                tempSpawned = Instantiate(pools[i].pooledPrefab, pools[i].prefabParent);
                tempSpawned.SetActive(false);
                objectsOnPool.Enqueue(tempSpawned.GetComponent<IPooledObject>());
            }
            poolDictionary.Add(pools[i].prefabTag, objectsOnPool);
            
        }
    }

    public IPooledObject SpawnFromPool(string _prefabTag, Vector3 _position, Quaternion _rotation,Vector3 _localScale)
    {
        if (!poolDictionary.ContainsKey(_prefabTag))
        {
            return null;
        }

        if (poolDictionary[_prefabTag].Count > 0)
        {
            tempPooled = poolDictionary[_prefabTag].Dequeue();
            spawnOnPool = tempPooled.GetGameObject();
        }
        else
        {
            for (int i = pools.Count - 1; i >= 0; i--)
            {
                if (pools[i].prefabTag == _prefabTag)
                {
                    spawnOnPool = Instantiate(pools[i].pooledPrefab, pools[i].prefabParent);
                    tempPooled = spawnOnPool.GetComponent<IPooledObject>();
                    break;
                }
            }
        }

        spawnOnPool.SetActive(true);
        spawnOnPool.transform.position = _position;
        spawnOnPool.transform.rotation = _rotation;
        spawnOnPool.transform.localScale = _localScale;

        tempPooled.OnObjectSpawn();

        activeOnScene[_prefabTag].Enqueue(tempPooled);

        return tempPooled;
    }

    public void DeactiveObject(string _prefabTag,IPooledObject _iPooled)
    {
        poolDictionary[_prefabTag].Enqueue(_iPooled);
    }

    public void SceneObjectToPool()
    {
        for(int i=pools.Count-1;i>=0;i--)
        {
            for(int j=activeOnScene[pools[i].prefabTag].Count-1;j>=0;j--)
            {
                poolDictionary[pools[i].prefabTag].Peek();
                activeOnScene[pools[i].prefabTag].Dequeue().GetGameObject().SetActive(false) ;
            }

        }
    }

    public void SetStackableScaleOnScene(ref Transform _parent)
    {

            for (int j = activeOnScene["StackableJelly"].Count - 1; j >= 0; j--)
            {
            activeOnScene["StackableJelly"].ToArray()[j].GetGameObject().transform.localScale = _parent.localScale;
            }

        
    }
}
