using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideWell : FocalPoint
{
    [SerializeField] GameObject oldWestTown;

    protected override void OnProximityEventTriggered()
    {
        base.OnProximityEventTriggered();

        oldWestTown.SetActive(true);
    }
}
