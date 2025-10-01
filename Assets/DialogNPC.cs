using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class DialogNPC : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject dialogBackground;
    [SerializeField] private TMP_Text dialogText;

    [SerializeField] private string[] dialogs;
    [SerializeField] private int dialogsCount;
    [SerializeField] private float textSpeed;

    [SerializeField] bool isActive;
    [SerializeField] string interactionText;

    [SerializeField] private bool dialogActive;

    public void Interact()
    {
        if (isActive)
        {
            dialogText.text = string.Empty;
            ShowDialog();
        }
    }

    private void Update()
    {
        if (dialogActive)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                if (dialogText.text == dialogs[dialogsCount])
                {
                    ContinueDialog();
                }
                else
                {
                    StopAllCoroutines();
                    dialogText.text = dialogs[dialogsCount];
                }
            }

        }
    }

    void ShowDialog()
    {
        dialogActive = true;
        dialogBackground.SetActive(true);
        dialogsCount = 0;
        StartCoroutine(typeDialog());
    }

    IEnumerator typeDialog()
    {
        foreach (char c in dialogs[dialogsCount].ToCharArray()) 
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void ContinueDialog()
    {
        if (dialogsCount < dialogs.Length - 1)
        {
            dialogsCount++;
            dialogText.text = string.Empty;
            StartCoroutine(typeDialog());
        }
        else
        {
            dialogActive = false;
            dialogBackground.SetActive(false);
        }
    }
    public string InteractionText()
    {
        if (isActive)
        {
            return interactionText;
        }
        else
        {
            return "";
        }
    }
}
