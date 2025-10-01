using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    public Transform messageText_Transform;

    public TMP_Text messageText;
    public TMP_Text interactionText;

    public float message_VisibilityTime = 3.5f;
    public float message_FadeOut_Duration = 1f;

    public void ShowMessage(string message)
    {
        TMP_Text activeMessageText = Instantiate(messageText, messageText_Transform.position, Quaternion.identity, messageText_Transform);
        activeMessageText.text = message;
        StartCoroutine(FadeOutMessage(activeMessageText));
    }

    public void EnableInteractionText(string text)
    {
        interactionText.enabled = true;
        interactionText.text = text;
    }

    public void DisableInteractionText()
    {
        interactionText.enabled = false;
    }

    IEnumerator FadeOutMessage(TMP_Text activeMessageText)
    {
        yield return new WaitForSeconds(message_VisibilityTime);
        activeMessageText.CrossFadeAlpha(0f, message_FadeOut_Duration, ignoreTimeScale: false);
        Destroy(activeMessageText.gameObject, message_FadeOut_Duration);
    }

}
