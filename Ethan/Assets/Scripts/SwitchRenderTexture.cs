using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeleportLocation { LOCATION1, LOCATION2 }

public class SwitchRenderTexture : MonoBehaviour
{
    TeleportLocation targetTeleportLocation = TeleportLocation.LOCATION1;
    public TeleportLocation TargetTeleportLocation { get { return targetTeleportLocation; } }

    [SerializeField] Material material1, material2;
    [SerializeField] Renderer myRenderer;

    TextMesh myText;

    bool playerIsInTrigger;

    private void Start()
    {
        myRenderer.material = material1;
        myText = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {
        if (playerIsInTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            targetTeleportLocation = targetTeleportLocation == TeleportLocation.LOCATION1 ? TeleportLocation.LOCATION2 : TeleportLocation.LOCATION1;
            myRenderer.material = targetTeleportLocation == TeleportLocation.LOCATION1 ? material1 : material2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerIsInTrigger = true;
        StartCoroutine(FadeText(true));
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsInTrigger = false;
        StartCoroutine(FadeText(false));
    }

    IEnumerator FadeText(bool fadeIn)
    {
        Color originalTextColor = myText.color, newColor = fadeIn ? Color.white : Color.clear;
        float fadeTimer = 1, elapsedTime = 0;

        while (elapsedTime < fadeTimer)
        {
            myText.color = Color.Lerp(originalTextColor, newColor, elapsedTime / fadeTimer);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        myText.color = newColor;
    }
}
