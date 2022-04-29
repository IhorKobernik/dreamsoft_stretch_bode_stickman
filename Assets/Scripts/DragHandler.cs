using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
 bool issnapped;
 //[HideInInspector]
 public bool isHitWall = true;
 Vector3 globalPositionOfContact;
 public Transform target;
 public Transform startPosition = null;
 bool mouseUp;
 public GameObject headPosition;
 Vector3 previousPosition;
 Vector3 targetScale;
 bool isdrag;
 bool isAttached;
 GameObject coll = null;
 DragControl control;
 public bool canMove = true;
 [SerializeField] Obi.ObiRopePrefabPlugger obiRef;
 BallFollow ballRef;
 boxFollow boxRef;
 bool hasPickedBox = false;
 public void Init(DragControl control)
 {
  this.control = control;
 }
 private void Start()
 {
  targetScale = target.transform.localScale;
  if (startPosition != null)
    transform.position = startPosition.position;

 }
 public void OnMouseDown()
 {
  if (control != null)
  {
    if(!control.CheckListHit() && isHitWall){
        return;
      } //chỉ còn 1 point đính trên tường
      
  }

  //When the player picks up one of the body parts
  if (!UIManager.instance.isPaused)
  {
   if (!LevelFinish.instance.isLevelFinished)
   {
      Debug.Log("Down");
      mouseUp = false;
      this.gameObject.GetComponent<HingeJoint>().connectedBody = target.GetComponent<Rigidbody>();
      issnapped = false;
      isHitWall = false;
      AudioManager.instance.Snap();
      target.transform.localScale += new Vector3(.1f, .1f, .1f);
   }
  }
 }

 public void OnMouseUp()
 {
    if (control != null)
    {
    if (!control.CheckListHit() && isHitWall) //chỉ còn 1 point đính trên tường
      return;
    }
    if (!UIManager.instance.isPaused)
    {
      isdrag = false;
      mouseUp = true;
      target.transform.localScale = targetScale;
      if(isHitWall){
        issnapped = true;
      } 
      else
      {
        previousPosition = headPosition.transform.position + new Vector3(0.1f,0.1f, 0);
      }
    }

    if (ballRef != null)
    {
      ballRef.canFollow = false;
      ballRef.objToFollow = null;
      ballRef.GetComponent<Rigidbody>().useGravity = true;
      ballRef.gameObject.GetComponent<SphereCollider>().enabled = true;
      ballRef = null;
    }

    if(boxRef != null)
    {
      boxRef.canFollow = false;
      boxRef = null;
      hasPickedBox = false;
    }
  }

 public void OnMouseDrag()
 {
  if (control != null)
  {
   if (!control.CheckListHit() && isHitWall) //chỉ còn 1 point đính trên tường
    return;
  }
  if (!UIManager.instance.isPaused)
  {
   if (!LevelFinish.instance.isLevelFinished && !issnapped)
   {
      isdrag = true;
   }
  }

 }
 private void FixedUpdate()
 {
  if (!UIManager.instance.isPaused)
  {

   {
    if(issnapped)
    {
      float distance = Vector3.Distance(target.transform.position, globalPositionOfContact);
      target.transform.position = Vector3.Slerp(target.transform.position, globalPositionOfContact, 1);
      if (distance <= .05f)
      {
        target.transform.position = globalPositionOfContact;
        previousPosition = globalPositionOfContact;
      }
    }
    else if(mouseUp)
    {
      float distance = Vector3.Distance(transform.position, previousPosition);
      transform.position = Vector3.MoveTowards(transform.position, previousPosition, 15 * Time.deltaTime);
      this.gameObject.GetComponent<HingeJoint>().connectedBody = null;
      if (distance <= .05f)
      {
        transform.position = previousPosition;
      }
    }

    if (!isHitWall && mouseUp && !isdrag)
    {
      transform.position = target.position;
    }

   }

   if (isdrag && isAttached)
   {
    if (coll != null)
    {
      coll.transform.position = this.transform.position;
    }
   }

   if (!isdrag && !isAttached)
   {
    if (coll != null)
    {
     coll.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
   }
  }
 }

 void Update()
 {
  if(isdrag)
  {
    Vector3 mousePos = Input.mousePosition;
    mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
    Vector3 delta = Camera.main.ScreenToWorldPoint(mousePos);
    Vector3 direction = delta - transform.position;
    float distance = Vector3.Distance(delta, transform.position);
    float speed = distance / 0.05f;
    if (direction.magnitude > .05f)
    {
      direction = direction.normalized * Time.deltaTime * speed;
      this.GetComponent<CharacterController>().Move(direction);
    }
  }

 }

 private void OnTriggerEnter(Collider other)
 {
   /////////////////
   if (other.CompareTag("Bullet") && mouseUp == false)
    {
      ballRef = other.GetComponent<BallFollow>();
      if (!ballRef.canFollow)
      {
        ballRef.objToFollow = this.gameObject;
        ballRef.canFollow = true;
        ballRef.gameObject.GetComponent<SphereCollider>().enabled = false;
      }
    }

    ////////////////////////
    if (other.CompareTag("PickableBox") && mouseUp == false && hasPickedBox == false)
    {
      boxRef = other.GetComponent<boxFollow>();
      if(!boxRef.canFollow)
      {
        boxRef.objToFollow = this.gameObject;
        boxRef.canFollow = true;
        hasPickedBox = true;
      }
    }

  /////////////////////////////
  if (canMove)
  {
   if (other.gameObject.CompareTag("Liftable"))
   {
    coll = other.gameObject;
    isAttached = true;
    if (isAttached)
    {
     other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
   }

   ///////////////////////
   if (other.gameObject.CompareTag("Walls") && !mouseUp)
   {
      isHitWall = true;
      globalPositionOfContact = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
      previousPosition = globalPositionOfContact;
      this.gameObject.GetComponent<HingeJoint>().connectedBody = target.GetComponent<Rigidbody>();
   }

   if (other.gameObject.CompareTag("Walls") && mouseUp)
   {
      Vector3 velo = target.GetComponent<Rigidbody>().velocity;
      velo.z = 0;
      target.GetComponent<Rigidbody>().velocity = -0.7f * velo;
   }

   ////////////////////////
   if (other.gameObject.CompareTag("Enemy"))
   {
      AudioManager.instance.Break();
      canMove = false;
      obiRef.die();
      UIManager.instance.OpenMenuLose();
      target.gameObject.SetActive(false);
      Destroy(this.gameObject);
   }
  }
 }

 private void OnTriggerStay(Collider other)
 {
    if(other.gameObject.CompareTag("Walls") && !mouseUp)
   {
      isHitWall = true;
      globalPositionOfContact = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
      previousPosition = globalPositionOfContact;
      this.gameObject.GetComponent<HingeJoint>().connectedBody = target.GetComponent<Rigidbody>();
   }
 }
 private void OnTriggerExit(Collider other)
 {
   if (other.CompareTag("Walls") && !mouseUp)
  {
    issnapped = false;
    isHitWall = false;
    this.gameObject.GetComponent<HingeJoint>().connectedBody = target.GetComponent<Rigidbody>();
  }

  if (other.CompareTag("Bullet") && mouseUp == false)
    {
      ballRef = other.GetComponent<BallFollow>();
      if (!ballRef.canFollow)
      {
        ballRef.objToFollow = this.gameObject;
        ballRef.canFollow = true;
        ballRef.gameObject.GetComponent<SphereCollider>().enabled = false;
      }
    }

  if (other.CompareTag("Liftable"))
  {

   isAttached = false;
   if (!isAttached)
   {
    other.GetComponent<Rigidbody>().isKinematic = false;
   }
  }
 }

}
