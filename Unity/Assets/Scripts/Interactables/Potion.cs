using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);

        Destroy(transform.parent.gameObject);
    }
}
