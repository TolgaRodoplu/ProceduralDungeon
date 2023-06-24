using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Handle : MonoBehaviour, IInteractable
{

    private Animator animator;

    private bool isOpen = false;

    [SerializeField] private string openClipName;
    [SerializeField] private string closeClipName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //var outline = transform.AddComponent<Outline>();
        //outline.OutlineColor = Color.white;
        //outline.OutlineWidth = 7;
        //outline.OutlineMode = Outline.Mode.OutlineAll;
        //outline.enabled = false;
    }


    public void Interact(Transform interactor)
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


            EventSystem.instance.TriggerSound("DoorClip");
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
