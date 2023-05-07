using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;

    public event EventHandler<int> puzzleTriggered;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void TriggerPuzzle(int puzzleID)
    {
        puzzleTriggered?.Invoke(this, puzzleID);
    }
}
