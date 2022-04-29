using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
 public Transform targetObject;
 public int bodyPartsOnSwitch = 0;
 public Transform targetObject2;
 public bool isLever;
 Animator anim;
 bool istargetMoving;
 public float targetSpeed;
 public bool isDoor;
 public bool isMoveableObject;
 public bool isTopTrapActivator;
 public Rigidbody topTrap;
 // Start is called before the first frame update
 bool isOpen;
 void Start()
 {
  anim = GetComponent<Animator>();
  isOpen = false;
 }

 // Update is called once per frame
 void Update()
 {
  if (!isDoor && !isMoveableObject)
  {
   if (istargetMoving)
   {
    targetObject.transform.Translate(Vector3.left * targetSpeed * Time.deltaTime);
    StartCoroutine(StopMoving(5f));
   }
  }
  else
  {
   if (istargetMoving && !isMoveableObject)
   {
    targetObject.GetComponent<Animator>().SetTrigger("Open");
    StartCoroutine(StopMoving(5f));
   }
  }
 }
 private void OnTriggerEnter(Collider other)
 {
  if (other.gameObject.CompareTag("Player"))
   bodyPartsOnSwitch++;
  if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Liftable")) && !isOpen)
  {
   AudioManager.instance.Switch();
   anim.SetTrigger("SwitchOn");
   istargetMoving = true;
   isOpen = true;
   if (isMoveableObject)
   {
    targetObject.GetComponent<rotate>().goToPos2 = true;
    if (targetObject2 != null)
     targetObject2.GetComponent<rotate>().goToPos2 = true;
   }
   if (isTopTrapActivator)
    activateTopTrap();
  }
 }

 private void OnTriggerExit(Collider other)
 {
  if (other.gameObject.CompareTag("Player"))
   bodyPartsOnSwitch--;
  if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Liftable")) && isOpen && !isLever)
  {
   AudioManager.instance.Switch();
   anim.SetTrigger("SwitchOff");
   isOpen = false;
   if (isMoveableObject)
   {
    print(bodyPartsOnSwitch);
    if (bodyPartsOnSwitch == 1)
    {
     targetObject.GetComponent<rotate>().goToPos2 = false;
     if (targetObject2 != null)
      targetObject2.GetComponent<rotate>().goToPos2 = false;
    }
   }
  }
 }

 private void activateTopTrap()
 {
  topTrap.velocity = new Vector3(0f, -0.35f, 0f);
 }
 IEnumerator StopMoving(float time)
 {
  yield return new WaitForSeconds(time);
  istargetMoving = false;
 }
}
