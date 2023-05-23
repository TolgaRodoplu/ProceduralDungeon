using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private bool isEnterance = false;
    [SerializeField] private Transform teleportPos;
    [SerializeField] private Transform teleportPosSecond;
    [SerializeField] private PlayerController playerController;
    private bool isUsed = false;

    

    private void OnTriggerEnter(Collider other)
    {
        if (isEnterance) 
        {
            if(playerController != null) 
            {
                playerController.active = false;
                Camera.main.transform.rotation = Quaternion.Euler(-90, 0, 0);

            }

            Time.timeScale = 0.2f;

            other.gameObject.transform.position = teleportPos.position;
        }

        else
        {

            Time.timeScale = 1f;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerController.active = true;

            if(!isUsed)
            {
                isUsed = true;
                other.gameObject.transform.position = teleportPos.position;
            }

            else
            {
                isUsed = false;
                other.gameObject.transform.position = teleportPosSecond.position;
            }
        }
    }
}
