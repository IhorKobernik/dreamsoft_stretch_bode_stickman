using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPointSnap : MonoBehaviour
{
    //Vector3 direction1 = Vector3.left;
    //Vector3 direction2 = Vector3.right;
    //Vector3 direction3 = Vector3.up;
    //Vector3 direction4 = Vector3.down;
    public Transform target;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            Vector3 globalPositionOfContact = collision.contacts[0].point;
            target.transform.position = globalPositionOfContact;
        }
    }
    
   
}
