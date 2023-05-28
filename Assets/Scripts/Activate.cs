using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    private bool isUsed = false;
    [SerializeField] private GameObject activate;
    [SerializeField] private GameObject deactivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isUsed)
        {
            activate.SetActive(true);
            deactivate.SetActive(false);
        }

    }
}
