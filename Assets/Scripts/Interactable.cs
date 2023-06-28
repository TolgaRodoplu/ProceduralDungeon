using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string soundEffect = null;
    public string cross = null;
    public virtual void Interact(Transform interactor)
    {
        
    }
}
