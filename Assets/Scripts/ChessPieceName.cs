using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceName : Subtitle
{

    public override void Set()
    {
        text = gameObject.name;
    }

}
