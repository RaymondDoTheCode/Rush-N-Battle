using UnityEngine;

public class RandomInteraction : Interactable
{
    public GameObject[] scriptsToCall; // An array of GameObjects containing the scripts to call

    protected override void Interact()
    {
        int scriptIndex = Random.Range(0, scriptsToCall.Length);
        scriptsToCall[scriptIndex].SendMessage("Interact", SendMessageOptions.RequireReceiver);
    }
}
