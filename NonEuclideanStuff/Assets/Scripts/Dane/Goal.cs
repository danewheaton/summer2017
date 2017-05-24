using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    [SerializeField]
    Collider ball;

    [SerializeField]
    Light goalLight;

    [SerializeField]
    Renderer[] goalRenderers;

    private void OnTriggerEnter(Collider other)
    {
        if (other == ball)
        {
            goalLight.color = Color.blue;
            //foreach (Renderer r in goalRenderers) r.material.color = Color.yellow;
        }
    }
}
