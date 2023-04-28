using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAnchor : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;

    private float zPositionOffset;

    private void Awake()
    {
        zPositionOffset = transform.localPosition.z - playerCamera.localPosition.z;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zPositionOffset);
    }
}
