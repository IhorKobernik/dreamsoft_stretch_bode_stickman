using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBase : MonoBehaviour
{
    [SerializeField] GameObject particles;
    [SerializeField] float launchSpeed;
    public Animator anim;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(Action(collision.transform));
        }
    }

    IEnumerator Action(Transform target){
        target.transform.GetComponent<BallFollow>().ActiveBullet();
        target.transform.GetComponent<SphereCollider>().enabled = true;
        transform.parent.GetComponent<MeshCollider>().enabled = false;
        anim.Play("Action",-1,0);
        yield return new WaitForSeconds(0.5f);
        target.transform.GetComponent<SphereCollider>().isTrigger = true;
        target.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * launchSpeed, ForceMode.Impulse);
        AudioManager.instance.Cannon();
        particles.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transform.parent.GetComponent<MeshCollider>().enabled = true;

    }
}
