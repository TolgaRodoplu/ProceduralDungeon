using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4 : Room
{
    private void Awake()
    {

        shape = new int[,] { {1},
                             {4},
                             {2}};
    }
}
