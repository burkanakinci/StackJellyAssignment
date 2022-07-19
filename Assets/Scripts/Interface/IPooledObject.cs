using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;


public interface IPooledObject
{
        SkinnedMeshRenderer rendererObject
        {
            get;
            set;
        }
    void OnObjectSpawn();
    void OnObjectDeactive();
    GameObject GetGameObject();

    //Func<T> funcTest();
}