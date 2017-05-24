using UnityEngine;
using System.Collections;

public class Player_SpatialTricks : MonoBehaviour
{
    [SerializeField]
    Collider doorway01Collider;

    [SerializeField]
    Transform focalPoint;

    [SerializeField]
    GameObject[] roomGeo;

    [SerializeField]
    Material regularGeometryMaterial, hiddenGeometryMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other == doorway01Collider)
        {
            // if you're looking into the middle of the room
            if (Vector3.Angle(transform.forward, (transform.position - focalPoint.position)) > Camera.main.fieldOfView)
            {
                // activate the collision and visibility of the room
                foreach (GameObject g in roomGeo)
                {
                    g.GetComponent<Collider>().enabled = true;
                    g.GetComponent<Renderer>().material = regularGeometryMaterial;
                }
            }

            // if you're not looking into the middle of the room
            else
            {
                // deactivate the collision and visibility of the room
                foreach (GameObject g in roomGeo)
                {
                    g.GetComponent<Collider>().enabled = false;
                    g.GetComponent<Renderer>().material = hiddenGeometryMaterial;
                }
            }
        }
    }
}
