using UnityEngine;

public class DefaultPooledController : MonoBehaviour, IPooledObject
{
    public SkinnedMeshRenderer rendererObject
    {
        get
        {
            return null;
        }
        set
        {
            return;
        }
    }

    public void OnObjectSpawn()
    {
        return;
    }
    public void OnObjectDeactive()
    {
        ObjectPool.Instance.DeactiveObject("Platform",this);

        this.gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
