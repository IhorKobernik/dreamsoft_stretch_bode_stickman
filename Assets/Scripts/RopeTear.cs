using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class RopeTear : MonoBehaviour
{
    public ObiRope[] Ropes;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            foreach (ObiRope r in Ropes)
            {
                r.GetComponent<ObiRope>().tearResistanceMultiplier = 0;
            }
        }
    }
}
