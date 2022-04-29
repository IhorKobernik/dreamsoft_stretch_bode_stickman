using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combiationLock : MonoBehaviour
{
 public GameObject[] locks;
 [SerializeField] Vector3 pos1;
 [SerializeField] Vector3 pos2;
 [SerializeField] float speed;
 [SerializeField] GameObject targetObj;
 bool goToPos2 = false;
 public void checkForLock()
 {
  bool canOpen = true;
  foreach (GameObject go in locks)
   if (!go.CompareTag("Unlocked"))
    canOpen = false;
  if (canOpen)
   goToPos2 = true;
  else goToPos2 = false;
 }

 void goToSecondPos()
 {
  this.transform.position = Vector3.MoveTowards(this.transform.position, pos2, Time.deltaTime * speed);
 }

 void goToFirstPos()
 {
  this.transform.position = Vector3.MoveTowards(this.transform.position, pos1, Time.deltaTime * speed);
 }

 void Update()
 {
  if (goToPos2)
   goToSecondPos();
  else goToFirstPos();
  if (targetObj != null && this.transform.position.y <= targetObj.transform.position.y + 0.65f)
   Destroy(targetObj);
 }
}
