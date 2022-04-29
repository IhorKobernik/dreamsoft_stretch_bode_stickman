using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlatform : MonoBehaviour
{
 [SerializeField]
 GameObject extension;
 private void OnTriggerEnter(Collider other)
 {
  if (other.CompareTag("Liftable"))
  {
   extension.SetActive(false);
  }
 }
 private void OnTriggerExit(Collider other)
 {
  if (other.CompareTag("Liftable"))
  {
   extension.SetActive(true);
  }
 }
}
