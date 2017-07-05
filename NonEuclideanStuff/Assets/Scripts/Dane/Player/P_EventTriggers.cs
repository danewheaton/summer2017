using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PURPOSE: this script handles contextual events, and should be attached to the player

public class P_EventTriggers : MonoBehaviour
{
    [SerializeField] P_Controller playerController;
    [SerializeField] GameObject oldWestTown;
    [SerializeField] Collider canyonTrigger, hiddenRoomTrigger;

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
        if (other == canyonTrigger)
        {
            transform.position += new Vector3(1000, 0, 0);
        }
        else if (other == hiddenRoomTrigger)
        {

        }
    }

    private void ReactToFocalPointEvent(GameObject focalPoint, bool shouldTeleport, bool shouldSetLookDirection, Transform destination, float wait)
    {
        if (shouldTeleport && destination != null)
            StartCoroutine(Teleport(shouldSetLookDirection, destination, wait));

        switch (focalPoint.name.ToLower())  // TODO: find a cleaner solution to identifying focal points than the name of the gameObject
        {
            case "startingcorridor":
                playerController.ChangeMovementSpeed(playerController.OriginalSpeed);
                foreach (Renderer r in oldWestTown.GetComponentsInChildren<Renderer>())
                {
                    r.enabled = false;
                    r.gameObject.GetComponent<Collider>().enabled = false;
                }
                oldWestTown.SetActive(false);
                break;
            case "insidewell":
                oldWestTown.SetActive(true);
                break;
            case "canyon":
            case "oldwesttownobject":   // this particular group of focal points was a test - the focalPoint script is not intended to be used on dozens of similar objects, as is the case for old west town objects
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
