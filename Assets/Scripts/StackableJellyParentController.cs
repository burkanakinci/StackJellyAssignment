using UnityEngine;

public class StackableJellyParentController : MonoBehaviour, IPooledObject
{
    [SerializeField] private StackableJellyController stackableJelly;
    public SkinnedMeshRenderer rendererObject
    {
        get
        {
            if(rendererObject == null)
            {
                rendererObject = stackableJelly.stackableRenderer;
            }
            return rendererObject;
        }
        set
        {
            return;
        }
    }

    
    public void OnObjectSpawn()
    {
        stackableJelly.transform.localScale = transform.localScale;

        transform.localScale = Vector3.one;

        stackableJelly.OnSpawnStackableJelly();
        stackableJelly.transform.localPosition = Vector3.zero;
    }
    public void OnObjectDeactive()
    {
        ObjectPool.Instance.DeactiveObject("StackableJelly", this);

        this.gameObject.SetActive(false);
    }
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

}
