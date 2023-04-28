using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private bool active = true;
    [SerializeField] private Transform playerCamera = null;
    private CharacterController controller = null;

    [Header("Restraints")]
    private bool canHeadBob = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode interractKey = KeyCode.E;


    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float sprintSpeed = 1.0f;
    [SerializeField] private float crouchSpeed = 1.0f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;

    [Header("Jumping Parameters")]
    [SerializeField] private float gravity = -13.0f;
    private float velocityY = 0.0f;

    [Header("Looking Parameters")]
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField][Range(1.0f, 2.0f)] private float interractDistance = 5f;
    private float xRotation = 0.0f;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Awake()
    {
        
        controller = GetComponent<CharacterController>();
        Activate();
    }

    void Update()
    {
        if (active)
        {
            InterractWithObject();
            MouseLook();
            Move();
        }


    }

    private void InterractWithObject()
    {
        var ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        Debug.DrawRay(playerCamera.position, playerCamera.transform.TransformDirection(playerCamera.forward));

        if (Physics.Raycast(ray, out hit, interractDistance, LayerMask.GetMask("Interact")))
        {
            Debug.Log(hit.transform.name);
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable != null)
            {

                if (Input.GetKeyDown(interractKey))
                {
                    interactable.Interact(this.transform);
                }

            }
        }
        
    }
    private void MouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        xRotation -= currentMouseDelta.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.localEulerAngles = Vector3.right * xRotation;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    

    private void Move()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }


        velocityY += gravity * Time.deltaTime;

        Vector3 dir = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(dir * Time.deltaTime);
    }

    
    private void Activate()
    {
        active = true;
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void Deactivate(object sender, Vector3 rgb)
    {
        active = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
