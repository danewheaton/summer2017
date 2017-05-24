using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    [SerializeField]
    Collider ball;

    [SerializeField]
    Light goalLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other == ball) goalLight.color = Color.blue;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == ball) goalLight.color = Color.red;
    }
}
