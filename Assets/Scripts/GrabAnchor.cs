using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAnchor : MonoBehaviour
{
    public bool isInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.GetMask("Interact")) 
        {
            isInside = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        isInside = false;
    }

}
