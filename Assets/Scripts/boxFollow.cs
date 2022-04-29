using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxFollow : MonoBehaviour
{
  public GameObject objToFollow;
  public bool canFollow = false;
  public GameObject psDestroy;

  void Update()
  {
    if(canFollow){
      this.transform.position = objToFollow.transform.position;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
      if (other.gameObject.CompareTag("Enemy"))
      {
        Instantiate(psDestroy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
      }
  }
}
