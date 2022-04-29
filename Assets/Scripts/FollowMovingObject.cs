using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovingObject : MonoBehaviour
{
    public GameObject arm1, arm2, leg1, leg2,head,hip;
    public GameObject cube1, cube2, cube3, cube4, headobj,hipObj;

    private void Update()
    {
        arm1.transform.position = cube1.transform.position;
        arm2.transform.position = cube2.transform.position;
        leg1.transform.position = cube3.transform.position;
        leg2.transform.position = cube4.transform.position;
        head.transform.position = headobj.transform.position;
        hip.transform.position = hipObj.transform.position;
    }


}
