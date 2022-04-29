using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyableBox : MonoBehaviour
{
  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Enemy")){
      if(this.gameObject.CompareTag("Box"))
        this.gameObject.GetComponent<OnDestroyObject>().psDestroyBox();
      Destroy(this.gameObject);
    }
  }

  void OnTriggerEnter(Collider other)
  {
      if(other.CompareTag("Enemy")){
        if(this.gameObject.CompareTag("Box"))
          this.gameObject.GetComponent<OnDestroyObject>().psDestroyBox();
        Destroy(this.gameObject);
      }
  }
}
