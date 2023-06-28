using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private bool isEnterance = false;
    [SerializeField] private Transform teleportPos;
    [SerializeField] private Transform teleportPosSecond;
    [SerializeField] private Transform wonderlandLight;
    private bool isUsed = false;

    

    private void OnTriggerEnter(Collider other)
    {


        UI.instance.ToggleCanvas(!isEnterance);

        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController == null)
        {
            other.gameObject.transform.position = teleportPosSecond.position;
            return;
        }
            

        if (isEnterance) 
        {

            if(playerController != null) 
            {

                playerController.Deactivate();
                playerController.playerCamera.rotation = Quaternion.Euler(-90, 0, 0);
                
            }




            other.gameObject.transform.position = teleportPos.position;
        }

        else
        {

            Time.timeScale = 1f;
            playerController.playerCamera.rotation = Quaternion.Euler(0, 0, 0);
            playerController.Activate();

            if(!isUsed)
            {
                if(!wonderlandLight.gameObject.activeInHierarchy)
                {
                    FindObjectOfType<AudioManeger>().Play("ForestBackground");
                    FindObjectOfType<AudioManeger>().Stop("DungeonBackground");

                    wonderlandLight.gameObject.SetActive(true);
                }

                isUsed = true;
                other.gameObject.transform.position = teleportPos.position;
            }

            else
            {
                if (wonderlandLight.gameObject.activeInHierarchy)
                {
                    FindObjectOfType<AudioManeger>().Play("DungeonBackground");
                    FindObjectOfType<AudioManeger>().Stop("ForestBackground");
                    wonderlandLight.gameObject.SetActive(false);
                }

                isUsed = false;
                other.gameObject.transform.position = teleportPosSecond.position;
            }
        }
    }
}
