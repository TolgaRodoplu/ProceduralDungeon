using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : Interactable
{
    private Transform grabAnchor;
    private bool grabbed = false;
    private float maxDistance = 1f;
    private float xRot;
    private Rigidbody rb;
    



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        if(GetComponent<Key>() == null )
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        cross = "grab";
        
    }

    public override void Interact(Transform interactor)
    {
        grabAnchor = interactor.Find("CamHolder").Find("Main Camera").Find("GrabAnchor");

        if (grabAnchor == null)
            return;


        if(rb.isKinematic)
            rb.isKinematic = false;

        xRot = transform.rotation.eulerAngles.x;

        grabbed = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;


    }

    public void Uninteract()
    {
        grabbed = false;
        rb.useGravity = true;
        grabAnchor = null;
        if (GetComponent<Key>() != null)
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

    }

    public void Update()
    {
        if (grabbed)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Vector3.Distance(grabAnchor.transform.position, transform.position) > maxDistance)
            {
                Uninteract();
            }
        }
    }

    private void FixedUpdate()
    {
        if (grabbed)
        {
            Vector3 DirectionToPoint = grabAnchor.position - transform.position;

            float DistanceToPoint = DirectionToPoint.magnitude;

            rb.velocity = DirectionToPoint * 50f * DistanceToPoint;

            transform.rotation = Quaternion.Euler(xRot, grabAnchor.rotation.eulerAngles.y, grabAnchor.rotation.eulerAngles.z);

            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnDisable()
    {
        Uninteract();
    }

   

    

}