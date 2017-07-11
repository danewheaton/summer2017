using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canyon : FocalPoint
{
    protected override void OnProximityEventTriggered()
    {
        base.OnProximityEventTriggered();

        player.position += new Vector3(1000, 0, 0);
    }
}
