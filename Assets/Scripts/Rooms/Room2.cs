using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : Room
{
    private void Awake()
    {
        shape = new int[,] { {0, 0, 2, 0, 0},
                             {1, 1, 4, 1, 1},
                             {1, 1, 1, 1, 1},
                             {1, 1, 1, 1, 1},
                             {1, 1, 1, 1, 1}};
    }
}
