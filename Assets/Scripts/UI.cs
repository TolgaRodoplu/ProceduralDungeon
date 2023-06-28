using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public GameObject canvas;

    public Image cross;

    public Sprite defaultCross, grabCross, handleCross;

    public static UI instance;

    [SerializeField] private TextMeshProUGUI subtitleText;

   

    private void Awake()
    {
        instance = this;
    }

    public void SetSubtitle(string subtitle)
    {
        subtitleText.text = subtitle;
    }

    public void SubtitleToggle(bool activate, string subtitle) 
    {
        SetSubtitle(subtitle);
        subtitleText.gameObject.transform.parent.parent.gameObject.SetActive(activate);
    }

    public void ToggleCanvas(bool activate)
    {
        canvas.SetActive(activate);
    }

    public void ButtonHoverSound()
    {
        FindObjectOfType<AudioManeger>().Play("ExplenationClip");
    }

    public void NewGameButtonPressed()
    {
        FindObjectOfType<AudioManeger>().Play("PaperRip");
        SceneManager.LoadScene("Test");
    }
    public void QuitButtonPressed()
    {
        FindObjectOfType<AudioManeger>().Play("PaperRip");
        Application.Quit();
    }

    public void ReturnToMenu()
    {

        SceneManager.LoadScene("Menu");
    }

    public void changeCrosshair(string newCross)
    {
        if (newCross != null)
        {
            if (newCross == "default")
            {
                cross.sprite = defaultCross;
                cross.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 0.1f;
            }
            else
            {
                cross.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 0.7f;
                if (newCross == "grab")
                {
                    cross.sprite = grabCross;
                }
                else if (newCross == "handle")
                {
                    cross.sprite = handleCross;
                }
            }

        }
        
    }

}
