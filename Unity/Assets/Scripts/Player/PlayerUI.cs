using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI PromptText;
    [SerializeField]
    private TextMeshProUGUI MessageText;

    private float timeToAppear = 2f;
    private float timeWhenDisappear;

    private void Update()
    {
        if (MessageText.enabled && (Time.time >= timeWhenDisappear))
        {
            MessageText.enabled = false;
        }
    }

    public void EnableText(string text)
    {
        MessageText.text = text;
        MessageText.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }


    public void UpdateText(string promptText)
    {
        PromptText.text = promptText;
    }
}
