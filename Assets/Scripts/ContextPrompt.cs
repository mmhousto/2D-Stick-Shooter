using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextPrompt : MonoBehaviour
{
    private PlayerInput input;
    private GameObject contextPrompt;
    private TextMeshProUGUI context;
    private Image contextImage;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        contextPrompt = GameObject.FindWithTag("ContextPrompt");
        context = contextPrompt.GetComponentInChildren<TextMeshProUGUI>(true);
        contextImage = contextPrompt.GetComponentInChildren<Image>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if(interactable.Interactable)
                context.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && input.isInteracting)
        {
            if (interactable.Interactable)
                interactable.Interact();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            context.gameObject.SetActive(false);
        }
    }
}
