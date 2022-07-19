using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackableJellyController : MonoBehaviour
{
    [SerializeField] private List<JellyJoint> jellyJoints;

    [SerializeField] private BoxCollider stackableJellyCollider;
    [SerializeField] private ShakeData shakeData;


    public SkinnedMeshRenderer stackableRenderer;

    public Transform stackableJellyParent;

    public ParticleSystem stackableParticle;

    private void Start()
    {
        for (int i = jellyJoints.Count - 1; i >= 0; i--)
        {
            jellyJoints[i].startPos = jellyJoints[i].joint.localPosition;
        }
 }

    public void OnSpawnStackableJelly()
    {
        stackableJellyCollider.isTrigger = false;

        transform.localPosition = Vector3.zero;

        this.gameObject.layer = LayerMask.NameToLayer("StackableJelly");
        this.gameObject.tag = "StackableJelly";

        stackableParticle.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Jelly"))
        {

            JellyManager.Instance.StackJelly(this);

            this.gameObject.layer= LayerMask.NameToLayer("Jelly");
            this.gameObject.tag = "Jelly";

            stackableJellyCollider.isTrigger = true;
        }
        else if (other.CompareTag("Finish"))
        {
            JellyManager.Instance.GetJellyGameStateMachine().ChangeState(
                  JellyManager.Instance.GetJellyGameStateMachine().finishState);
        }
    }

    public void ShakeStackedJelly()
    {
        JellyJoint.ShakeJellyJoints(ref jellyJoints,ref shakeData);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
