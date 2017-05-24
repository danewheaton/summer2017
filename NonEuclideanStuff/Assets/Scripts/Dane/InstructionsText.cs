using UnityEngine;
using System.Collections;

public class InstructionsText : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.parent.position);
    }
}
