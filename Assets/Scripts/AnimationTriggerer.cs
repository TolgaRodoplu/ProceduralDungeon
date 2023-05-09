using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerer : MonoBehaviour
{
    [SerializeField] private AnimateObject[] animations;

    private void Start()
    {
        EventSystem.instance.animationTriggered += TriggerAnim;
    }

    private void TriggerAnim(object sender, int[] triggerID)
    {
        for(int i = 0; i < triggerID.Length; i++) 
        {
            animations[triggerID[i]].Animate();
        }
    }
}
