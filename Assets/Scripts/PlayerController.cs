using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public bool active = false;
    public bool isInteracting = false;
    public Transform playerCamera = null;
    private CharacterController controller = null;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;
    [SerializeField] private float gravity = -100f;
    private float velocityY = 0.0f;

    [Header("Looking Parameters")]
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField][Range(0f, 5f)] private float interractDistance = 1000f;
    private float xRotation = 0.0f;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        this.transform.parent = null;
        EventSystem.instance.playStarted += Activate;
    }

    void Update()
    {
        if (active)
        {
            InterractWithObject();
            MouseLook();
        }


        Move();

        if (Input.GetKeyUp(KeyCode.Mouse0) && active)
            UI.instance.ToggleCanvas(true);

        if(Input.GetKeyDown(KeyCode.F1))
        {
            UI.instance.ReturnToMenu();
        }
    }

    private void InterractWithObject()
    {
        var ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Player");


        string crossName = "default";
        string subtitleText = null;
        bool subtitleStatus = false;

        if (Physics.Raycast(ray, out hit, interractDistance, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            //Debug.Log(hit.transform.name);
            Interactable interactable = hit.transform.GetComponent<Interactable>();

            Subtitle explenation = hit.transform.transform.GetComponent<Subtitle>();


            if (explenation != null)
            {
                subtitleStatus = true;
                subtitleText = explenation.text;
            }
            

            if (interactable != null)
            {
                crossName = interactable.cross;


                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    isInteracting = true;
                    UI.instance.ToggleCanvas(false);
                    interactable.Interact(this.transform);

                    
                }
            }
        }
        

        UI.instance.SubtitleToggle(subtitleStatus, subtitleText);
        UI.instance.changeCrosshair(crossName);
    }
    private void MouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        xRotation -= currentMouseDelta.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -50f, 60f);

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


    public void Activate()
    {

        UI.instance.ToggleCanvas(true);
        active = true;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void Deactivate()
    {
        UI.instance.ToggleCanvas(false);
        active = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}