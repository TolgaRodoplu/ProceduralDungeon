using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRadius : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Grabbable interactObject = other.GetComponent<Grabbable>();

        if(interactObject != null)
        {
            interactObject.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Grabbable interactObject = other.GetComponent<Grabbable>();

        if (interactObject != null)
        {
            interactObject.enabled = false;
        }
    }
}
