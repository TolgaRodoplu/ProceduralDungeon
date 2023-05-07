using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Grabbable : MonoBehaviour, IInteractable
{
    private Transform grabAnchor;
    private bool grabbed = false;
    private float maxDistance = 1f;
    private float xRot;
    private Rigidbody rb;
    string _type;

    public string type
    {
        get => _type;
        set => _type = value;
    }

    void Awake()
    {
        this.enabled = false;
        type = "Pick Up";
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Interact(Transform interactor)
    {
        grabAnchor = interactor.Find("Main Camera").Find("GrabAnchor");

        if (grabAnchor == null)
            return;

        xRot = transform.rotation.eulerAngles.x;

        Debug.Log("grabbed");
        grabbed = true;
        rb.useGravity = false;
    }
    
    public void Update() 
    {
        if (grabbed)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Vector3.Distance(grabAnchor.transform.position, transform.position) > maxDistance)
            {
                Debug.Log("let Go");
                grabbed = false;
                rb.useGravity = true;
                grabAnchor = null;
            }
        }
    }

    private void FixedUpdate()
    {

        if (grabbed)
        {
            Vector3 DirectionToPoint = grabAnchor.position - transform.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            rb.velocity = DirectionToPoint * 100f * DistanceToPoint;

            transform.rotation = Quaternion.Euler(xRot,grabAnchor.rotation.eulerAngles.y, grabAnchor.rotation.eulerAngles.z);
            
            rb.angularVelocity = Vector3.zero;
        }

    }

    private void OnDestroy()
    {
        grabbed = false;
        rb.useGravity = true;
        grabAnchor = null;
    }

   
}