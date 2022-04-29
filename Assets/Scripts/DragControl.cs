using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DragControl : MonoBehaviour
{
    public List<DragHandler> listDrag;

    void Awake(){
        Init();
    }

    //init control to list Drag
    private void Init(){
        foreach(var drag in listDrag){
            drag.Init(this);
        }            
    }

    public bool CheckListHit(){
        bool isHaveMove = false;
        if(listDrag.Count(x=>x.isHitWall) > 1)
            isHaveMove = true;
        return isHaveMove;
    }
}
