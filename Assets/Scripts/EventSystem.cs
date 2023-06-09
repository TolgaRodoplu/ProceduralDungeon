using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;
    public event EventHandler<string> soundTriggered, soundStopped;
    public event EventHandler<int> puzzleTriggered;
    public event EventHandler<int[]> animationTriggered;
    public event Action gameStarted, playStarted;
    void Awake()
    {

        if (instance == null)
            instance = this;
        
    }

    private void Start()
    {
        Debug.Log("sa");
    }

    

    public void StartPlay()
    {
        FindObjectOfType<AudioManeger>().Play("DungeonBackground");
        playStarted?.Invoke();
    }

    public void TriggerPuzzle(int puzzleID)
    {
        puzzleTriggered?.Invoke(this, puzzleID);
    }
    
    public void TriggerAnimation(int[] animationID)
    {
        animationTriggered?.Invoke(this, animationID);
    }

    
}
