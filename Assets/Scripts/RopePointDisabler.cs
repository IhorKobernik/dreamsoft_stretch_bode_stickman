using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePointDisabler : MonoBehaviour
{
    public GameObject Rope, AttachedObject;

    private void OnEnable()
    {
        Rope.SetActive(false);
        AttachedObject.GetComponent<HingeJoint>().connectedBody = null;
    }
}
