using System.Collections.Generic;
using UnityEngine;

public class StackablePieceController : MonoBehaviour,IPooledObject
{
    [SerializeField] private List<JellyJoint> jellyJoints;
    [SerializeField] private ShakeData shakeData;
    [SerializeField] private SkinnedMeshRenderer pieceRenderer;

    public SkinnedMeshRenderer rendererObject
    {
        get
        {
            
            return pieceRenderer;
        }
        set
        {
            return;
        }
    }

    private void Awake()
    {
        for (int i = jellyJoints.Count - 1; i >= 0; i--)
        {
            jellyJoints[i].startPos = jellyJoints[i].joint.localPosition;
        }
    }

    //public void ChangeMaterial(ref SkinnedMeshRenderer _renderer)
    //{
    //    rendererObject.material = _renderer.material;
    //}

    public void OnObjectSpawn()
    {
        ShakeStackedJelly();
    }
    public void OnObjectDeactive()
    {
        ObjectPool.Instance.DeactiveObject("StackablePiece", this);

        this.gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public StackablePieceController GetCachedComponent()
    {
        return this;
    }

    public void ShakeStackedJelly()
    {
        JellyJoint.ShakeJellyJoints(ref jellyJoints,ref shakeData);
    }

}
