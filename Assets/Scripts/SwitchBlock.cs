using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
    {
    public MoveObject[] targetObject;
    Animator anim;
    bool isOpen;
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.Switch();
            anim.SetTrigger("SwitchOn");
            foreach (MoveObject item in targetObject)
            {
                item.SetMoveObject();
            }
        }
    }
}
