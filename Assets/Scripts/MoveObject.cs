using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 startPostion;
    public Vector3 endPosition;
    float distance;
    private void Start() {
        distance = (endPosition - startPostion).magnitude;
        distance = distance/speed;
    }
    public void SetMoveObject(){
        transform.DOLocalMove(endPosition, distance, false).SetEase(Ease.Linear);
    }

}
