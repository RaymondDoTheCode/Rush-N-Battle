using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField]
    private GameObject gameChest;
    private bool chestOpen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);

        if (chestOpen == false)
        {
            chestOpen = true;
        }
        gameChest.GetComponent<Animator>().SetBool("IsOpen", gameChest);
    }
}
