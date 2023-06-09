using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Subtitle
{
    private int bookCount = 96;
    [SerializeField] private bool isKey = false;
    public override void Set()
    {
        if(!isKey)
        {
            int index = UnityEngine.Random.Range(0, bookCount);
            text = ReadFile.ReaLine("Books", index);
        }
    }
}
