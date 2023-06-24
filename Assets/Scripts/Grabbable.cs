using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour, IInteractable
{
    private Transform grabAnchor;
    private bool grabbed = false;
    private float maxDistance = 1f;
    private float xRot;
    private Rigidbody rb;
    [SerializeField] private string soundEffect = null;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        //var outline = transform.AddComponent<Outline>();
        //outline.OutlineColor= Color.white;
        //outline.OutlineWidth = 4;
        //outline.OutlineMode = Outline.Mode.OutlineAll;
        //outline.enabled = false;
    }

    public void Interact(Transform interactor)
    {


        if (rb.isKinematic)
            rb.isKinematic = false;

        grabAnchor = interactor.Find("CamHolder").Find("Main Camera").Find("GrabAnchor");

        if (grabAnchor == null)
            return;

        xRot = transform.rotation.eulerAngles.x;

        grabbed = true;
        rb.useGravity = false;



    }

    public void Uninteract()
    {
        grabbed = false;
        rb.useGravity = true;
        grabAnchor = null;
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

    private void OnDestroy()
    {
        Uninteract();
    }

    private void OnDisable()
    {
        Uninteract();
    }


}