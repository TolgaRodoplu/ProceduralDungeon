using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
