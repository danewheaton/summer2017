using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] Transform teleportDestination1, teleportDestination2;

    private void OnTriggerEnter(Collider other)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        switch (GetComponentInParent<SwitchRenderTexture>().TargetTeleportLocation)
        {
            case TeleportLocation.LOCATION1:
                player.position = new Vector3(teleportDestination1.position.x, player.position.y, teleportDestination1.position.z);
                player.eulerAngles = new Vector3(player.eulerAngles.x, teleportDestination1.eulerAngles.y, player.eulerAngles.z);
                break;
            case TeleportLocation.LOCATION2:
                player.position = new Vector3(teleportDestination2.position.x, player.position.y, teleportDestination2.position.z);
                player.eulerAngles = new Vector3(player.eulerAngles.x, teleportDestination2.eulerAngles.y, player.eulerAngles.z);
                break;
        }
    }
}
