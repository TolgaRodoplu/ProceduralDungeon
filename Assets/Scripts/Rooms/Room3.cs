using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3 : Room
{
    private void Awake()
    {

        shape = new int[,] { {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                             {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                             {2, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                             {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                             {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};
    }
}
