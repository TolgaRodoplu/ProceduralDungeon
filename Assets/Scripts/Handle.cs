using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Handle : Interactable
{

    private Animator animator;

    private bool isOpen = false;

    [SerializeField] private string openClipName;
    [SerializeField] private string closeClipName;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cross = "handle";
    }


    public override void Interact(Transform interactor)
    {
        if (!AnimatorIsPlaying())
        {
            if (!isOpen)
            {
                animator.Play(openClipName, 0, 0.0f);
                isOpen = true;
            }
            else
            {
                animator.Play(closeClipName, 0, 0.0f);
                isOpen = false;
            }



            FindObjectOfType<AudioManeger>().Play("DoorClip");
        }
        
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(AnimatorIsPlaying() && collision.collider.tag == "Player") 
        {
            animator.speed = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (AnimatorIsPlaying() && collision.collider.tag == "Player")
        {
            animator.speed = 1f;
        }
    }

}
