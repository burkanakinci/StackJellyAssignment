using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class JellyManager : MonoBehaviour
{
    public static JellyManager Instance { get; private set; }

    [SerializeField] private GameStateMachine gameStateMachine;

    public List<StackableJellyController> stackeds { get; private set; }
    private Vector3 tempStackJellyPos, tempStackJellyScale,tempScale;

    [SerializeField] private List<JellyJoint> jellyJoints;
    public JellyMovementController jellyMovementController { get; private set; }
    [SerializeField] private ShakeData shakeData;

    [SerializeField]private SkinnedMeshRenderer jellyRenderer;
    private Material jellyMaterial;
    private IPooledObject tempPiece;

    [SerializeField] private Transform plasticBag;

    [SerializeField]private int jellyAmount=0;

    private void Awake()
    {
        Instance = this;

        stackeds = new List<StackableJellyController>();

        jellyMovementController = GetComponent<JellyMovementController>();

        GameManager.Instance.levelStart += gameStateMachine.ChangeInitialState;
    }
    private void Start()
    {
        jellyMaterial = jellyRenderer.material;

        for (int i = jellyJoints.Count - 1; i >= 0; i--)
        {
            jellyJoints[i].startPos = jellyJoints[i].joint.localPosition;
        }
    }



    public void ResetJellyValues()
    {
        jellyMovementController.jellyMovementParent.position = Vector3.zero;
        transform.position = GameManager.Instance.startPlatform.position ;


        transform.SetParent(null);
        jellyMovementController.jellyMovementParent.localScale = Vector3.one;
        transform.SetParent(jellyMovementController.jellyMovementParent);
        transform.localScale = Vector3.one;

        transform.DOKill();

        stackeds.Clear();

    }

    public void JumpToPlayingPos()
    {
        transform.DOLocalJump(Vector3.zero, jellyMovementController.movementData.jumpPower, 1, jellyMovementController.movementData.jumpDuration).
            OnComplete(()=>ShakeJelly());
    }

    public GameStateMachine GetJellyGameStateMachine()
    {
        return gameStateMachine;
    }

    public void ShakeJelly()
    {
        JellyJoint.ShakeJellyJoints(ref jellyJoints,ref shakeData);

        for(int i= stackeds.Count-1;i>=0;i--)
        {
            stackeds[i].ShakeStackedJelly();
        }
    }


    public void StackJelly(StackableJellyController _stackable)
    {

        if (Mathf.Abs(_stackable.transform.localScale.x - transform.localScale.x) > 0.1)
        {
            if (transform.localScale.x < _stackable.transform.localScale.x)
            {
                tempStackJellyScale = new Vector3(( _stackable.transform.localScale.x- transform.localScale.x) * jellyMovementController.movementParentScale.x / 2f,
                _stackable.transform.localScale.y * jellyMovementController.movementParentScale.y, 1f);

                tempStackJellyPos = (Vector3.right * (transform.localScale.x + (( _stackable.transform.localScale.x- transform.localScale.x ) / 2f) / 2f));

                tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", _stackable.transform.position + tempStackJellyPos, Quaternion.identity, tempStackJellyScale);
                tempPiece.rendererObject.material = _stackable.stackableRenderer.material;
                tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", _stackable.transform.position + (tempStackJellyPos * -1f), Quaternion.identity, tempStackJellyScale);
                tempPiece.rendererObject.material = _stackable.stackableRenderer.material;
                //
                tempStackJellyScale = new Vector3(transform.localScale.x * jellyMovementController.movementParentScale.x,
                        ( transform.localScale.y -_stackable.transform.localScale.y ) * jellyMovementController.movementParentScale.y, 1f);

                tempStackJellyPos = (Vector3.up * _stackable.transform.localScale.y * 2f);

                for (int i = stackeds.Count - 1; i >= 0; i--)
                {

                    tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", stackeds[i].transform.position + tempStackJellyPos, Quaternion.identity, tempStackJellyScale);
                    tempPiece.rendererObject.material = stackeds[i].stackableRenderer.material;
                }
                tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", transform.position + tempStackJellyPos, Quaternion.identity, tempStackJellyScale);
                tempPiece.rendererObject.material = jellyRenderer.material;

            }
            else
            {

                tempStackJellyScale = new Vector3(_stackable.transform.localScale.x * jellyMovementController.movementParentScale.x,
                        (_stackable.transform.localScale.y - transform.localScale.y) * jellyMovementController.movementParentScale.y, 1f);

                tempStackJellyPos = _stackable.transform.position + (Vector3.up * transform.localScale.y * 2f);

                tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", tempStackJellyPos, Quaternion.identity, tempStackJellyScale);
                tempPiece.rendererObject.material = _stackable.stackableRenderer.material;

                tempStackJellyScale = new Vector3((transform.localScale.x - _stackable.transform.localScale.x)  * jellyMovementController.movementParentScale.x / 2f,
                        transform.localScale.y * jellyMovementController.movementParentScale.y, 1f);

                tempStackJellyPos = (Vector3.right * (_stackable.transform.localScale.x + ((transform.localScale.x - _stackable.transform.localScale.x) / 2f) / 2f));

                for (int i = stackeds.Count - 1; i >= 0; i--)
                {

                    tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", stackeds[i].transform.position + tempStackJellyPos, Quaternion.identity, tempStackJellyScale);
                    tempPiece.rendererObject.material = stackeds[i].stackableRenderer.material;
                    tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", stackeds[i].transform.position + (tempStackJellyPos*-1f), Quaternion.identity, tempStackJellyScale);
                    tempPiece.rendererObject.material = stackeds[i].stackableRenderer.material;
                }

                tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", transform.position + tempStackJellyPos, Quaternion.identity, tempStackJellyScale);
                tempPiece.rendererObject.material = jellyRenderer.material;
                tempPiece = ObjectPool.Instance.SpawnFromPool("StackablePiece", transform.position + (tempStackJellyPos * -1f), Quaternion.identity, tempStackJellyScale);
                tempPiece.rendererObject.material = jellyRenderer.material;

            }

            tempScale = new Vector3((transform.localScale.x <= _stackable.transform.localScale.x) ? transform.localScale.x : _stackable.transform.localScale.x,
                            (transform.localScale.y <= _stackable.transform.localScale.y) ? transform.localScale.y : _stackable.transform.localScale.y,
                            1f);

            tempScale.x /= transform.localScale.x;
            tempScale.y /= transform.localScale.y;

            jellyMovementController.movementParentScale.x *= tempScale.x;
            jellyMovementController.movementParentScale.y *= tempScale.y;
        }
        else
        {
            tempStackJellyScale = Vector3.one;

            _stackable.stackableParticle.Play();
        }

        _stackable.transform.localScale = transform.localScale;
        _stackable.transform.SetParent(this.transform);
        _stackable.stackableJellyParent.transform.localScale = Vector3.one;
        stackeds.Add(_stackable);

        jellyMovementController.jellyMovementParent.localScale = jellyMovementController.movementParentScale;

        ObjectPool.Instance.SetStackableScaleOnScene(ref jellyMovementController.jellyMovementParent);

        ShakeJelly();
    }

    public void StartFinishMovement()
    {
        jellyAmount = 0;

        StartCoroutine(StackedsMoveOnBag());
    }
    private void EndFinishMovement()
    {

        UIManager.Instance.ShowSuccessPanel();
    }
    private IEnumerator<WaitForSeconds> StackedsMoveOnBag()
    {
        yield return new WaitForSeconds(jellyMovementController.movementData.finishJumpRate);

        if (stackeds.Count > 0)
        {
            jellyAmount += (int)(jellyMovementController.jellyMovementParent.localScale.x * jellyMovementController.jellyMovementParent.localScale.y * 20f);


            stackeds[0].stackableJellyParent.localScale = Vector3.one;
            stackeds[0].transform.SetParent(stackeds[0].stackableJellyParent);

            stackeds[0].transform.DOJump(plasticBag.transform.position, jellyMovementController.movementData.finishJumpPower, 1, jellyMovementController.movementData.finishJumpDuration).
                OnComplete(()=>UIManager.Instance.IncreasePlasticBagJellyAmount(ref jellyAmount));
            stackeds.Remove(stackeds[0]);
            StartCoroutine(StackedsMoveOnBag());
        }
        else
        {
            transform.DOJump(plasticBag.transform.position, jellyMovementController.movementData.finishJumpPower, 1, jellyMovementController.movementData.finishJumpDuration).
                    OnComplete(() => EndFinishMovement());
        }
    }

}
