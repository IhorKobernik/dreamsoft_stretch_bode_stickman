using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
  public static UIManager instance;
  public GameObject GameOver;
  public GameObject VictoryPanel;
  public bool isPaused;
  public Text txtLevel;
  private int currentLevel;
  public Button btnRePlay, btnSkip, btnSkipLose, btnNext, btnReplayLose;
  private void Awake()
  {
    instance = this;
  }
  private void Start()
  {
    btnRePlay.onClick.AddListener(Restart);
    btnSkip.onClick.AddListener(SkipLevel);
    btnSkipLose.onClick.AddListener(SkipLevel);
    btnNext.onClick.AddListener(NextLevel);
    btnReplayLose.onClick.AddListener(Restart);

    currentLevel = PlayerPrefs.GetInt("Level",1);
    GameOver.SetActive(false);
    VictoryPanel.SetActive(false);
    txtLevel.text = "Level " + currentLevel;
  }

  public void OpenMenuWin(){
    StartCoroutine(VictoryPanelOpen(1.5f));
  }
  IEnumerator VictoryPanelOpen(float time)
  {
    currentLevel ++;
    PlayerPrefs.SetInt("Level",currentLevel);
    yield return new WaitForSeconds(time);
    VictoryPanel.SetActive(true);
  }

  public void OpenMenuLose(){
    AudioManager.instance.PlayerLose();
    isPaused = true;
    GameOver.SetActive(true);
  }

  public void Restart()
  {
    AudioManager.instance.ButtonClick();
    StartCoroutine(WaitTimeLoadScene(.5f));
  }

  public void SkipLevel()
  {
    AudioManager.instance.ButtonClick();
    currentLevel ++;
    PlayerPrefs.SetInt("Level",currentLevel);
    StartCoroutine(WaitTimeLoadScene(.5f));
  }

  public void NextLevel()
  {
    AudioManager.instance.ButtonClick();
    StartCoroutine(WaitTimeLoadScene(.5f));
  }

  IEnumerator WaitTimeLoadScene(float time)
  {
    yield return new WaitForSeconds(time);
    SceneManager.LoadScene(currentLevel);
  }
  
}
