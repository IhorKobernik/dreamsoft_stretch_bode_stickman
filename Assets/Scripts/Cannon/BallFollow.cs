using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollow : MonoBehaviour
{
  public GameObject objToFollow;
  public bool canFollow = false;
  private bool active;
  void Update()
  {
      if(canFollow && !active)
        this.transform.position = objToFollow.transform.position;
  }

  public void ActiveBullet(){
    active = true;
  }


  private void OnTriggerEnter(Collider other)
  {
      if(other.CompareTag("Box") && active)
      {
        other.GetComponent<OnDestroyObject>().psDestroyBox();
        Destroy(other.gameObject);
        Destroy(this.gameObject);
      }
  }
}
