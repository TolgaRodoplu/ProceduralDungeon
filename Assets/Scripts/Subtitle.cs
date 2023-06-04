using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subtitle : MonoBehaviour
{
    public string text = "***";

    public abstract void Set();

    void Start()
    {
        Set();
    }


    
}
