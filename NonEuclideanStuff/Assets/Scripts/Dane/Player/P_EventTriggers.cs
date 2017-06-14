using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PURPOSE: this script handles contextual events, and should be attached to the player

public class P_EventTriggers : MonoBehaviour
{
    [SerializeField] P_Controller playerController;
    [SerializeField] GameObject oldWestTown;

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

    private void ReactToFocalPointEvent(GameObject focalPoint, bool shouldTeleport, bool shouldSetLookDirection, Transform destination, float wait)
    {
        if (shouldTeleport && destination != null)
            StartCoroutine(Teleport(shouldSetLookDirection, destination, wait));

        switch (focalPoint.name.ToLower())
        {
            case "startingcorridor":
                playerController.ChangeMovementSpeed(playerController.OriginalSpeed);
                foreach (Renderer r in oldWestTown.GetComponentsInChildren<Renderer>())
                {
                    r.enabled = false;
                    r.gameObject.GetComponent<Collider>().enabled = false;
                }
                break;
            case "oldwesttownobject":
                focalPoint.GetComponent<Renderer>().enabled = true;
                focalPoint.GetComponent<Collider>().enabled = true;
                break;
            default:
                print("P_EventTriggers: no special behavior for " + focalPoint + " implemented");
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
