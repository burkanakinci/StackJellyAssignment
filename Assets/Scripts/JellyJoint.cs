using UnityEngine;
using System;
using DG.Tweening;
using System.Collections.Generic;
[Serializable]
public class JellyJoint
{
    public enum Direction
    {
        Up,
        Down
    }
    public Direction jointDirection;
    public Transform joint;
    [HideInInspector]public Vector3 startPos;

    private void ResetJointPos()
    {
        joint.DOKill();
        joint.localPosition = startPos;
    }

    public static void ShakeJellyJoints(ref List<JellyJoint> _jellyJoints,ref ShakeData _shakeData)
    {
        for(int i=_jellyJoints.Count-1;i>=0;i--)
        {
            if(_jellyJoints[i].jointDirection!=Direction.Up)
                continue;

            _jellyJoints[i].ResetJointPos();

            _jellyJoints[i].joint.DOShakePosition(_shakeData.duration,_shakeData.strength,_shakeData.vibrato).SetEase(Ease.InOutExpo);

        }
    }
}

