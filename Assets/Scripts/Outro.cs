using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    public GameObject[] texts;


    public void Start()
    {
        StartCoroutine(StartOntro());
    }

    private IEnumerator StartOntro()
    {
        EventSystem.instance.TriggerSound("Outro");
        int i = 0;
        while(i < texts.Length - 1)
        {
            yield return new WaitForSeconds(7);

            
            texts[i].SetActive(false);
            i++;
            texts[i].SetActive(true);
            
            
        }

        yield return new WaitForSeconds(20f);

        SceneManager.LoadScene("Menu");

    }
}
