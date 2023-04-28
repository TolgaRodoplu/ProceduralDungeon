using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public string type {get; set;}
    public void Interact(Transform interactor);
}
