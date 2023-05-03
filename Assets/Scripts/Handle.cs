using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour, IInteractable
{

    private Animator animator;

    private bool isOpen = false;

    [SerializeField] private string openClipName;
    [SerializeField] private string closeClipName;

    string _type;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public string type
    {
        get => _type;
        set => _type = value;
    }

    void Start()
    {
        type = "grab";
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
