using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private float amount = 0.002f;
    [SerializeField] private float frequency= 10.0f;
    [SerializeField] private float smooth = 10.0f;
    private Vector3 startPos;
    private float yValue;
    private bool stepped = false;

    private void Start()
    {
        startPos = transform.localPosition;  
    }

    private void Update()
    {
        
        CheckForHeadBob();
        StopHeadBob();

    }

    private void CheckForHeadBob()
    {
        float input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (input > 0f)
            StartHeadBob();
    }

    private Vector3 StartHeadBob()
    {
        Vector3 pos = Vector3.zero;

        if (yValue < Mathf.Sin(Time.time * frequency) && !stepped)
        {
            EventSystem.instance.TriggerSound("FootStep");
            stepped = true;
        }
        else if(yValue > Mathf.Sin(Time.time * frequency) && stepped)
            stepped = false;
            

        yValue = Mathf.Sin(Time.time * frequency);
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;
        return pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == startPos) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 2 *Time.deltaTime);
    }

}
