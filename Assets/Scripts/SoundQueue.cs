using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundQueue : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EventSystem.instance.TriggerSound("AfterLybrinth");
            //EventSystem.instance.StopSound("DungeonBackground");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //EventSystem.instance.TriggerSound("DungeonBackground");
            EventSystem.instance.StopSound("AfterLybrinth");
        }
    }
}
