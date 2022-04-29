using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combinationLockSwitch : MonoBehaviour
{
 public Rigidbody[] targetObjects;
 [SerializeField] GameObject laser;
 [SerializeField] combiationLock lockRef;
 Animator anim;
 public float speed;
 int bodyPartsOnSwitch;
 // Start is called before the first frame update
 bool isOpen;
 void Start()
 {
  anim = GetComponent<Animator>();
  isOpen = false;
 }
 private void OnTriggerEnter(Collider other)
 {
  if (other.gameObject.CompareTag("Player"))
   bodyPartsOnSwitch++;
  if (other.gameObject.CompareTag("Player") && !isOpen)
  {
   AudioManager.instance.Switch();
   anim.SetTrigger("SwitchOn");
   isOpen = true;
   this.gameObject.tag = "Unlocked";
   lockRef.checkForLock();
  }
 }

 private void OnTriggerExit(Collider other)
 {
  if (other.gameObject.CompareTag("Player"))
   bodyPartsOnSwitch--;
  if (other.gameObject.CompareTag("Player") && isOpen)
  {
   if (bodyPartsOnSwitch == 1)
   {
    AudioManager.instance.Switch();
    anim.SetTrigger("SwitchOff");
    isOpen = false;
    this.gameObject.tag = "Untagged";
    lockRef.checkForLock();
   }
  }
 }
}
