using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this enum is to check whether the player is supposed to be looking at the object, away from it, etc. to activate it
enum PlayerIs { lookingAtObject, lookingAwayFromObject, hasGlimpsedObject }

// PURPOSE: this script is for objects which make weird things happen to the player
// SUBSCRIBER SCRIPT(S): P_EventTriggers

public class FocalPoint : MonoBehaviour
{
    public delegate void FocalPointEventTriggered(string nameOfFocalPoint, bool shouldTeleport, bool shouldSetLookDirection, Transform destination);
    public static event FocalPointEventTriggered OnFocalPointEventTriggered;

    [SerializeField] float range = 10;
    [SerializeField] PlayerIs activationCriteriaForPlayer;
    [SerializeField] bool isTeleporter, overwritePlayerCameraAngles;

    [Tooltip("this field only matters if the isTeleporter bool is checked")]
    [SerializeField] Transform teleportDestination;

    Transform player;
    bool playerIsClose, playerIsLookingInTheRightDirection, activated;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        playerIsClose = Vector3.Distance(transform.position, player.position) < range;

        switch (activationCriteriaForPlayer)
        {
            case PlayerIs.lookingAtObject:
                playerIsLookingInTheRightDirection =
                    Vector3.Angle(transform.forward, (transform.position - player.position)) > Camera.main.fieldOfView;
                break;
            case PlayerIs.lookingAwayFromObject:
                playerIsLookingInTheRightDirection =
                    Vector3.Angle(transform.forward, (transform.position - player.position)) < Camera.main.fieldOfView;
                break;
            case PlayerIs.hasGlimpsedObject:
                // TODO
                break;
            default:
                break;
        }

        if (playerIsClose && playerIsLookingInTheRightDirection && !activated)
        {
            if (OnFocalPointEventTriggered != null)
                OnFocalPointEventTriggered(gameObject.name, isTeleporter, overwritePlayerCameraAngles, teleportDestination);

            activated = true;
        }
    }
}
