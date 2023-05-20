using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private bool isEnterance = false;
    [SerializeField] private Transform teleportPos;
    [SerializeField] private Transform teleportPosSecond;
    [SerializeField] private PlayerController playerController;

    

    private void OnTriggerEnter(Collider other)
    {
        if (isEnterance) 
        {
            if(playerController != null) 
            {
                playerController.active = false;
                Camera.main.transform.rotation = Quaternion.Euler(-90, 0, 0);
            }

            other.gameObject.transform.position = teleportPos.position;
        }

        else
        {
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerController.active = true;
            other.gameObject.transform.position = teleportPos.position;
        }
    }
}
