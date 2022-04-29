using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
 public static LevelFinish instance;

 public bool isCompleted = false;
 public bool isLevelFinished = false;
 bool isAudio;
 public ParticleSystem psWin;
 private void Awake()
 {
  instance = this;
 }

 private void Start()
 {
    psWin = GameObject.Find("LevelFinishTarget/Particle System").GetComponent<ParticleSystem>();
 }

 private void OnTriggerEnter(Collider other)
 {
  if(other.gameObject.CompareTag("LevelEnd"))
  {
    if(isCompleted) return;
    isCompleted = true;
    isLevelFinished = true;
    transform.parent.parent.parent.GetComponent<Rigidbody>().isKinematic = true;
    psWin.Play();
    FaceChanger.instance.PlayerWin();
    AudioManager.instance.PlayerWin();
    StartCoroutine(Wait(.5f));
    Debug.Log("LevelFinished");
    UIManager.instance.OpenMenuWin();
  }
 }
 IEnumerator Wait(float time)
 {
    yield return new WaitForSeconds(time);
    isLevelFinished = true;
 }
}

