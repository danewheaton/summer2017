using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PURPOSE: this script handles contextual trigger events

public class P_EventTriggers : MonoBehaviour
{
    private void OnEnable()
    {
        FocalPoint.OnFocalPointEventTriggered += ReactToFocalPointEvent;
    }
    private void OnDisable()
    {
        FocalPoint.OnFocalPointEventTriggered -= ReactToFocalPointEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void ReactToFocalPointEvent(string nameOfFocalPoint, bool shouldTeleport, bool shouldSetLookDirection, Transform destination)
    {
        if (shouldTeleport && destination != null)
        {
            transform.position = destination.position;
            if (shouldSetLookDirection)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, destination.eulerAngles.y, transform.eulerAngles.z);
                Camera.main.transform.eulerAngles = new Vector3(destination.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }

        switch (nameOfFocalPoint)
        {
            default:
                print("P_EventTriggers: no special behavior for " + nameOfFocalPoint + " implemented");
                break;
        }
    }
}
