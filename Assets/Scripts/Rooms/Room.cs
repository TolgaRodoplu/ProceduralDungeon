using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector]public int[,] shape; //1 = Obstacle // 2-Door // 3-Hallway
    [HideInInspector]public int doorZ;
    [HideInInspector]public int doorX;
}
