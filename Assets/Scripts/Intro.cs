using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
   

    public void StartStartIntro()
    {
        StartCoroutine(StartIntro());
    }

    private IEnumerator StartIntro()
    {
        EventSystem.instance.StopSound("DungeonBackground");
        EventSystem.instance.TriggerSound("Intro");
        
        yield return new WaitForSeconds(9.5f);

        Text1.SetActive(true);

        yield return new WaitForSeconds(9.5f);

        Text1.SetActive(false);
        Text2.SetActive(true);


        yield return new WaitForSeconds(8f);

        EventSystem.instance.gameStarted -= StartStartIntro;
        EventSystem.instance.StartPlay();
        Destroy(this.gameObject);

    }
}
