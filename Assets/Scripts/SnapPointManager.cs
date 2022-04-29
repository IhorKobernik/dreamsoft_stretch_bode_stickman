using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointManager : MonoBehaviour
{
    private static SnapPointManager instance;

    public static SnapPointManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    
}
