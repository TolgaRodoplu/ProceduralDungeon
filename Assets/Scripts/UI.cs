using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject canvas;

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

    public bool SubtitleToggle(bool activate) 
    {
        subtitleText.gameObject.transform.parent.parent.gameObject.SetActive(activate);

        return canvas.active;
    }

    public void ToggleCanvas(bool activate)
    {
        canvas.SetActive(activate);
    }
}
