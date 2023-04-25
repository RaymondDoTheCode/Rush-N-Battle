using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // add or remove an InteractionEvent component to this gameobject
    public bool useEvents;

    // message displayed to player when looking at an interactable
    [SerializeField]
    public string promptMessage;

    // this will be called from our player
    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {
        // no code in this function
        // template function to be overriden by our subclasses
    }
}
