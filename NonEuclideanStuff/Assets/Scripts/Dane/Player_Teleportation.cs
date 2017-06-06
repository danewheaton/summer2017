using UnityEngine;
using System.Collections;

public class Player_Teleportation : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] float range = 10;

    bool inOriginalRoom;

    private void Update()
    {
        bool targetIsCloseAndInView =
            Vector3.Angle(transform.forward, (transform.position - targetObject.transform.position)) > Camera.main.fieldOfView &&
            Vector3.Distance(transform.position, targetObject.transform.position) < range;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inOriginalRoom = !inOriginalRoom;
            transform.position += new Vector3((inOriginalRoom ? 100 : -100), 0, 0);

            if (targetIsCloseAndInView)
            {
                targetObject.transform.position += new Vector3((inOriginalRoom ? 100 : -100), 0, 0);
            }
        }
    }
}
