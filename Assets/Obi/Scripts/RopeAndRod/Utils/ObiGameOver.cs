using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Obi
{
    public class ObiGameOver : MonoBehaviour
    {
        public GameObject objGameOver;
        bool isGameOverOpen;
        public AudioSource source;
        private void Start()
        {
            if(objGameOver==null){
                objGameOver = GameObject.Find("Canvas/UIManager/IngameMenu/GameOver").gameObject;
            }
        }

        public void CallGameOver(){
            Debug.LogError("Open GameOver");
            if(isGameOverOpen)
                return;
            if(objGameOver != null)
                StartCoroutine(GameOverOpen(1f));
        }
        IEnumerator GameOverOpen(float time)
        {
            yield return new WaitForSeconds(time);
            objGameOver.SetActive(true);
            isGameOverOpen = true;
        }
    }
}