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

    private void Update()
    {
        print(Vector3.Angle(transform.forward, (transform.position - doorway01Collider.transform.position)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == doorway01Collider)
        {
            if (Vector3.Angle(transform.forward, (transform.position - focalPoint.position)) > Camera.main.fieldOfView)
            {
                foreach (GameObject g in roomGeo)
                {
                    g.GetComponent<Collider>().enabled = true;
                    g.GetComponent<Renderer>().material = regularGeometryMaterial;
                }
            }
            else
            {
                foreach (GameObject g in roomGeo)
                {
                    g.GetComponent<Collider>().enabled = false;
                    g.GetComponent<Renderer>().material = hiddenGeometryMaterial;
                }
            }
        }
    }
}
