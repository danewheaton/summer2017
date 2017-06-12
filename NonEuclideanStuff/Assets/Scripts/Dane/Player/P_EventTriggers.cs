using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PURPOSE: this script handles contextual events, and should be attached to the player

public class P_EventTriggers : MonoBehaviour
{
    [SerializeField] P_Controller playerController;

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

    private void ReactToFocalPointEvent(string nameOfFocalPoint, bool shouldTeleport, bool shouldSetLookDirection, Transform destination, float wait)
    {
        if (shouldTeleport && destination != null)
            StartCoroutine(Teleport(shouldSetLookDirection, destination, wait));

        switch (nameOfFocalPoint)
        {
            case "startingcorridor":
                playerController.ChangeMovementSpeed(playerController.OriginalSpeed);
                break;
            default:
                print("P_EventTriggers: no special behavior for " + nameOfFocalPoint + " implemented");
                break;
        }
    }

    private IEnumerator Teleport(bool shouldSetLookDirection, Transform destination, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        transform.position = destination.position;

        if (shouldSetLookDirection)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, destination.eulerAngles.y, transform.eulerAngles.z);
            Camera.main.transform.eulerAngles = new Vector3(destination.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
