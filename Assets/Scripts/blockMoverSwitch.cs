using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMoverSwitch : MonoBehaviour
{
 public Rigidbody[] targetObjects;
 [SerializeField] GameObject laser;
 Animator anim;
 [SerializeField] Vector3 direction;
 // Start is called before the first frame update
 bool isOpen;
 void Start()
 {
  anim = GetComponent<Animator>();
  isOpen = false;
 }
 private void OnTriggerEnter(Collider other)
 {
  if (other.gameObject.CompareTag("Player") && !isOpen)
  {
   isOpen = true;
   AudioManager.instance.Switch();
   anim.SetTrigger("SwitchOn");
   foreach (Rigidbody rb in targetObjects)
   {
    rb.velocity = direction;
   }
  }
  if ((other.gameObject.CompareTag("Liftable")) && !isOpen)
  {
   isOpen = true;
   laser.SetActive(false);
  }
 }
}
