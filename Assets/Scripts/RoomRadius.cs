using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRadius : MonoBehaviour
{
    Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        
    }
}
