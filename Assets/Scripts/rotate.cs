using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rotate : MonoBehaviour
{
    public Vector3 direction;
    public float rotateSpeed;
    [SerializeField] Vector3 pos1;
    [SerializeField] Vector3 pos2;
    [SerializeField] float speed;
    [SerializeField] bool isMovingObject;
    public bool loop;
    public bool goToPos2;
    private int pos;

    float distance;
    private void Start() {
        distance = (pos2 - pos1).magnitude;
        distance = distance/speed;
        if(loop){
            MoveRotate();
        }
    }

    private void Update()
    {
        transform.Rotate(direction * rotateSpeed * Time.deltaTime);
        if(loop){
            if(pos == 0)
                this.transform.position = Vector3.MoveTowards(this.transform.position, pos1, Time.deltaTime * speed);
            else if(this.transform.position == pos1)
                this.transform.position = Vector3.MoveTowards(this.transform.position, pos2, Time.deltaTime * speed);
        }
        else
        {
            if(isMovingObject){
            if(goToPos2 && this.transform.position != pos2)
                this.transform.position = Vector3.MoveTowards(this.transform.position, pos2, Time.deltaTime * speed);
            else if(!goToPos2 && this.transform.position != pos1)
                this.transform.position = Vector3.MoveTowards(this.transform.position, pos1, Time.deltaTime * speed);
            }
        }
    }

    public void MoveRotate(){
        if(pos == 0){
            transform.DOLocalMove(pos2, distance, false).SetEase(Ease.Linear).OnComplete(()=>{
                pos = 1;
                MoveRotate();
            });
        }
        else
        {
            transform.DOLocalMove(pos1, distance, false).SetEase(Ease.Linear).OnComplete(()=>{
                pos = 0;
                MoveRotate();
            });
        }
    }
}
