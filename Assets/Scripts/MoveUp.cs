using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 5f;

    bool isdirchange;
    // Update is called once per frame
    void Update()
    {
        if (!LevelFinish.instance.isLevelFinished && !isdirchange)
        {
            if (!isdirchange)
                transform.Translate(direction * speed * Time.deltaTime);
            else
                transform.Translate(-direction * speed * Time.deltaTime);
        }
        else
        {
            return;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("LevelEnd"))
        {
            isdirchange = true;
        }
    }

}
