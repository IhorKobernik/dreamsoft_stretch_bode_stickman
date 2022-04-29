using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 AudioSource source;
 public AudioClip clip;
 int level;
 private void Start()
 {
  level = PlayerPrefs.GetInt("Level");
  if (level == 0)
  {
   level = 1;
  }
  source = GetComponent<AudioSource>();
 }
 public void LevelToLoad()
 {
  source.PlayOneShot(clip);
  StartCoroutine(WaitTime(.5f));

 }
 IEnumerator WaitTime(float time)
 {
  yield return new WaitForSeconds(time);
  if (level >= 20)
  {
    SceneManager.LoadScene(Random.Range(0, SceneManager.sceneCountInBuildSettings));
  }
  else
  {
    SceneManager.LoadScene(level);
  }
 }

 public void PP()
 {
  Application.OpenURL("https://seepeegames.blogspot.com/2020/01/privacy-policy-this-privacy-policy.html#more");
 }
}
