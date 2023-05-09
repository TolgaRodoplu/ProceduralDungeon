using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] protected int puzzleID;
    [SerializeField] protected AnimateObject[] animations;
    [SerializeField] protected  int triggerCount;

    protected virtual void Start()
    {
        EventSystem.instance.puzzleTriggered += Trigger;
    }

    protected virtual void Trigger(object sender, int puzzleID)
    {
        if(puzzleID == this.puzzleID)
        {
            triggerCount--;

            if(triggerCount == 0)
            {
                AnimateObjects();
            }
        }
    }

    protected virtual void AnimateObjects()
    {
        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].Animate();
        }
    }
}
