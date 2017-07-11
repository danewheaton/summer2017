using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this enum is to check whether the player is supposed to be looking at the object, away from it, etc. to activate it
enum PlayerIs { lookingAtObject, lookingAwayFromObject, hasGlimpsedObject }

// PURPOSE: this script is for objects which make weird things happen to the player

public class FocalPoint : MonoBehaviour, IPlayTricksOnThePlayer
{
    [SerializeField] float range = 10, visibilityAngle = 60, waitTime;
    [SerializeField] PlayerIs activationCriteriaForPlayer;
    [SerializeField] bool isTeleporter, overwritePlayerCameraAngles;

    [Tooltip("this field only matters if the isTeleporter bool is checked")]
    [SerializeField] Transform teleportDestination;

    [Tooltip("these are debug tools to check values in Update")]
    [SerializeField] bool printDistance, printAngle;

    protected Transform player;
    float distance, angle;
    bool playerIsClose, playerIsLookingInTheRightDirection, activated;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range / 2);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        angle = Vector3.Angle(player.forward, (transform.position - player.position));

        playerIsClose = distance < range;

        switch (activationCriteriaForPlayer)
        {
            case PlayerIs.lookingAtObject:
                playerIsLookingInTheRightDirection = angle < visibilityAngle;
                break;
            case PlayerIs.lookingAwayFromObject:
                playerIsLookingInTheRightDirection = angle > visibilityAngle;
                break;
            case PlayerIs.hasGlimpsedObject:
                // TODO: player sees object, then looks away
                break;
            default:
                break;
        }

        if (playerIsClose && playerIsLookingInTheRightDirection && !activated)
        {
            OnProximityEventTriggered();
            activated = true;
        }

        if (printDistance) print(distance);
        if (printAngle) print(angle);
    }

    public virtual void OnTriggerEvent()
    {
        // see child class overrides for specific behaviors
    }

    protected virtual void OnProximityEventTriggered()
    {
        if (isTeleporter && teleportDestination != null)
            StartCoroutine(Teleport());

        // see child class overrides for specific behaviors
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(waitTime);

        player.position = teleportDestination.position;

        if (overwritePlayerCameraAngles)
        {
            player.eulerAngles = new Vector3(player.eulerAngles.x, teleportDestination.eulerAngles.y, player.eulerAngles.z);
            Camera.main.transform.eulerAngles = new Vector3(teleportDestination.eulerAngles.x, player.eulerAngles.y, player.eulerAngles.z);
        }
    }
}
