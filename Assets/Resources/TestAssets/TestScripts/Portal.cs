using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer renderObject;
    Camera playerCamera;
    Camera portalCamera;

    
    private void Start()
    {

        playerCamera = Camera.main;
        portalCamera = GetComponentInChildren<Camera>();


        if (portalCamera.targetTexture != null)
        {
            portalCamera.targetTexture.Release();
        }
        portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 0);

        //renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        //portalCamera.targetTexture = renderTexture;
        linkedPortal.renderObject.material.SetTexture("_MainTex", portalCamera.targetTexture);

        //linkedPortal.renderObject.material.mainTexture = this.portalCamera.targetTexture;
        
    }

    private void LateUpdate()
    {
        MoveRotatePortalCamera();
        RenderView();
    }

    void MoveRotatePortalCamera()
    {
        //Vector3 playerOffsetFromPortal = playerCamera.transform.position - linkedPortal.transform.position;
        //portalCamera.transform.position = this.transform.position + playerOffsetFromPortal;

        //float angularDifferenceBetweenPortalPositions = Quaternion.Angle(this.transform.rotation, linkedPortal.transform.rotation);

        //Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalPositions, Vector3.up);
        //Vector3 newCameraDirection = portalRotationalDifference * playerCamera.transform.forward;
        //portalCamera.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

        Vector3 relativePosition = linkedPortal.transform.InverseTransformPoint(playerCamera.transform.position);
        Quaternion relativeRotation = Quaternion.Inverse(linkedPortal.transform.rotation) * playerCamera.transform.rotation;

        portalCamera.transform.position = this.transform.TransformPoint(relativePosition);
        portalCamera.transform.rotation = this.transform.rotation * relativeRotation;

    }

    void RenderView()
    {
        
    }

   
}
