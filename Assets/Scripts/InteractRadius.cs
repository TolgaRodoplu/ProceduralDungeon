using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRadius : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Grabbable interactObject = other.GetComponent<Grabbable>();

        Subtitle subtitle= other.GetComponent<Subtitle>();

        if(interactObject != null)
        {
            interactObject.enabled = true;
        }
        
        if(subtitle != null) 
        {
            subtitle.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Grabbable interactObject = other.GetComponent<Grabbable>();

        Subtitle subtitle = other.GetComponent<Subtitle>();

        if (interactObject != null)
        {
            interactObject.enabled = false;
        }

        if (subtitle != null)
        {
            subtitle.enabled = false;
        }

    }


}
