using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWestTownObject : FocalPoint
{
    protected override void OnProximityEventTriggered()
    {
        base.OnProximityEventTriggered();

        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
