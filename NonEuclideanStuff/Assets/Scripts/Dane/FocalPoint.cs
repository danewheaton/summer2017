﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this enum is to check whether the player is supposed to be looking at the object, away from it, etc. to activate it
enum PlayerIs { lookingAtObject, lookingAwayFromObject, hasGlimpsedObject }

// PURPOSE: this script is for objects which make weird things happen to the player
// SUBSCRIBER SCRIPT(S): P_EventTriggers

public class FocalPoint : MonoBehaviour
{
    public delegate void FocalPointEventTriggered(GameObject focalPoint, bool shouldTeleport, bool shouldSetLookDirection, Transform destination, float wait);
    public static event FocalPointEventTriggered OnFocalPointEventTriggered;

    [SerializeField] float range = 10, visibilityAngle = 60, waitTime;
    [SerializeField] PlayerIs activationCriteriaForPlayer;
    [SerializeField] bool isTeleporter, overwritePlayerCameraAngles;

    [Tooltip("this field only matters if the isTeleporter bool is checked")]
    [SerializeField] Transform teleportDestination;

    [Tooltip("these are debug tools to check values in Update")]
    [SerializeField] bool printDistance, printAngle;

    Transform player;
    float distance, angle;
    bool playerIsClose, playerIsLookingInTheRightDirection, activated;

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
            // passes the name of the gameObject. TODO: I'm uncomfortable passing the name of the gameObject, because people might rename the object, but I am not sure what the best alternative is -DW
            if (OnFocalPointEventTriggered != null)
                OnFocalPointEventTriggered(gameObject, isTeleporter, overwritePlayerCameraAngles, teleportDestination, waitTime);

            activated = true;
        }

        if (printDistance) print(distance);
        if (printAngle) print(angle);
    }
}
