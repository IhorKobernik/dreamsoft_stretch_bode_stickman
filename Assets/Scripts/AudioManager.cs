using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
 public static AudioManager instance;
 AudioSource source;
 public AudioClip buttonClickSound;
 public AudioClip snapSound;
 public AudioClip victorySound;
 public AudioClip breakSound;
 public AudioClip switchSound;
 public AudioClip attachSound;
  public AudioClip cannon;
  public AudioClip playerWin;
  public AudioClip lose;
 private void Awake()
 {
  instance = this;
 }
 private void Start()
 {
  source = GetComponent<AudioSource>();
 }
 public void ButtonClick()
 {
  source.PlayOneShot(buttonClickSound);
 }
 public void Snap()
 {
  if (source.isPlaying)
  {
   return;
  }
  else
   source.PlayOneShot(snapSound);
 }
 public void Victory()
 {
  source.PlayOneShot(victorySound);
 }
 public void Break()
 {
  source.PlayOneShot(breakSound);
 }
 public void Switch()
 {
  source.PlayOneShot(switchSound);
 }
 public void Attach()
 {
  source.PlayOneShot(attachSound);
 }

 public void Cannon()
 {
  source.PlayOneShot(cannon);
 }

 public void PlayerWin()
 {
  source.PlayOneShot(playerWin);
 }

 public void PlayerLose()
 {
  source.PlayOneShot(lose);
 }
}
