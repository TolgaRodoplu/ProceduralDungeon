using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : Room
{
    private void Awake()
    {
        shape = new int[,] { {1, 1, 0 },
                             {1, 1, 0 },
                             {1, 4, 2 }};
    }
}
