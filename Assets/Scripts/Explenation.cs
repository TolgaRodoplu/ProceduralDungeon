using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explenation : Subtitle
{
    [SerializeField] private int index;
    public override void Set()
    {
        text = ReadFile.ReaLine("explenation", index - 1);
    }
}
