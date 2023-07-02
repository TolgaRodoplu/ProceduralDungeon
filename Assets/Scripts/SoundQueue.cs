using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundQueue : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AudioManeger>().Play("AfterLybrinth");
            FindObjectOfType<AudioManeger>().Stop("DungeonBackground");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManeger>().Stop("AfterLybrinth");
            FindObjectOfType<AudioManeger>().Play("DungeonBackground");
        }
    }
}
