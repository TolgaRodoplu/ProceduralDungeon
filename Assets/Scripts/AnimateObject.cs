using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateObject : MonoBehaviour
{
    Animator animator;
    [SerializeField] private string soundEffect = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Animate()
    {
        if(soundEffect !=null) 
            FindObjectOfType<AudioManeger>().Play(soundEffect);

        animator.Play("Default", 0, 0.0f);
    }
}
