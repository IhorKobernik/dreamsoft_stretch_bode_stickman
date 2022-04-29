using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
 public static FaceChanger instance;
 public Material NormalF, HappyF, UnhappyF, DeadFace, LevelFinishF;
 public GameObject Arm1, Arm2;
 public GameObject Leg1, Leg2;
 public GameObject Head;
 public GameObject Hip;
 public float threashold1;
 public float threashold2;
 public SpriteRenderer playerLip;
 [SerializeField] Animator anim;
 bool isUpset;
 [SerializeField] Material upsetMat;
 [SerializeField] Gradient colors;
 float distance;
 [SerializeField]
 GameObject rEye;
 [SerializeField]
 GameObject lEye;
 float maxDist;
 int maxDistLimb;
 Vector3 target;
 bool left = false;
 bool up = false;
 int lookPos;

 void Start()
 {
  instance = this;
 }

 void Update()
 {
   if(LevelFinish.instance.isLevelFinished) return;
  if (target != Vector3.zero)
  {
   if (target.x < 0.5f)
    left = true;
   else left = false;
   if (target.y > 0)
    up = true;
   else up = false;

   if (up)
   {
    if (left)
     lookPos = 1;
    else lookPos = 2;
   }
   else if (left)
    lookPos = 3;
   else lookPos = 4;
   anim.SetBool("UL", false);
   anim.SetBool("UR", false);
   anim.SetBool("BL", false);
   anim.SetBool("BR", false);
   anim.SetBool("upset", false);
   switch (lookPos)
   {
    case 1:
     {
      anim.SetBool("UL", true);
      break;
     }
    case 2:
     {
      anim.SetBool("UR", true);
      break;
     }
    case 3:
     {
      anim.SetBool("BL", true);
      break;
     }
    case 4:
     {
      anim.SetBool("BR", true);
      break;
     }
   }
  }
  // Vector3 vt1 = Arm1.transform.position - Hip.transform.position;
  // Vector3 vt2 = Arm2.transform.position - Hip.transform.position;
  // float angle = Vector3.Angle(vt1, vt2);
  // float angle2 = Vector3.Angle(vt1, Vector3.up);
  // Head.transform.localEulerAngles = new Vector3(0, 0, -angle2 + angle / 2);
 }

 Color ColorFromGradient(float value)
 {
  return colors.Evaluate(value);
 }

 private void FixedUpdate()
 {
   if(LevelFinish.instance.isLevelFinished) return;
  float arm1Dist = Vector3.Distance(Arm1.transform.position, Head.transform.position);
  float arm2Dist = Vector3.Distance(Arm2.transform.position, Head.transform.position);
  float Leg1Dist = Vector3.Distance(Leg1.transform.position, Hip.transform.position);
  float Leg2Dist = Vector3.Distance(Leg2.transform.position, Hip.transform.position);

  isUpset = false;

  if (arm2Dist > arm1Dist)
  {
   if (arm2Dist >= threashold1)
   {
    isUpset = true;
    distance = arm2Dist;
    if (distance > maxDist)
    {
     maxDist = distance;
     maxDistLimb = 2;
    }
   }


  }
  else
  {
   if (arm1Dist >= threashold1)
   {
    isUpset = true;
    distance = arm1Dist;
    if (distance > maxDist)
    {
     maxDist = distance;
     maxDistLimb = 1;
    }
   }

  }
  if (Leg2Dist > Leg1Dist)
  {
   if (Leg2Dist >= threashold1)
   {
    isUpset = true;
    distance = Leg2Dist;
    if (distance > maxDist)
    {
     maxDist = distance;
     maxDistLimb = 4;
    }
   }
  }
  else
  {
   if (Leg1Dist >= threashold1)
   {
    isUpset = true;
    distance = Leg1Dist;
    if (distance > maxDist)
    {
     maxDist = distance;
     maxDistLimb = 3;
    }
   }
  }

  upsetMat.color = ColorFromGradient((distance - threashold1) / (threashold2 - threashold1));

  if (!isUpset)
  {
   anim.SetFloat("panicAnimSpeed", 1f);
   maxDist = 0;
   target = Vector3.zero;
   lEye.transform.localRotation = Quaternion.Euler(70f, 0f, 0f);
   rEye.transform.localRotation = Quaternion.Euler(70f, 0f, 0f);
   anim.SetBool("UL", false);
   anim.SetBool("UR", false);
   anim.SetBool("BL", false);
   anim.SetBool("BR", false);
   anim.SetBool("veryUpset", false);
   anim.SetBool("normal", true);
  }
  else
  {
   anim.SetBool("normal", false);
   anim.SetFloat("panicAnimSpeed", distance);
   switch (maxDistLimb)
   {
    case 1:
     target = Arm1.transform.localPosition;
     break;
    case 2:
     target = Arm2.transform.localPosition;
     break;
    case 3:
     target = Leg1.transform.localPosition;
     break;
    case 4:
     target = Leg2.transform.localPosition;
     break;
   }
   if (distance <= threashold2)
   {
    anim.SetBool("veryUpset", false);
   }
   else if (distance > threashold2)
   {
    anim.SetBool("UL", false);
    anim.SetBool("UR", false);
    anim.SetBool("BL", false);
    anim.SetBool("BR", false);
    anim.SetBool("veryUpset", true);
    target = Vector3.zero;
   }

  }
 }

 public void PlayerWin()
 {  
    anim.SetBool("normal", false);
    anim.Play("Happy", -1, 0);
 }

}
