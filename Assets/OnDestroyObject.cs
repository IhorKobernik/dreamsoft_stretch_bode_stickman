using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyObject : MonoBehaviour
{
    public GameObject psDestroy;

    // private void OnDestroy()
    // {
    //     Instantiate(psDestroy, transform.position, Quaternion.identity);
    // }

    public void psDestroyBox(){
        Instantiate(psDestroy, transform.position, Quaternion.identity);
    }
            
}
