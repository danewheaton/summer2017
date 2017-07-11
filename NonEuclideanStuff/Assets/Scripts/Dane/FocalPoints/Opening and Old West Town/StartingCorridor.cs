using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCorridor : FocalPoint
{
    [SerializeField] GameObject oldWestTown;
    [SerializeField] P_Controller playerController;

    protected override void OnProximityEventTriggered()
    {
        base.OnProximityEventTriggered();

        playerController.ChangeMovementSpeed(playerController.OriginalSpeed);
        foreach (Renderer r in oldWestTown.GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
            r.gameObject.GetComponent<Collider>().enabled = false;
        }
        oldWestTown.SetActive(false);
    }
}
