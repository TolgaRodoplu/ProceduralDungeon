using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room0 : Room
{
    private void Awake()
    {
        shape = new int[,] { {1, 1, 1 },
                             {1, 1, 1 },
                             {1, 1, 1 },
                             {1, 1, 1 },
                             {0, -1, 0 }};
    }
}