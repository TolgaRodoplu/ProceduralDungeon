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
 
        if (isEnterance) 
        {

            if(playerController != null) 
            {

                playerController.Deactivate();
                playerController.playerCamera.rotation = Quaternion.Euler(-90, 0, 0);
                Time.timeScale = 0.2f;
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
                    EventSystem.instance.TriggerSound("ForestBackground");
                    EventSystem.instance.StopSound("DungeonBackground");

                    wonderlandLight.gameObject.SetActive(true);
                }

                isUsed = true;
                other.gameObject.transform.position = teleportPos.position;
            }

            else
            {
                if (wonderlandLight.gameObject.activeInHierarchy)
                {
                    EventSystem.instance.TriggerSound("DungeonBackground");
                    EventSystem.instance.StopSound("ForestBackground");
                    wonderlandLight.gameObject.SetActive(false);
                }

                isUsed = false;
                other.gameObject.transform.position = teleportPosSecond.position;
            }
        }
    }
}
